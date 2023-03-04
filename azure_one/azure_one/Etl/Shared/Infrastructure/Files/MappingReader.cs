using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace azure_one.Etl.Shared.Infrastructure.Files;

public sealed class MappingReader
{
    public static dynamic JsonDecode(string fileName)
    {
        string pathFolder = FileHelper.GetMappingFolder();
        string pathFile = $"{pathFolder}/{fileName}.json";

        if (FileHelper.isFile(pathFile))
        {
            string strJson = FileHelper.GetFileContent(pathFile);
            return JsonConvert.DeserializeObject(strJson);
        }

        return null;
    }

    public static Dictionary<string, string> GetMappingFromObject(dynamic objList)
    {
        Dictionary<string, string> mapping = new();
        foreach (JObject row in objList)
            foreach (KeyValuePair<string, JToken> prop in row)
                mapping.Add(prop.Key, prop.Value.ToString());
        return mapping;
    }
}