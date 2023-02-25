namespace azure_one.Etl.RawLoaders.Domain.Entities;

public sealed class ImpCitiesEntity: AbstractEntity
{
    private ImpCitiesEntity()
    {
        Table = "imp_cities";
     
        PathXls = "/Downloads/data-in.xlsx";
        SheetNr = 4;
        SheetMaxColumn = 3;

        ColumnMapping = new()
        {
            { "Column0", "provinces_uuid" },
            { "Column1", "uuid" },
            { "Column2", "val" },
            { "Column3", "codesap" },
        };     
    }
    
    public static ImpCitiesEntity GetInstance()
    {
        return new ImpCitiesEntity();
    }     
}