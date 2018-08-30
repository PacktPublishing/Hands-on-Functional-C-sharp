using System;

public class DemoAction
{
    private Action justAnAction;

    private Action<T> AsAction<T>()
    {
        var name = typeof(T).Name;
        return v => Console.WriteLine(name);
    }

    private void ContrivedMethod()
    {
        var willTakeString = AsAction<string>();

        willTakeString("Hello world");
        // outputs: String

        //type safety - squiggly
        willTakeString("1");
    }

    Action<string> takesString; // and does stuff with it
    Action<int, string> takesIntAndString; // and does stuff with it

    Action<object> takesAnyObject;

    Action<int, int, int, int, int, int, int, int, int, int, int, int, int, int, int, int> takesMaximum;
    // I've never used so many - so don't worry
}
