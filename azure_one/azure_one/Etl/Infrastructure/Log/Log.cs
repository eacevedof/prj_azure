using System;

namespace azure_one.Etl.Infrastructure.Log;

public static class Lg
{
    public static void Pr(string text, string title="")
    {
        if (title!="")
            Console.WriteLine(title);
        
        Console.WriteLine(text);
    }
}