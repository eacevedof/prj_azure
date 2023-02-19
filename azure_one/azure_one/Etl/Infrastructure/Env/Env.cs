using System;

namespace azure_one.Etl.Infrastructure.Env;

public static class Env
{
    public static string Get(string key)
    {
        return Environment.GetEnvironmentVariable(key);
    }
}