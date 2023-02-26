namespace azure_one.Etl.RawLoaders.Domain.Entities;

public sealed class ImpCompaniesEntity: AbstractEntity
{
    private ImpCompaniesEntity()
    {
        Table = "imp_companies";
        
        PathXls = "/Downloads/data-in.xlsx";
        SheetNr = 5;
        SheetMaxColumn = 10;

        ColumnMapping = new()
        {
            { "Column0", "city_uuid" },
            { "Column1", "uuid" },
            { "Column2", "company_type" },
            { "Column3", "company_name" },
            
            { "Column4", "company_address1" },
            { "Column5", "company_address2" },
            { "Column6", "company_cp" },
            { "Column7", "company_contact_person" },
            { "Column8", "company_contact_phone" },
            { "Column9", "company_contact_email" },
            { "Column10", "codesap" },
        };
    }
    
    public static ImpCompaniesEntity GetInstance()
    {
        return new ImpCompaniesEntity();
    }     
}