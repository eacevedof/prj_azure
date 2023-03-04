using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace azure_one.Etl.RawLoaders.Domain.Entities;

public sealed class GenericMapper
{
    private readonly Dictionary<string, string> _source;
    private readonly Dictionary<string, string> _target;
    private readonly Dictionary<string, string> _mapping;

    public GenericMapper(dynamic fromJson)
    {
        _source = GetMappingFromObject(fromJson.source);
        _target = GetMappingFromObject(fromJson.target);
        _mapping = GetMappingFromObjectList(fromJson.mapping);
    }
 
    private Dictionary<string, string> GetMappingFromObject(dynamic simpleObj)
    {
        Dictionary<string, string> result = new();
        foreach (KeyValuePair<string, JToken> prop in simpleObj)
        {
            if (result.ContainsKey(prop.Key)) continue;
            result.Add(prop.Key, prop.Value.ToString());
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