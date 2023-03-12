using System;
using azure_one.Etl.RawLoaders.Domain.Entities;
using azure_one.Etl.Shared.Infrastructure.Files;
using azure_one.Etl.Shared.Infrastructure.Log;
using azure_one.Etl.Shared.Infrastructure.Db;
using azure_one.Etl.Shared.Infrastructure.Db.QueryBuilders;


namespace azure_one.Etl.RawLoaders.Application.Force;

public sealed class LoadXlsForceService: AbsRawService
{
    public override void Invoke()
    {
        ExcelMapper excelMapper = ExcelMapper.GetForceInstance("000100_force");
        ExcelReader excelReader = ExcelReader.FromPrimitivesSheetName((
            excelMapper.Source["path"],
            excelMapper.Source["sheet_name"], 
            Int32.Parse(excelMapper.Source["sheet_max_col"])
        ));
        
        string sql = (
            new BulkInsertForce(
                excelMapper.Target["table"],
                excelReader.GetData()
            )
        ).GetBulkInsertForceQuery();
        
        Lg.pr(sql);
        Mssql.GetInstanceByReq().Execute(sql);
    }
}