using System;
using System.Collections.Generic;

namespace azure_one.Etl.Infrastructure.Generators;

public static class Rnd
{
    public static int Number(int from, int to)
    {
        return (new Random()).Next(from, to);
    }
    
    public static string Text(int length)
    {
        List<string> chars = new List<string>();
        for (char c = 'A'; c <= 'Z'; c++)
            chars.Add(c.ToString());
        for (char c = 'a'; c <= 'z'; c++)
            chars.Add(c.ToString());
        for (char c = '0'; c <= '9'; c++)
            chars.Add(c.ToString());
        List<string> rnd = new List<string>();
        for (int i = 0; i < length; i++)
        {
            int pos = Number(0, chars.Count - 1);
            rnd.Add(chars[pos]);
        }

        return string.Join("",rnd);
    }
}