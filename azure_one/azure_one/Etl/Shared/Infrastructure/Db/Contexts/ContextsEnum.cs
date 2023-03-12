using System.ComponentModel;

namespace azure_one.Etl.Shared.Infrastructure.Db.Contexts;

public enum ContextsEnum
{
    [Description("local_staging")]
    local_staging,
    [Description("local_stagin2")]
    local_staging2,
}
