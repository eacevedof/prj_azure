using System;

namespace azure_one.Etl.RawLoaders.Domain.Exceptions;

public sealed class SheetNotFoundException : Exception
{
    public SheetNotFoundException(string message) : base(message)
    {
        
    }
}