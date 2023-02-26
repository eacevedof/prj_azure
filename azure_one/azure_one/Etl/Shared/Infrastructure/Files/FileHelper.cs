using System;
using System.IO;
using System.Collections;

namespace azure_one.Etl.Shared.Infrastructure.Files;

public sealed class FileHelper
{
    public static string GetFileContent(string pathFile)
    {
        return File.ReadAllText($@"{pathFile}");
    }

    public static string GetSqlFilesFolder()
    {
        return Path.Combine(Directory.GetCurrentDirectory(), "sql_files");
    }

    public static bool isFile(string pathFile)
    {
        return File.Exists(pathFile);
    }

    public static bool isDir(string pathDir)
    {
        return Directory.Exists(pathDir);
    }

    public static string[] GetFileNamesInDir(string pathDir, string search="")
    {
        string [] fileEntries = Directory.GetFiles(pathDir, search);
        return fileEntries;
    }
}