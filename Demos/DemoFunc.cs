using System;
using System.Collections.Generic;
using System.Linq;
using Books.ConsoleApp;

public class DemoFunc
{
    private Func<int> intFunc;

    private Func<T> AsFunc<T>(T input)
    {
        return () => input;
    }

    private T Exec<T>(Func<T> func)
    {
        return func();
    }


    public void ContrivedMethod()
    {
        Func<string> helloWorldFunc = AsFunc("Hello World!");
        var helloWorld = Exec(helloWorldFunc);
        // helloWorld == "Hello World!"
        
        intFunc = AsFunc(1);
        var one = Exec(intFunc);
        // one == 1);
    }


    public void SomewhatMoreRealisticMethod()
    {
        // cant use var - because we need to tell the compiler what we'll describe
        Func<IEnumerable<BooksByAuthor>, string, bool> authorExists =
            (booksByAuthor, authorName) => booksByAuthor.Any(ba => ba.Author == authorName);


        Func<IEnumerable<BooksByAuthor>, string, BooksByAuthor> getGataloguedAuthor =
            (booksByAuthor, authorName) => {
                // can do complex stuff or help in debugging
                return booksByAuthor.FirstOrDefault(ba => ba.Author == authorName);
            };


        var bas = Enumerable.Empty<BooksByAuthor>();// for conciseness

        var exits = authorExists(bas, "Homer");

        var homersBooks = getGataloguedAuthor(bas, "Homer");
    }
}
