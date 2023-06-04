using System;
using System.Collections.Generic;
using System.Linq;

namespace azure_one.Etl.Shared.Infrastructure.Db.QueryBuilders;

public sealed class ReadQuery
{
    
    private string comment = "";
    private string table = "";
    private bool isDistinct = false;
    private bool calcFoundRows = false;
    private List<string> arGetFields = new List<string>();
    private List<string> arJoins = new List<string>();
    private List<string> arAnds = new List<string>();
    private List<string> arGroupBy = new List<string>();
    private List<string> arHaving = new List<string>();
    private Dictionary<string, string> arOrderBy = new Dictionary<string, string>();
    private Dictionary<string, int> arLimit = new Dictionary<string, int>();
    private List<string> arEnd = new List<string>();

    private List<string> arNumeric = new List<string>();
    private Dictionary<string, string> arInsertFv = new Dictionary<string, string>();

    private Dictionary<string, string> arUpdateFv = new Dictionary<string, string>();
    private Dictionary<string, string> arPKs = new Dictionary<string, string>();
    private List<string> select = new List<string>();

    private string sql = "";
    private string sqlCount = "";

    private object oDB = null;

    private List<string> reserved = new List<string> { "get", "order", "password" };

    public const string READ = "r";
    public const string WRITE = "w";

    public ReadQuery(string table)
    {
        this.table = table;
    }

    public static ReadQuery fromTable(string table)
    {
        return (new(table));
    }
    
    private string GetJoins()
    {
        if (!arJoins.Any())
            return "";
        return string.Join("\n", arJoins);
    }

    private string GetHaving()
    {
        if (!arHaving.Any())
            return "";
        return "HAVING " + string.Join(", ", arHaving);
    }

    private string GetGroupBy()
    {
        if (!arGroupBy.Any())
            return "";
        return "GROUP BY " + string.Join(",", arGroupBy);
    }

    private string GetOrderBy()
    {
        if (!arOrderBy.Any())
            return "";
        return "ORDER BY " + string.Join(",", arOrderBy.Select(kvp => $"{kvp.Key} {kvp.Value}"));
    }

    private string GetEnd()
    {
        if (!arEnd.Any())
            return "";
        return " " + string.Join("\n", arEnd);
    }

    private string GetLimit()
    {
        if (!arLimit.Any())
            return "";
        if (arLimit["regfrom"] == 0)
            return " LIMIT " + arLimit["perpage"];
        var limit = string.Join(", ", arLimit.Values);
        return " LIMIT " + limit;
    }

    private bool IsNumeric(string fieldName)
    {
        return arNumeric.Contains(fieldName);
    }

    private bool IsReserved(string word)
    {
        return reserved.Contains(word.ToLower());
    }

    private void CleanReserved(object mxFields)
    {
        if (mxFields is string)
        {
            if (IsReserved(mxFields.ToString()))
                mxFields = $"[{mxFields}]";
            return;
        }

        if (mxFields is List<string>)
        {
            List<string> temp = mxFields as List<string>;
            for (var i = 0; i < temp.Count; i++)
            {
                var field = temp[i];
                if (IsReserved(field))
                    temp[i] = $"[{field}]";
            }
            mxFields = temp;
        }
    }

    
}