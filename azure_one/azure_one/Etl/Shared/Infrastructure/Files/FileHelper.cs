using System;
using System.IO;
using System.Runtime.CompilerServices;
using Microsoft.Azure.WebJobs;

namespace azure_one.Etl.Shared.Infrastructure.Files;

public sealed class FileHelper
{
    public static ExecutionContext Context;
    public static string GetFilePath(string pathFile)
    {
        return GetAppDir(Context) + pathFile;
    }

    public static string GetAppDir(ExecutionContext context)
    {
        string CarpetaEquiposUbicaciones = Path.Combine(context.FunctionAppDirectory, "sql_files");
        return CarpetaEquiposUbicaciones;
        //return Environment.CurrentDirectory;
        //return Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\.."));
    }
    
    public static string GetFileContent(string pathFile)
    {
        return File.ReadAllText($@"{pathFile}");
    }

}