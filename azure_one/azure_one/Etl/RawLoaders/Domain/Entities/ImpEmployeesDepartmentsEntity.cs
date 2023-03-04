namespace azure_one.Etl.RawLoaders.Domain.Entities;

public sealed class ImpEmployeesDepartmentsEntity : AbstractEntity
{
    private ImpEmployeesDepartmentsEntity()
    {
        Table = "";
     
        PathXls = "/Downloads/data-in.xlsx";
        SheetNr = 9;
        SheetMaxColumn = 11;

        ColumnMapping = new()
        {
     
        };     
    }
    
    public static ImpEmployeesDepartmentsEntity GetInstance()
    {
        return new ImpEmployeesDepartmentsEntity();
    }
}