using azure_one.Etl.RawLoaders.Domain.Entities;
using azure_one.Etl.Shared.Infrastructure.Files;
using azure_one.Etl.Shared.Infrastructure.Env;
using azure_one.Etl.Shared.Infrastructure.Log;
using azure_one.Etl.Shared.Infrastructure.Db;
using azure_one.Etl.Shared.Infrastructure.Db.QueryBuilders;

namespace azure_one.Etl.RawLoaders.Application;

public sealed class LoadXlsCompaniesService: AbsRawService
{
    public override void Invoke()
    {
        ImpCompaniesEntity companiesEntity = ImpCompaniesEntity.GetInstance();
        string pathExcel = Env.GetConcat("HOME", companiesEntity.PathXls);
        
        ExcelReader excelReader = ExcelReader.FromPrimitives((
            pathExcel, 
            companiesEntity.SheetNr, 
            companiesEntity.SheetMaxColumn
        ));
        
        string sql = (
            new BulkInsert(
                companiesEntity.Table,
                companiesEntity.ColumnMapping,
                excelReader.GetData()
            )
        ).GetBulkInsertQuery();
        
        Lg.pr(sql);
        Mssql.GetInstance().Execute(sql);
    }
}