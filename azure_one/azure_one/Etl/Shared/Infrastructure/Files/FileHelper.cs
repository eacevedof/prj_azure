using System.IO;

namespace azure_one.Etl.Shared.Infrastructure.Files;

public sealed class FileHelper
{
    public static string GetCurrentPath()
    {
        string filePath = Path.Combine(Directory.GetCurrentDirectory(), "");
        return filePath;
    }
}