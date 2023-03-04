using System.Collections.Generic;
using azure_one.Etl.RawLoaders.Domain.Exceptions;
using Newtonsoft.Json.Linq;
using azure_one.Etl.Shared.Infrastructure.Files;
using azure_one.Etl.Shared.Infrastructure.Env;

namespace azure_one.Etl.RawLoaders.Domain.Entities;

public sealed class ExcelMapper
{
    private readonly Dictionary<string, string> _source;
    private readonly Dictionary<string, string> _target;
    private readonly Dictionary<string, string> _mapping;

    private ExcelMapper(string jsonFileName)
    {
        dynamic fromJson = MappingReader.JsonDecodeImpTables(jsonFileName);
        if (fromJson is null) throw new JsonFileNotFoundException($"json file {jsonFileName}.json not found");
        _target = GetMappingFromObject(fromJson.target);
        _mapping = GetMappingFromObjectList(fromJson.mapping);
        _source = GetMappingFromObject(fromJson.source);
        
        string pathHome = Env.Get("HOME");
        string pathExcel = _source["path"];
        pathExcel = pathExcel.Replace("%folder_in%", "Downloads");
        _source["path"] = $"{pathHome}/{pathExcel}";
    }

    public static ExcelMapper GetInstance(string jsonFileName)
    {
        return new ExcelMapper(jsonFileName);
    }
 
    private Dictionary<string, string> GetMappingFromObject(dynamic simpleObj)
    {
        Dictionary<string, string> result = new();
        foreach (JProperty prop in simpleObj)
        {
            string key = prop.Name;
            if (result.ContainsKey(key)) continue;
            string value = prop.Value.ToString();
            result.Add(key, value);
        }
        return result;
    }
    
    private Dictionary<string, string> GetMappingFromObjectList(dynamic objList)
    {
        Dictionary<string, string> mapping = new();
        foreach (JObject row in objList)
            foreach (KeyValuePair<string, JToken> prop in row)
            {
                if (mapping.ContainsKey(prop.Key)) continue;
                mapping.Add(prop.Key, prop.Value.ToString());
            }
        return mapping;
    }

    public Dictionary<string, string> Source
    {
        get { return _source;}
    }
    
    public Dictionary<string, string> Target
    {
        get { return _target;}
    }    
    
    public Dictionary<string, string> Mapping
    {
        get { return _mapping;}
    }
    
}