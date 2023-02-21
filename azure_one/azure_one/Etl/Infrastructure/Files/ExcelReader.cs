using System;
using System.Collections.Generic;
using ExcelDataReader;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System.Data;
using System.IO;


namespace azure_one.Etl.Infrastructure.Files;

public sealed class ExcelReader
{
    public List<Dictionary<string, string>> GetData(string pathToExcelFile, int iMaxColum=-1)
    {
        var sheetData = new List<Dictionary<string, string>>();
        
        // Lees het Excel-bestand
        using (var stream = File.Open(pathToExcelFile, FileMode.Open, FileAccess.Read))
        {
            using (var reader = ExcelReaderFactory.CreateReader(stream))
            {
                // Haal het eerste werkblad op
                reader.Read();
                var sheet = reader.AsDataSet().Tables[1];

                // Loop door de rijen van het werkblad
                foreach (DataRow row in sheet.Rows)
                {
                    // Maak een dictionary voor elke rij
                    var rowData = new Dictionary<string, string>();
                    // Loop door de kolommen van de rij
                    for (int i = 0; i < sheet.Columns.Count; i++)
                    {
                        if (iMaxColum>-1 && i>iMaxColum) continue;
                        // Haal de kolomnaam op
                        var columnName = sheet.Columns[i].ColumnName;
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