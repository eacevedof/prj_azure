using azure_one.Etl.HaveALook.Application;

namespace azure_one.Etl.HaveALook.Infrastructure.Controllers;

public sealed class CheckPaginationController
{
    private readonly CheckPaginationService _checkPaginationService;

    public CheckPaginationController(CheckPaginationService checkPaginationService)
    {
        _checkPaginationService = checkPaginationService;
    }

    public  Invoke()
    {
        _checkPaginationService.Invoke();
    }
}