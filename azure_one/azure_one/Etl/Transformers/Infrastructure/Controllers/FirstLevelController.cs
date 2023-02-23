using azure_one.Etl.Transformers.Application;

namespace azure_one.Etl.Transformers.Infrastructure.Controllers;

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
        //_transformDemoService.Invoke();
    }
}