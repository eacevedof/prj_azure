using System;
using azure_one.Etl.RawLoaders.Domain.Entities;
using azure_one.Etl.Shared.Infrastructure.Db;
using azure_one.Etl.Shared.Infrastructure.Db.QueryBuilders;
using azure_one.Etl.Shared.Infrastructure.Files;
using azure_one.Etl.Shared.Infrastructure.Log;

namespace azure_one.Etl.RawLoaders.Application.ImpTables;

public sealed class LoadXlsCountriesServices: AbsRawService
{
    public override void Invoke()
    {
        ExcelMapper excelMapper = ExcelMapper.GetInstance("000200_countries");
        ExcelReader excelReader = ExcelReader.FromPrimitivesSheetName((
            excelMapper.Source["path"],
            excelMapper.Source["sheet_name"], 
            Int32.Parse(excelMapper.Source["sheet_max_col"])
        ));
        
        string sql = (
            new BulkInsert(
                excelMapper.Target["table"],
                excelMapper.Mapping,
                excelReader.GetData(excelMapper.Mapping)
            )
        ).GetBulkInsertQuery();
        
        sql = ChangeDatabaseByReq(sql);
        Lg.pr(sql);
        Mssql.GetInstanceByReq().Execute(sql);
    }
}