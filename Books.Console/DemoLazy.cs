using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

public class Lazy
{
    public static void Demo()
    {
        var shows = new List<string> {
            "Harry Potter",
            "Prince of Persia",
            "Moon can be a Harsh Mistress",
            "Sindbad and the 9 Seas",
            "Robinson Crusoe",
            "Lost",
            "Star Wars: The Empire Strikes Back",
            "The Godfather"
        };

        var longShows = shows
            .Where(s => s.Length > 15)
            .Select(s =>
            {
                Console.WriteLine(s);
                return s.Length;
            });

        // at this point none of the logs have apperead
        Console.WriteLine("After lazy part - notice no lines printed out - yet \n--------- press key ---------");
        Console.ReadLine();

        // force the invocation - as no other way to "flesh" out the list
        // we can force completion by any method requiring the whole collection
        longShows.ToList();
        Console.WriteLine("\n--------- ----- --- ---------\n");

        // Prepares the ordering - but does not go through - no one needs it yet
        var ordered = shows.OrderBy(s =>
        {
            Console.Write(".");
            return s.Length;
        });

        // aggregation methods - not lazy
        var maxLength = shows.Max(s =>
        {
            // this will get printed out along with each iteration while calculating the max length
            Console.Write("calcMax/");
            return s.Length;
        });

        Console.WriteLine($"\nmaxLenght: {maxLength} ");
        Console.WriteLine("\n--------- ----- --- ---------\n");


        // we never used "ordered" and therefore it was never actually computed/executed


        // foreach - not lazy
        var t = new List<int>();
        foreach (var item in shows)
        {
            Console.Write("+");
            t.Add(item.Length);
        }

    }
}
