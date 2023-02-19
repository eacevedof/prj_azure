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
    
    public static void PrRowsDics(List<List<Dictionary<string, string>>> rows)
    {
        foreach (List<Dictionary<string, string>> row in rows)
        {
            foreach (Dictionary<string, string> column in row)
            {
                Pr($"{column.}, {column.Value}");
            }
        }            
    }
    
    public static void PrRows(List<object> rows)
    {
        foreach (List<object> row in rows)
        {
            foreach (dynamic column in row)
            {
                Pr($"{column.position}, {column.column}, {column.value}");
            }
        }            
    }
}