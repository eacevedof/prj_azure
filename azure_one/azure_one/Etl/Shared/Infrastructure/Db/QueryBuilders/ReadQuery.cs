using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Extensions;

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
            if (IsReserved(mxFields))
                mxFields = $"[{mxFields}]";
            return;
        }

        if (mxFields is List<string>)
        {
            for (var i = 0; i < mxFields.Count; i++)
            {
                var field = mxFields[i];
                if (IsReserved(field))
                    mxFields[i] = $"[{field}]";
            }
        }
    }

     public ReadQuery Select(string table = null, string[] fields = null, string[] arpks = null)
    {
        sql = "/*error select*/";
        sqlCount = "/*error selectcount*/";

        if (table == null)
            table = this.table;

        if (table == null)
            throw new Exception("missing table in select");

        fields = fields ?? arGetFields;

        if (fields.Length == 0)
            throw new Exception("missing fields in select");

        string comment = this.comment != null ? $"/*{this.comment}*/" : "/*select*/";
        arpks = arpks ?? arPKs;

        select.Add($"{comment} SELECT");
        if (isDistinct)
            select.Add("DISTINCT");

        CleanReserved(fields);

        select.Add(string.Join(",", fields));
        select.Add($"FROM {table}");

        select.Add(GetJoins());

        var arconds = GetPkConds(arpks);
        var araux = arconds.Concat(arAnds);
        if (araux.Any())
            select.Add($"WHERE {string.Join(" AND ", araux)}");

        select.Add(GetGroupBy());
        select.Add(GetHaving());
        select.Add(GetOrderBy());
        select.Add(GetEnd());
        select.Add(GetLimit());

        RemoveEmptyItem(select);

        sql = string.Join(" ", select);
        sqlcount = "";

        if (calcfoundrows)
            sqlcount = GetSqlCount();

        return this;
    }
}