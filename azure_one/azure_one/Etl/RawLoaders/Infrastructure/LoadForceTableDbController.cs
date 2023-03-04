using azure_one.Etl.RawLoaders.Application.Force;

namespace azure_one.Etl.RawLoaders.Infrastructure;

public sealed class LoadForceTableController
{
    private readonly LoadXlsForceService _loadXlsForService;

    public LoadForceTableController(LoadXlsForceService loadXlsForService)
    {
        _loadXlsForService = loadXlsForService;
    }

    public void Invoke()
    {
        _loadXlsForService.Invoke();
    }
}