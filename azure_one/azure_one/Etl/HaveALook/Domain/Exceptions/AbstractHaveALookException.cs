using System;

namespace azure_one.Etl.HaveALook.Domain.Exceptions;

public abstract class AbstractHaveALookException : Exception
{
    protected int _code = 0;
    protected const int BAD_REQUEST_CODE = 400;

    public AbstractHaveALookException(string message, int code = 0) : base(message)
    {
        _code = code;
    }

    public int GetCode()
    {
        return _code;
    }
}