using System.Collections.Generic;
using azure_one.Etl.Shared.Infrastructure.Files;
using azure_one.Etl.Shared.Infrastructure.Env;
using azure_one.Etl.Shared.Infrastructure.Log;
using azure_one.Etl.Shared.Infrastructure.Db;
using azure_one.Etl.Shared.Infrastructure.Db.QueryBuilders;
using azure_one.Etl.RawLoaders.Domain.Enums;

namespace azure_one.Etl.RawLoaders.Application;

public sealed class TruncateTableService: AbsRawService
{
    public override void Invoke()
    {

    }
}