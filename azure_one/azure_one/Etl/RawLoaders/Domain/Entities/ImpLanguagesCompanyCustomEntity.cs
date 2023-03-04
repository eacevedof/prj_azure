using azure_one.Etl.Shared.Infrastructure.Db.QueryBuilders;

namespace azure_one.Etl.RawLoaders.Domain.Entities;

public sealed class ImpLanguagesCompanyCustomEntity: AbstractEntity
{
    private ImpLanguagesCompanyCustomEntity()
    {
        Table = "";
        
        PathXls = "/Downloads/data-in.xlsx";
        SheetNr = 6;
        SheetMaxColumn = 4;

        ColumnMapping = new()
        {
   
            //{ $"{BulkInsert.TAG_CONSTANT}:hola", "imp_uuid"},
            //{ $"{BulkInsert.TAG_CONSTANT}:finix", "tenant_slug"},
        };        
    }
    
    public static ImpLanguagesCompanyCustomEntity GetInstance()
    {
        return new ImpLanguagesCompanyCustomEntity();
    }    
}