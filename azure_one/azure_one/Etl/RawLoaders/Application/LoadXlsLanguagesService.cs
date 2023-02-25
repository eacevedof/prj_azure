using azure_one.Etl.RawLoaders.Domain.Entities;
using azure_one.Etl.Shared.Infrastructure.Files;
using azure_one.Etl.Shared.Infrastructure.Env;
using azure_one.Etl.Shared.Infrastructure.Log;
using azure_one.Etl.Shared.Infrastructure.Db;
using azure_one.Etl.Shared.Infrastructure.Db.QueryBuilders;

namespace azure_one.Etl.RawLoaders.Application;

public sealed class LoadXlsLanguagesService: AbsRawService
{
    public override void Invoke()
    {
        ImpLanguagesEntity language = ImpLanguagesEntity.GetInstance();
        string pathExcel = Env.GetConcat("HOME", language.PathXls);
        
        ExcelReader excelReader = ExcelReader.FromPrimitives((
            pathExcel, 
            language.SheetNr, 
            language.SheetMaxColumn
        ));
        
        string sql = (
            new BulkInsert(
                language.Table,
                language.ColumnMapping,
                excelReader.GetData()
            )
        ).GetBulkInsertQuery();
        
        Lg.pr(sql);
        Mssql.GetInstance().Execute(sql);
    }
}