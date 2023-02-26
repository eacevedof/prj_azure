using azure_one.Etl.Shared.Infrastructure.Db.QueryBuilders;

namespace azure_one.Etl.RawLoaders.Domain.Entities;

public sealed class ImpLanguagesCompanyCustomEntity: AbstractEntity
{
    private ImpLanguagesCompanyCustomEntity()
    {
        Table = "languages_company_custom";
        
        PathXls = "/Downloads/data-in.xlsx";
        SheetNr = 6;
        SheetMaxColumn = 4;

        ColumnMapping = new()
        {
            { "Column0", "companies_uuid" },
            { "Column1", "lang_from" },
            { "Column2", "lang_tr" },
            { "Column3", "value_tr" },
            { "Column4", "tr_num" },
            //{ $"{BulkInsert.TAG_CONSTANT}:hola", "imp_uuid"},
            //{ $"{BulkInsert.TAG_CONSTANT}:finix", "tenant_slug"},
        };        
    }
    
    public static ImpLanguagesCompanyCustomEntity GetInstance()
    {
        return new ImpLanguagesCompanyCustomEntity();
    }    
}