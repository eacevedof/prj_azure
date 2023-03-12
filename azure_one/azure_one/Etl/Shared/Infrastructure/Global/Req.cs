using System.Collections.Generic;
using azure_one.Etl.Shared.Infrastructure.Db.Contexts;

namespace azure_one.Etl.Shared.Infrastructure.Global;

public static class Req
{
    public static Dictionary<string, string> Request = new();
    public static ContextsEnum ContextId;
    
}