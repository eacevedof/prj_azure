using System.ComponentModel;

namespace azure_one.Etl.RawLoaders.Domain.Enums;

public abstract class ExcelSheetsEnum
{
    public const string path_file = "/Downloads/data-in.xlsx";
    
    public const string languages_table = "imp_languages";
    public const int languages_sheetnr = 1;
    public const int languages_max_col = 3;

    public const string countries_table = "imp_countries";
    public const int countries_sheetnr = 2;
    public const int countries_max_col = 3;
    
    public const string provinces_table = "imp_provinces";
    public const int provinces_sheetnr = 2;
    public const int provinces_max_col = 3;
}