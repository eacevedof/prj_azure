using System;

namespace azure_one.Etl.HaveALook.Domain.Exceptions;

public class HaveALookException : AbstractHaveALookException
{
    public HaveALookException(string message) : base(message)
    {
    }

    public static void FailIfMissingPage()
    {
        throw new HaveALookException("missing page");
    }

    public static void FailIfPageIsNotInteger()
    {
        throw new HaveALookException("Page must be a integer");
    }
}