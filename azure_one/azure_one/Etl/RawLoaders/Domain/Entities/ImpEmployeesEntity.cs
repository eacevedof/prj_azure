namespace azure_one.Etl.RawLoaders.Domain.Entities;

public sealed class ImpEmployeesEntity : AbstractEntity
{
    private ImpEmployeesEntity()
    {
        Table = "imp_employees";
     
        PathXls = "/Downloads/data-in.xlsx";
        SheetNr = 12;
        SheetMaxColumn = 12;

        ColumnMapping = new()
        {
            { "Column0", "uuid" },
            { "Column1", "employee_name" },
            { "Column2", "employee_surname_1" },
            { "Column3", "employee_surname_2" },
            { "Column4", "user_types_uuid" },
            { "Column5", "company_uuid" },
            { "Column6", "employee_email" },
            { "Column7", "language_uuid" },
            { "Column8", "department_uuid" },
            { "Column9", "position_uuid" },
            { "Column10", "rol_uuid" },
            { "Column11", "codesap" },
        };     
    }
    
    public static ImpEmployeesEntity GetInstance()
    {
        return new ImpEmployeesEntity();
    }
}