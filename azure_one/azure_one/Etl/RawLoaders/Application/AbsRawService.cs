using System.Collections.Generic;
using azure_one.Etl.Shared.Infrastructure.Files;

namespace azure_one.Etl.RawLoaders.Application;

public abstract class AbsRawService
{
    public abstract void Invoke();
}