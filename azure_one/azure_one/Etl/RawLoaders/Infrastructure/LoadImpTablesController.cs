using azure_one.Etl.RawLoaders.Application.ImpTables;

namespace azure_one.Etl.RawLoaders.Infrastructure;

public sealed class LoadImpTablesController
{
    private readonly LoadXlsFilesIntoImpTables _loadXlsFilesIntoImpTables;
    
    public LoadImpTablesController(
        LoadXlsFilesIntoImpTables loadXlsFilesIntoImpTables
    )
    {
        _loadXlsFilesIntoImpTables = loadXlsFilesIntoImpTables;
    }

    public void Invoke()
    {
        _loadXlsFilesIntoImpTables.Invoke();
    }
}