using azure_one.Etl.HaveALook.Application;
using azure_one.Etl.HaveALook.Domain;

namespace azure_one.Etl.HaveALook.Infrastructure.Controllers;

public sealed class CheckPaginationController
{
    private readonly CheckPaginationService _checkPaginationService;

    public CheckPaginationController(CheckPaginationService checkPaginationService)
    {
        _checkPaginationService = checkPaginationService;
    }

    public ProvincesDto Invoke()
    {
        return _checkPaginationService.Invoke();
    }
}