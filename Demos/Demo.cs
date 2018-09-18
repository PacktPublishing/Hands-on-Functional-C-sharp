using System;
using System.Collections.Generic;
using System.Linq;

class Demo
{

    public static int Add(int x, int y)
    {
        return x + y;
    }

    public static Func<int, int> InClosure(int i, Func<int, int, int> function)
    {
        // create a function that closes over i
        Func<int, int> result =
            x => function(i, x);

        // and we return that allowing for the user to only specify the second argument
        // (this is also called partial application)
        return result;
    }

    public static void Main()
    {
        var AddOneTo = InClosure(1, Add);

        System.Console.WriteLine($"1 + 5 = {AddOneTo(5)}");
        System.Console.WriteLine($"1 + 3 = {AddOneTo(3)}");
        System.Console.WriteLine($"1 + 7 = {AddOneTo(7)}");

        PrintOut(CreateClosureWithForLoop);
        PrintOut(CreateClosureWithForEachLoop);


        Console.ReadLine();
    }

    private static void PrintOut(Func<IEnumerable<Func<int>>> creator)
    {
        var funcs = creator();
        foreach (var func in funcs)
        {
            var val = func();
            System.Console.WriteLine("Executed and value is " + val);
        }
    }

    public static IEnumerable<Func<int>> CreateClosureWithForLoop()
    {
        System.Console.WriteLine("Creating closure over for loop");

        var funcs = new List<Func<int>>();
        for (int i = 1; i <= 10; i++)
        {
            funcs.Add(() => i);
        }

        return funcs;
    }

    public static IEnumerable<Func<int>> CreateClosureWithForEachLoop()
    {
        System.Console.WriteLine("Creating closures with for each");
        var range = Enumerable.Range(1, 10);

        var funcs = new List<Func<int>>();
        foreach (var i in range)
        {
            funcs.Add(() => i);
        }

        return funcs;
    }
    //should result in:
    //1
    //2
    //3
    //..
    //10

}

internal class Explain
{
    public void ClosureWithShared()
    {
        //  

        var funcs = new List<Func<int>>();
        var i = 1;
        for (; i <= 10; i++)
        {
            funcs.Add(() => i);
        }

        // here all the funcs have closed over the same i 
        // and when we use it - we'll use the last ever value of i
        // because 11 <= 10 is false) 
    }

    public void Closure(IEnumerable<int> enumerable)
    {
        // foreach (var i in enumerable)

        var funcs = new List<Func<int>>();

        var enumerator = enumerable.GetEnumerator();
        while (enumerator.MoveNext())
        {
            // i is a fresh new variable created in the closed over with the func
            var i = enumerator.Current;
            funcs.Add(() => i);
        }
    }
}