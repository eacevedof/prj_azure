using System.Collections.Generic;

namespace azure_one.Etl.RawLoaders.Domain.Entities;

public abstract class AbstractEntity
{
    public string PathXls;
    
    public string Table;
    public int SheetNr;
    public int SheetMaxColumn;

    public Dictionary<string, string> ColumnMapping;
    
    //public static EntityInterface GetInstance();
}