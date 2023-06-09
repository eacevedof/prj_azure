using System;

namespace azure_one.Etl.HaveALook.Domain.Exceptions;

public class HaveALookException : AbstractHaveALookException
{
    public HaveALookException(string message) : base(message)
    {
    }

    public static void Ex()
    {
        throw new HaveALookException("xx");
    }
}