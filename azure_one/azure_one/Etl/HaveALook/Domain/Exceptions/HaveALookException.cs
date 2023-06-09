using System;

namespace azure_one.Etl.HaveALook.Domain.Exceptions;

public class HaveALookException : AbstractHaveALookException
{

    public HaveALookException(string message, int code = 0) : base(message, code)
    {
    }

    public static void FailIfMissingPage()
    {
        throw new HaveALookException("missing page", AbstractHaveALookException.BAD_REQUEST_CODE);
    }

    public static void FailIfPageIsNotInteger()
    {
        throw new HaveALookException("page must be a integer", AbstractHaveALookException.BAD_REQUEST_CODE);
    }

    public static void FailIfMissingPerPage()
    {
        throw new HaveALookException("per_page is not provided", AbstractHaveALookException.BAD_REQUEST_CODE);
    }

    public static void FailIfPerPageIsNotInteger()
    {
        throw new HaveALookException("per_page must be a integer", AbstractHaveALookException.BAD_REQUEST_CODE);
    }


}