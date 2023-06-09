
using System;
using Microsoft.AspNetCore.Http;
using azure_one.Etl.HaveALook.Domain.Exceptions;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Extensions;
using System.Linq;

namespace azure_one.Etl.HaveALook.Domain.Rules;

public sealed class RequestValidator
{
    private IQueryCollection query;

    public void Invoke(IQueryCollection httpQuery)
    {
        query = httpQuery;
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

        FailIfNotValidOrderBy();   
     
    }

    private void FailIfNotValidOrderBy()
    {
        if (!query.ContainsKey("order_by"))
            return;

        string orderBy = query["order_by"];
        if (orderBy.IsNullOrWhiteSpace())
            return;

        if (orderBy != "ASC" && orderBy != "DESC")
            HaveALookException.FailIfOrderByIsNotAscOrDesc();
    }

    private void FailIfOrderColumnIsNotValid()
    {
        if (!query.ContainsKey("order_column"))
            return;

        string orderBy = query["order_column"];
        if (orderBy.IsNullOrWhiteSpace())
            return;

        
    }

    private bool inArray(string value, string[] values)
    {
        return values.Contains(value);
    }


    private bool IsInteger(string value)
    {
        return int.TryParse(value, out int result);
    }
}