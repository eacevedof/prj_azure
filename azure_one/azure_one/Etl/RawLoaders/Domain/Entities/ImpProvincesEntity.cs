namespace azure_one.Etl.RawLoaders.Domain.Entities;

public sealed class ImpProvincesEntity: AbstractEntity
{
    private ImpProvincesEntity()
    {
        Table = "imp_provinces";
        
        PathXls = "/Downloads/data-in.xlsx";
        SheetNr = 3;
        SheetMaxColumn = 12;

        ColumnMapping = new()
        {
            { "Column0", "countries_uuid" },
            { "Column1", "uuid" },
            { "Column2", "val" },
            { "Column3", "codesap" },
            
            { "Column4", "tr_1" },
            { "Column5", "tr_2" },
            { "Column6", "tr_3" },
            { "Column7", "tr_4" },
            { "Column8", "tr_5" },
            { "Column9", "tr_6" },
            { "Column10", "tr_7" },
            { "Column11", "tr_8" },
            { "Column12", "tr_9" },
        };     
    }
    
    public static ImpProvincesEntity GetInstance()
    {
        return new ImpProvincesEntity();
    }     
}