using System;

namespace azure_one.Etl.HaveALook.Domain.Exceptions;

public class HaveALookException : AbstractHaveALookException
{

    public HaveALookException(string message, int code = 0) : base(message, code)
    {
    }

    public static void FailIfMissingPage()
    {
        throw new HaveALookException("missing page");
    }

    public static void FailIfPageIsNotInteger()
    {
        throw new HaveALookException("page must be a integer");
    }

    public static void FailIfMissingPerPage()
    {
        throw new HaveALookException("per_page is not provided");
    }

    public static void FailIfPerPageIsNotInteger()
    {
        throw new HaveALookException("per_page must be a integer");
    }


}