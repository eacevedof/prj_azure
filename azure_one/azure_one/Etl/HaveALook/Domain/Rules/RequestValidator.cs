
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
            HaveALookException.FailIfMissingPerPage();

        value = query["per_page"];
        if (!IsInteger(value))
            HaveALookException.FailIfPerPageIsNotInteger();

        int ivalue = int.Parse(query["page"]);
        if (ivalue < 1)
            HaveALookException.FailIfPageIsLowerThanOne();

        ivalue = int.Parse(query["per_page"]);
        if (ivalue < 1)
            HaveALookException.FailIfPerPageIsLowerThanOne();

    }

    private bool IsInteger(string value)
    {
        return int.TryParse(value, out int result);
    }
}