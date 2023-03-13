using Newtonsoft.Json;

namespace azure_one.Etl.Shared.Infrastructure.Files;

public sealed class MappingReader
{
    public static dynamic JsonDecodeImpTables(string pathFile)
    {
        //string pathFolder = FileHelper.GetMappingFolder();
        //string pathFile = $"{pathFolder}/imp_tables/{fileName}.json";
        if (FileHelper.isFile(pathFile))
        {
            string strJson = FileHelper.GetFileContent(pathFile);
            return JsonConvert.DeserializeObject(strJson);
        }
        return null;
    }
    
    public static dynamic JsonDecodeFromForceFolder(string fileName)
    {
        string pathFolder = FileHelper.GetMappingFolder();
        string pathFile = $"{pathFolder}/force/{fileName}.json";

        if (FileHelper.isFile(pathFile))
        {
            string strJson = FileHelper.GetFileContent(pathFile);
            return JsonConvert.DeserializeObject(strJson);
        }
        return null;
    }
}