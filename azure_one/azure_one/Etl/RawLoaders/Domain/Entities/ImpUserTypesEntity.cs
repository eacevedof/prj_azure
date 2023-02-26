namespace azure_one.Etl.RawLoaders.Domain.Entities;

public sealed class ImpUserTypesEntity : AbstractEntity
{
    private ImpUserTypesEntity()
    {
        Table = "imp_countries";
     
        PathXls = "/Downloads/data-in.xlsx";
        SheetNr = 7;
        SheetMaxColumn = 12;

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
    
    public static ImpUserTypesEntity GetInstance()
    {
        return new ImpUserTypesEntity();
    }
}