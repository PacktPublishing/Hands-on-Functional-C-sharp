using System;
using System.Collections.Generic;
using System.Linq;

public class DemoLINQ
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
            .Select(s => {
                Console.WriteLine(s);
                return s.Length;
            });

        // at this point none of the logs have apperead
        Console.WriteLine("After lazy part");

        // force the invocation - as no other way to "flesh" out the list
        longShows.ToList();
    }
}
