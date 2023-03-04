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
  
        };     
    }
    
    public static ImpProvincesEntity GetInstance()
    {
        return new ImpProvincesEntity();
    }     
}