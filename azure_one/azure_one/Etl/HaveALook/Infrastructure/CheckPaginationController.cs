using azure_one.Etl.HaveALook.Application;

namespace azure_one.Etl.HaveALook.Infrastructure;

public sealed class CheckPaginationController
{
    private readonly CheckPaginationService _CheckPaginationService;

    public CheckPaginationController(CheckPaginationService CheckPaginationService)
    {
        _CheckPaginationService = CheckPaginationService;
    }

    public void Invoke()
    {
        _CheckPaginationService.Invoke();
    }
}