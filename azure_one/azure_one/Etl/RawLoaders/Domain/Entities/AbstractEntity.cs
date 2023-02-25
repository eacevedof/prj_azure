using System.Collections.Generic;

namespace azure_one.Etl.RawLoaders.Domain.Entities;

public abstract class AbstractEntity
{
    public readonly string PathXls = "/Downloads/data-in.xlsx";
    
    public readonly string Table = "";
    public readonly int SheetNr = 0;
    public readonly int SheetMaxColumn = 2;

    public readonly Dictionary<string, string> ColumnMapping;
    
    //public static EntityInterface GetInstance();
}