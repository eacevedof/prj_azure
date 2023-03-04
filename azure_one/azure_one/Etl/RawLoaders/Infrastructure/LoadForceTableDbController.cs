using azure_one.Etl.RawLoaders.Application.Force;

namespace azure_one.Etl.RawLoaders.Infrastructure;

public sealed class LoadForceTableController
{
    private readonly LoadXlsForceService _loadXlsForceService;

    public LoadForceTableController(LoadXlsForceService loadXlsForceService)
    {
        _loadXlsForceService = loadXlsForceService;
    }

    public void Invoke()
    {
        _loadXlsForceService.Invoke();
    }
}