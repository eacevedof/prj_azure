using azure_one.Etl.RawLoaders.Domain.Entities;
using azure_one.Etl.Shared.Infrastructure.Files;
using azure_one.Etl.Shared.Infrastructure.Env;
using azure_one.Etl.Shared.Infrastructure.Log;
using azure_one.Etl.Shared.Infrastructure.Db;
using azure_one.Etl.Shared.Infrastructure.Db.QueryBuilders;

namespace azure_one.Etl.RawLoaders.Application;

public sealed class LoadXlsCitiesService: AbsRawService
{
    public override void Invoke()
    {
        dynamic config = MappingReader.JsonDecode("cities");
        ImpCitiesEntity citiesEntity = ImpCitiesEntity.GetInstance();
        //string pathExcel = Env.GetConcat("HOME", citiesEntity.PathXls);

        string pathHome = Env.Get("HOME");
        string pathExcel = config.source.path.Replace("%in_folder%","/Downloads");
        pathExcel = $"{pathHome}/{pathExcel}";
        
        ExcelReader excelReader = ExcelReader.FromPrimitives((
            pathExcel, 
            config.source.sheet_nr, 
            config.sheet_max_col
        ));
        
        string sql = (
            new BulkInsert(
                citiesEntity.Table,
                citiesEntity.ColumnMapping,
                excelReader.GetData()
            )
        ).GetBulkInsertQuery();
        
        Lg.pr(sql);
        Mssql.GetInstance().Execute(sql);
    }
}