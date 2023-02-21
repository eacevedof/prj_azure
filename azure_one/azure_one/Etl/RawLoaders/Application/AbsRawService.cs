using azure_one.Etl.Infrastructure.Files;

namespace azure_one.Etl.RawLoaders.Application;

public abstract class AbsRawService
{
    protected ExcelReader _excelReader;
}