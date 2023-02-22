using System;
using System.Collections.Generic;

namespace azure_one.Etl.Shared.Infrastructure.Log;

public static class Lg
{
    public static void pr(string text, string title="")
    {
        if (title!="")
            Console.WriteLine(title);
        
        Console.WriteLine(text);
    }
    
    public static void PrRows(List<Dictionary<string, string>> rows)
    {
        foreach (Dictionary<string, string> row in rows)
        {
            pr("==================================");
            foreach (var column in row)
            {
                pr($"{column.Key} => {column.Value}");
            }
        }            
    }
    
    public static void PrRows(List<string> rows)
    {
        foreach (string row in rows) pr(row);
    }
}