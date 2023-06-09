
using System;
using Microsoft.AspNetCore.Http;
using azure_one.Etl.HaveALook.Domain.Exceptions;

namespace azure_one.Etl.HaveALook.Domain.Rules;

public sealed class RequestValidator
{   
    public void Invoke(IQueryCollection httpQuery)
    {
        IQueryCollection query = httpQuery;
        if (!query.ContainsKey("page"))
            HaveALookException.FailIfMissingPage();

        string value = query["page"];
        if (!IsInteger(value))
            HaveALookException.FailIfPageIsNotInteger();
        
        if (!query.ContainsKey("per_page"))
            throw new Exception("missing per_page");

        value = query["per_page"];
        if (!IsInteger(value))
            throw new Exception("per_page must be integer");
        
        
        /*
      int page = int.Parse(req.Query["page"]);
                int perPage = int.Parse(req.Query["per_page"]);
                string search = req.Query["search"];
                string orderBy = req.Query["order_by"];
                string orderColumn = req.Query["order_column"];        
        */
    }

    private bool IsInteger(string value)
    {
        return int.TryParse(value, out int result);
    }
}