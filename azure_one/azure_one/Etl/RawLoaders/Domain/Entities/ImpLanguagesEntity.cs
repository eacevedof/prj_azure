using System.Collections.Generic;
using azure_one.Etl.Shared.Infrastructure.Db.QueryBuilders;

namespace azure_one.Etl.RawLoaders.Domain.Entities;

public sealed class ImpLanguagesEntity: AbstractEntity
{
    public readonly string PathXls = "/Downloads/data-in.xlsx";
    
    public readonly string Table = "imp_languages";
    public readonly int SheetNr = 1;
    public readonly int SheetMaxColumn = 3;
    
    public readonly Dictionary<string, string> ColumnMapping = new()
    {
        { "Column0", "uuid" },
        { "Column1", "val" },
        { "Column2", "codesap" },
        { $"{BulkInsert.TAG_CONSTANT}:hola", "imp_uuid"},
        { $"{BulkInsert.TAG_CONSTANT}:finix", "tenant_slug"},
    };
    
    public static ImpLanguagesEntity GetInstance()
    {
        return new ImpLanguagesEntity();
    }    
}