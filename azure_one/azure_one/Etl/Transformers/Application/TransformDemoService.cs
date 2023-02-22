using azure_one.Etl.Shared.Infrastructure.Files;

namespace azure_one.Etl.Transformers.Application;

public sealed class TransformDemoService
{
    public void Invoke()
    {
        string pathFile = FileHelper.GetCurrentPath();
        pathFile += "";
    }
}