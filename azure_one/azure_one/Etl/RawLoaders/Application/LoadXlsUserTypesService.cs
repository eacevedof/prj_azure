using azure_one.Etl.RawLoaders.Domain.Entities;
using azure_one.Etl.Shared.Infrastructure.Files;
using azure_one.Etl.Shared.Infrastructure.Env;
using azure_one.Etl.Shared.Infrastructure.Log;
using azure_one.Etl.Shared.Infrastructure.Db;
using azure_one.Etl.Shared.Infrastructure.Db.QueryBuilders;

namespace azure_one.Etl.RawLoaders.Application;

public sealed class LoadXlsUserTypesService: AbsRawService
{
    public override void Invoke()
    {
        ImpUserTypesEntity ImpUserTypesEntity = ImpUserTypesEntity.GetInstance();
        ExcelReader excelReader = ExcelReader.FromPrimitives((
            Env.GetConcat("HOME", ImpUserTypesEntity.PathXls), 
            ImpUserTypesEntity.SheetNr, 
            ImpUserTypesEntity.SheetMaxColumn
        ));
        
        string sql = (
            new BulkInsert(
                ImpUserTypesEntity.Table, 
                ImpUserTypesEntity.ColumnMapping, 
                excelReader.GetData()
            )
        ).GetBulkInsertQuery();
        Lg.pr(sql);
        Mssql.GetInstance().Execute(sql);
    }
}