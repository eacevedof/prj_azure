using System.IO;

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
}