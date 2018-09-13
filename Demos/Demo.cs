using System;

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
    }
}