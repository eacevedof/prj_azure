
using System;
using Microsoft.AspNetCore.Http;

namespace azure_one.Etl.HaveALook.Domain.Rules;

public sealed class RequestValidator
{   
    public void Invoke(HttpRequest httpRequest)
    {
        IQueryCollection query = httpRequest.Query;
        query.TryGetValue

        /*
      int page = int.Parse(req.Query["page"]);
                int perPage = int.Parse(req.Query["per_page"]);
                string search = req.Query["search"];
                string orderBy = req.Query["order_by"];
                string orderColumn = req.Query["order_column"];        
        */
    }
}