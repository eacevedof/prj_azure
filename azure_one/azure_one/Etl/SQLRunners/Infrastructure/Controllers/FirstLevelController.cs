using azure_one.Etl.SQLRunners.Application;

namespace azure_one.Etl.SQLRunners.Infrastructure.Controllers;

public sealed class FirstLevelController
{
    private readonly TransformDemoService _transformDemoService;
    
    public FirstLevelController(TransformDemoService transformDemoService)
    //public FirstLevelController()
    {
        _transformDemoService = transformDemoService;
    }

    public void Invoke()
    {
        _transformDemoService.Invoke();
    }
}