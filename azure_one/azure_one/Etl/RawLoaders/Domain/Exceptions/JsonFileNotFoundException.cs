using System;

namespace azure_one.Etl.RawLoaders.Domain.Exceptions;

public sealed class JsonFileNotFoundException : Exception
{
    public JsonFileNotFoundException(string message) : base(message) {}
}