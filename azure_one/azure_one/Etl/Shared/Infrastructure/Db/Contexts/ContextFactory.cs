using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using azure_one.Etl.Shared.Infrastructure.Db;
using Newtonsoft.Json;
using azure_one.Etl.Shared.Infrastructure.Files;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Extensions;

namespace azure_one.Etl.Shared.Infrastructure.Db.Contexts;

public sealed class ContextFactory
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
            dto.Server = context.server ?? "localhost";
            dto.Database = context.databse ?? "db_unknown";
            dto.Port = context.port ?? "1433";
            dto.Username = context.username ?? "sa";
            dto.Password = context.password ?? "";
            _contexts.Add(dto);
        }
    }
    
    private static dynamic JsonDecode()
    {
        string pathFile = Path.Combine(
                Directory.GetCurrentDirectory(), 
                "Etl/Shared/Infrastructure/Db/Contexts/contexts.json"
            );

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