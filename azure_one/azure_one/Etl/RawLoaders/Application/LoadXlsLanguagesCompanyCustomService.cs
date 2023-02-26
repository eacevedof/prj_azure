using azure_one.Etl.RawLoaders.Domain.Entities;
using azure_one.Etl.Shared.Infrastructure.Files;
using azure_one.Etl.Shared.Infrastructure.Env;
using azure_one.Etl.Shared.Infrastructure.Log;
using azure_one.Etl.Shared.Infrastructure.Db;
using azure_one.Etl.Shared.Infrastructure.Db.QueryBuilders;

namespace azure_one.Etl.RawLoaders.Application;

public sealed class LoadXlsLanguagesCompanyCustomService: AbsRawService
{
    public override void Invoke()
    {
        ImpLanguagesCompanyCustomEntity languageCompanyCustomEntity = ImpLanguagesCompanyCustomEntity.GetInstance();
        ExcelReader excelReader = ExcelReader.FromPrimitives((
            Env.GetConcat("HOME", languageCompanyCustomEntity.PathXls),
            languageCompanyCustomEntity.SheetNr, 
            languageCompanyCustomEntity.SheetMaxColumn
        ));
        
        string sql = (
            new BulkInsert(
                languageCompanyCustomEntity.Table,
                languageCompanyCustomEntity.ColumnMapping,
                excelReader.GetData()
            )
        ).GetBulkInsertQuery();
        
        Lg.pr(sql);
        Mssql.GetInstance().Execute(sql);
    }
}