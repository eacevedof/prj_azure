using azure_one.Etl.Shared.Infrastructure.Db.QueryBuilders;

namespace azure_one.Etl.RawLoaders.Domain.Entities;

public sealed class ImpLanguagesEntity: AbstractEntity
{
    private ImpLanguagesEntity()
    {
        PathXls = "/Downloads/data-in.xlsx";
        Table = "imp_languages";
        SheetNr = 1;
        SheetMaxColumn = 3;

        ColumnMapping = new()
        {
            { "Column0", "uuid" },
            { "Column1", "val" },
            { "Column2", "codesap" },
            { $"{BulkInsert.TAG_CONSTANT}:hola", "imp_uuid"},
            { $"{BulkInsert.TAG_CONSTANT}:finix", "tenant_slug"},
        };        
    }
    
    public static ImpLanguagesEntity GetInstance()
    {
        return new ImpLanguagesEntity();
    }    
}