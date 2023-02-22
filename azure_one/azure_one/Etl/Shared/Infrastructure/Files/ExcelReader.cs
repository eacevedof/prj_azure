using System.Collections.Generic;
using ExcelDataReader;
using System.Data;
using System.IO;

namespace azure_one.Etl.Infrastructure.Files;

public sealed class ExcelReader
{
    private string pathToFile;
    private int sheetNr = 0;
    private int maxColumn = -1;

    public ExcelReader(string pathToFile, int sheetNr=0, int maxColumn=-1)
    {
        this.pathToFile = pathToFile;
        this.sheetNr = sheetNr;
        this.maxColumn = maxColumn;
    }
    
    public ExcelReader(string pathToFile, int sheetNr=0)
    {
        this.pathToFile = pathToFile;
        this.sheetNr = sheetNr;
    }
    
    public ExcelReader(string pathToFile)
    {
        this.pathToFile = pathToFile;
    }

    public static ExcelReader FromPrimitives((string, int, int) primitives)
    {
        return new ExcelReader(primitives.Item1, primitives.Item2, primitives.Item3);
    }
    
    public static ExcelReader FromPrimitives((string, int) primitives)
    {
        return new ExcelReader(primitives.Item1, primitives.Item2);
    }  
    
    public static ExcelReader FromPrimitives(string pathfile)
    {
        return new ExcelReader(pathfile);
    }      
    
    public List<Dictionary<string, string>> GetData()
    {
        var sheetData = new List<Dictionary<string, string>>();
        
        // Lees het Excel-bestand
        using (var stream = File.Open(this.pathToFile, FileMode.Open, FileAccess.Read))
        {
            using (var excelDataReader = ExcelReaderFactory.CreateReader(stream))
            {
                // Haal het eerste werkblad op
                excelDataReader.Read();
                var sheet = excelDataReader.AsDataSet().Tables[this.sheetNr];
                // Loop door de rijen van het werkblad
                foreach (DataRow row in sheet.Rows)
                {
                    // Maak een dictionary voor elke rij
                    var rowData = new Dictionary<string, string>();
                    // Loop door de kolommen van de rij
                    for (int i = 0; i < sheet.Columns.Count; i++)
                    {
                        if (this.maxColumn>-1 && i>this.maxColumn) continue;
                        // Haal de kolomnaam op
                        string columnName = sheet.Columns[i].ColumnName;
                        // Haal de celwaarde op en voeg deze toe aan de dictionary
                        rowData.Add(columnName, row[i].ToString().Trim());
                    }
                    // Voeg de dictionary toe aan de lijst
                    sheetData.Add(rowData);
                }
            }
        }

        return sheetData;
    }
}