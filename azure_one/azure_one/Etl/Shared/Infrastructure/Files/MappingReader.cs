using Newtonsoft.Json;

namespace azure_one.Etl.Shared.Infrastructure.Files;

public sealed class MappingReader
{
    public static dynamic JsonDecode(string fileName)
    {
        string pathFolder = FileHelper.GetMappingFolder();
        string pathFile = $"{pathFolder}/{fileName}.json";

        if (FileHelper.isFile(pathFile))
            return JsonConvert.DeserializeObject(pathFile);
        return null;
    }
}