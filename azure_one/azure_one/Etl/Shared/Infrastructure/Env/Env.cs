using System;

namespace azure_one.Etl.Shared.Infrastructure.Env;

public static class Env
{
    public static string Get(string key)
    {
        return Environment.GetEnvironmentVariable(key);
    }

    public static string GetConcat(string key, string add)
    {
        return Environment.GetEnvironmentVariable(key) + add;
    }
}