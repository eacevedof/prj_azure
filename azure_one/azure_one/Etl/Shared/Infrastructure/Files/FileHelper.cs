using System;
using System.IO;
using System.Runtime.CompilerServices;
using Microsoft.Azure.WebJobs;

namespace azure_one.Etl.Shared.Infrastructure.Files;

public sealed class FileHelper
{
    private ExecutionContext _context;

    public FileHelper(ExecutionContext context)
    {
        _context = context;
    }
    
    public FileHelper() {}
    
    public static FileHelper GetInstance()
    {
        return new FileHelper();
    }
    public static FileHelper GetInstance(ExecutionContext context)
    {
        return new FileHelper(context);
    }
    
    public string GetFilePath(string pathFile)
    {
        return GetAppDir() + pathFile;
    }
    
    public string GetAppDir()
    {
        string CarpetaEquiposUbicaciones = Path.Combine(_context.FunctionAppDirectory, "sql_files");
        return CarpetaEquiposUbicaciones;
        //return Environment.CurrentDirectory;
        //return Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\.."));
    }
    
    public static string GetFileContent(string pathFile)
    {
        return File.ReadAllText($@"{pathFile}");
    }

}