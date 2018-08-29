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


    private void ContrivedMethod()
    {
        Func<string> helloWorldFunc = AsFunc("Hello World!");
        var helloWorld = Exec(helloWorldFunc);

        intFunc = AsFunc(1);
        var one = Exec(intFunc);
        var isTrue = (one == intFunc());
    }


    private void SomewhatMoreRealisticMethod()
    {
        // cant use var - because we need to tell the compiler what we'll describe
        Func<IEnumerable<BooksByAuthor>, string, bool> authorExists =
            (booksByAuthor, authorName) => booksByAuthor.Any(ba => ba.Author == authorName);


        Func<IEnumerable<BooksByAuthor>, string, BooksByAuthor> getGataloguedAuthor =
            (booksByAuthor, authorName) => {
                // can do complex stuff or help in debugging
                return booksByAuthor.First(ba => ba.Author == authorName);
            };


        var bas = Enumerable.Empty<BooksByAuthor>();// for conciceness

        var exits = authorExists(bas, "Homer");

        var homersBooks = getGataloguedAuthor(bas, "Homer");
    }
}
