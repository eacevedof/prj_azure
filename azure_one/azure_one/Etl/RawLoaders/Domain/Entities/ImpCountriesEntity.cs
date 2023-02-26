namespace azure_one.Etl.RawLoaders.Domain.Entities;

public sealed class ImpCountriesEntity : AbstractEntity
{
    private ImpCountriesEntity()
    {
        Table = "imp_countries";
     
        PathXls = "/Downloads/data-in.xlsx";
        SheetNr = 2;
        SheetMaxColumn = 11;

        ColumnMapping = new()
        {
            { "Column0", "uuid" },
            { "Column1", "val" },
            { "Column2", "codesap" },
            { "Column3", "tr_1" },
            { "Column4", "tr_2" },
            { "Column5", "tr_3" },
            { "Column6", "tr_4" },
            { "Column7", "tr_5" },
            { "Column8", "tr_6" },
            { "Column9", "tr_7" },
            { "Column10", "tr_8" },
            { "Column11", "tr_9" },
        };     
    }
    
    public static ImpCountriesEntity GetInstance()
    {
        return new ImpCountriesEntity();
    }
}