using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Extensions;
using azure_one.Etl.Shared.Infrastructure.Files;
using Newtonsoft.Json.Linq;

namespace azure_one.Etl.Shared.Infrastructure.Db.Contexts;

public sealed class ContextFinder
{
    private static readonly List<ContextDto> _contexts = new();
    
    private static void LoadContexts()
    {
        if (!_contexts.IsEmpty()) return;
        
        dynamic contexts = JsonDecode();
        foreach (dynamic context in contexts)
        {
            ContextDto dto = new ();
            dto.Id = context.id ?? -1;
            dto.Server = context.server ?? "127.0.0.1";
            dto.Database = context.database ?? "db_unknown";
            dto.Port = context.port ?? "";
            dto.Username = context.username ?? "";
            dto.Password = context.password ?? "";
            _contexts.Add(dto);
        }
    }
    
    private static dynamic JsonDecode()
    {
        string pathFile = Path.Combine(Directory.GetCurrentDirectory(), "config/contexts.json");
        if (FileHelper.isFile(pathFile))
        {
            string strJson = FileHelper.GetFileContent(pathFile);
            return JsonConvert.DeserializeObject(strJson);
        }
        return null;
    }

    public static ContextDto GetById(ContextsEnum id)
    {
        LoadContexts();
        foreach (ContextDto dto in _contexts)
        {
            if (dto.Id == id.ToString())
                return dto;
        }
        return null;
    }
}