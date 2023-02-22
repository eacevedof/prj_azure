using System;
using ExcelDataReader.Log;

namespace azure_one.Etl.RawLoaders.Infrastructure;

public sealed class RawLoadersController
{
    public RawLoadersController() {}

    public void Invoke()
    {
        try
        {

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}