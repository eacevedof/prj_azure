namespace azure_one.Etl.RawLoaders.Domain.Entities;

public sealed class ImpStatusEmployeesEntity : AbstractEntity
{
    private ImpStatusEmployeesEntity()
    {
        Table = "imp_status_employees";
     
        PathXls = "/Downloads/data-in.xlsx";
        SheetNr = 8;
        SheetMaxColumn = 11;

        ColumnMapping = new()
        {
            { "Column0", "uuid" },
            { "Column1", "val" },
            //{ "Column2", "codesap" },
            { "Column2", "tr_1" },
            { "Column3", "tr_2" },
            { "Column4", "tr_3" },
            { "Column5", "tr_4" },
            { "Column6", "tr_5" },
            { "Column7", "tr_6" },
            { "Column8", "tr_7" },
            { "Column9", "tr_8" },
            { "Column10", "tr_9" },
        };     
    }
    
    public static ImpStatusEmployeesEntity GetInstance()
    {
        return new ImpStatusEmployeesEntity();
    }
}