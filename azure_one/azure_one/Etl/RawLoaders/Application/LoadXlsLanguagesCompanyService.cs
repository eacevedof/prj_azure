using azure_one.Etl.RawLoaders.Domain.Entities;
using azure_one.Etl.Shared.Infrastructure.Files;
using azure_one.Etl.Shared.Infrastructure.Env;
using azure_one.Etl.Shared.Infrastructure.Log;
using azure_one.Etl.Shared.Infrastructure.Db;
using azure_one.Etl.Shared.Infrastructure.Db.QueryBuilders;

namespace azure_one.Etl.RawLoaders.Application;

public sealed class LoadXlsLanguagesCompanyService: AbsRawService
{
    public override void Invoke()
    {
        ImpLanguagesCompanyEntity languageCompanyEntity = ImpLanguagesCompanyEntity.GetInstance();
        ExcelReader excelReader = ExcelReader.FromPrimitives((
            Env.GetConcat("HOME", languageCompanyEntity.PathXls),
            languageCompanyEntity.SheetNr, 
            languageCompanyEntity.SheetMaxColumn
        ));
        
        string sql = (
            new BulkInsert(
                languageCompanyEntity.Table,
                languageCompanyEntity.ColumnMapping,
                excelReader.GetData()
            )
        ).GetBulkInsertQuery();
        
        Lg.pr(sql);
        Mssql.GetInstance().Execute(sql);
    }
}