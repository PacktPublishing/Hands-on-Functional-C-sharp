using System;
using System.Collections.Generic;
using System.Linq;

class Demo
{
    public static Func<int, int> InClosure(int i, Func<int, int, int> function)
    {
        // create a function that closes over i
        Func<int, int> result =
            x => function(i, x);

        // and we return that allowing for the user to only specify the second argument
        // (this is also called partial application)
        return result;
    }

    public static int Add(int x, int y)
    {
        return x + y;
    }
    public static void Main()
    {
        var AddOneTo = InClosure(1, Add);

        System.Console.WriteLine($"1 + 5 = {AddOneTo(5)}");
        System.Console.WriteLine($"1 + 3 = {AddOneTo(3)}");
        System.Console.WriteLine($"1 + 7 = {AddOneTo(7)}");

        // PrintOut(InClosureForEach);
        PrintOut(InClosureFor);
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

    public static IEnumerable<Func<int>> InClosureForEach()
    {
        System.Console.WriteLine("Creating closures with for each");
        var range = Enumerable.Range(1, 10);

        var evens = new List<Func<int>>();
        foreach (var i in range)
        {
            evens.Add(() => i);
        }

        return evens;
    }

    public static IEnumerable<Func<int>> InClosureFor()
    {
        System.Console.WriteLine("Creating closure over for loop");
        var range = Enumerable.Range(1, 10);

        var evens = new List<Func<int>>();
        for (int i = 0; i < 10; i++)
        {
            evens.Add(() => i);
        }

        return evens;
    }

}

internal class Explain
{
    public void ClosureWithShared()
    {
        //  

        var funcs = new List<Func<int>>();
        var i = 0;
        for (; i < 10; i++)
        {
            funcs.Add(() => i);
        }

        // here all the funcs have closed over the same i 
        // and when we use it - we'll use the last ever value of i
        // which comes to 10  (the final i++ increments from 9 to 10 and the for loop exits 
        // because 10 < 10 is false) 
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