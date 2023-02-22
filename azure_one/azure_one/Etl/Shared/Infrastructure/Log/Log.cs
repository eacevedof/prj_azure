using System;
using System.Collections.Generic;

namespace azure_one.Etl.Infrastructure.Log;

public static class Lg
{
    public static void Pr(string text, string title="")
    {
        if (title!="")
            Console.WriteLine(title);
        
        Console.WriteLine(text);
    }
    
    public static void PrRows(List<Dictionary<string, string>> rows)
    {
        foreach (Dictionary<string, string> row in rows)
        {
            Pr("==================================");
            foreach (var column in row)
            {
                Pr($"{column.Key} => {column.Value}");
            }
        }            
    }
    
    public static void PrRows(List<string> rows)
    {
        foreach (string row in rows) Pr(row);
    }
}