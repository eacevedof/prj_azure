using System;

namespace azure_one.Etl.HaveALook.Domain.Exceptions;

public abstract class AbstractHaveALookException : Exception
{
    public AbstractHaveALookException(string message) : base(message)
    {
    }
}