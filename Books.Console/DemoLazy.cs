using System;
using System.Collections.Generic;
using System.Linq;

public class Lazy
{
    public static void Demo()
    {
        var shows = new List<string> {
            "Star Wars: The Empire Strikes Back",
            "The Godfather",
            "Harry Potter",
            "Prince of Persia",
            "Moon can be a Harsh Mistress",
            "Sindbad and the 9 Seas",
            "Robinson Crusoe",
            "Lost"
        };

        var longShows = shows
            .Where(s => s.Length > 15)
            .Select(s =>
            {
                Console.WriteLine(s);
                return s.Length;
            });

        // at this point none of the logs have appeared
        Console.WriteLine("After lazy part - notice no lines printed out - yet \n--------- press key ---------");
        Console.ReadLine();

        // force the invocation - as no other way to "flesh out" the collection
        // we can force completion by any method that requires all of the items to finish their job
        // ToArray(), Count(), Max(), Min() ...
        longShows.ToList();
        Console.WriteLine("\n--------- Max   --- ---------\n");
        
        // aggregation methods - not lazy
        Console.WriteLine("Print out ^ for each item used in Max method ");
        var maxLength = shows.Max(s =>
        {
            // this will get printed out along with each iteration while calculating the max length
            Console.Write("^");
            return s.Length;
        });

        Console.WriteLine($"\n MaxLenght is: {maxLength} ");
        Console.WriteLine("\n--------- Ordered   ---------\n");


        // Prepares the ordering - but does not go through - no one needs it yet
        var ordered = shows.OrderBy(s =>
        {
            Console.Write(".");
            return s.Length;
        });

        // we never used "ordered" and therefore it was never actually computed/executed
        //// if we uncomment the line below ordered will get used

        //Console.WriteLine("\nThe dots are printed out for each item. And the top ordered one is :\n"
        //    + ordered.First());

        // notice how ordered needs to go over the whole collection

        Console.WriteLine("\n--------- Only first -------\n");
        // we'd get only one "only first" in the console because Linq takes the first and finishes
        var firstShow = shows
            .Where(s => { Console.Write("First only. "); return true; })
            .First();
        Console.WriteLine(firstShow);

        Console.WriteLine("\n--------- For each  ---------\n");
        // foreach - not lazy
        Console.WriteLine("+ is printed for each item in foreach:");
        foreach (var item in shows)
        {
            Console.Write("+");
        }

    }
}
