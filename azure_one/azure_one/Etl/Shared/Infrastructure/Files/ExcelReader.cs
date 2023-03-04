using System.Collections.Generic;
using ExcelDataReader;
using System.Data;
using System.IO;

using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Extensions;
using azure_one.Etl.RawLoaders.Domain.Exceptions;

namespace azure_one.Etl.Shared.Infrastructure.Files;

public sealed class ExcelReader
{
    private readonly string _pathToFile;
    private readonly int _sheetNr = 0;
    private readonly string _sheetName = "";
    private readonly int _maxColumn = -1;

    private ExcelReader(string pathToFile, int sheetNr=0, int maxColumn=-1)
    {
        _pathToFile = pathToFile;
        _sheetNr = sheetNr;
        _maxColumn = maxColumn;
    }
    
    private ExcelReader(string pathToFile, string sheetName="", int maxColumn=-1)
    {
        _pathToFile = pathToFile;
        _sheetName = sheetName;
        _maxColumn = maxColumn;
    }

    public static ExcelReader FromPrimitives((string, int, int) primitives)
    {
        return new ExcelReader(primitives.Item1, primitives.Item2, primitives.Item3);
    }
    
    public static ExcelReader FromPrimitivesSheetName((string, string, int) primitives)
    {
        return new ExcelReader(primitives.Item1, primitives.Item2, primitives.Item3);
    }
    
    public List<Dictionary<string, string>> GetData()
    {
        List<Dictionary<string, string>> sheetData = new();
        // Lees het Excel-bestand
        using (var stream = File.Open(_pathToFile, FileMode.Open, FileAccess.Read))
        {
            using (var excelDataReader = ExcelReaderFactory.CreateReader(stream))
            {
                // Haal het eerste werkblad op
                excelDataReader.Read();
                var dataSet = excelDataReader.AsDataSet();
                var sheets = dataSet.Tables;
                
                var sheet = dataSet.Tables[_sheetNr];
                if (!_sheetName.Trim().IsEmpty())
                    sheet = GetSheetObjectByName(sheets, _sheetName);

                if (sheet is null)
                    throw new SheetNotFoundException($"sheet {_sheetName} was not found!");

                DataRowCollection sheetRows = sheet.Rows;
                if (sheetRows.Count == 0)
                    return sheetData;

                // Loop door de rijen van het werkblad
                foreach (DataRow row in sheetRows)
                {
                    // Maak een dictionary voor elke rij
                    Dictionary<string,string> rowData = new();
                    // Loop door de kolommen van de rij
                    for (int i = 0; i < sheet.Columns.Count; i++)
                    {
                        if (_maxColumn>-1 && i>_maxColumn) continue;
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
    
    public List<Dictionary<string, string>> GetData(Dictionary<string, string> mapping)
    {
        List<Dictionary<string, string>> sheetData = new ();
        
        using (FileStream stream = File.Open(_pathToFile, FileMode.Open, FileAccess.Read))
        {
            using (IExcelDataReader excelDataReader = ExcelReaderFactory.CreateReader(stream))
            {
                excelDataReader.Read();
                DataSet dataSet = excelDataReader.AsDataSet();
                DataTableCollection sheets = dataSet.Tables;
                
                DataTable sheet = dataSet.Tables[_sheetNr];
                if (!_sheetName.Trim().IsEmpty())
                    sheet = GetSheetObjectByName(sheets, _sheetName);

                if (sheet is null)
                    throw new SheetNotFoundException($"sheet {_sheetName} was not found!");

                DataRowCollection sheetRows = sheet.Rows;
                if (sheetRows.Count == 0)
                    return sheetData;

                DataRow titleRow = sheetRows[0];
                Dictionary<string, int> columnPositions = GetColumnNames(titleRow);

                foreach (DataRow row in sheetRows)
                {
                    DataColumnCollection columns = sheet.Columns;
                    int numColumns = columns.Count;
                    if (numColumns==0) continue;
                    
                    Dictionary<string, string> rowData = new();
                    foreach (KeyValuePair<string,string> fromTo in mapping)
                    {
                        string columnFrom = fromTo.Key;
                        int colPosition = GetColumPositionByColumnName(columnPositions, columnFrom);
                        rowData.Add(columnFrom, row[colPosition].ToString().Trim());
                    }
                    sheetData.Add(rowData);
                }
            }// using.createReader
        }// using file.open
        return sheetData;
    }
    
    private Dictionary<string, int> GetColumnNames(DataRow titleRow)
    {
        Dictionary<string, int> colNames = new();
        int numCols = titleRow.Table.Columns.Count;
        for (int i = 0; i < numCols; i++)
        {
            if (i>_maxColumn) continue;
            string colName = titleRow[i].ToString().Trim();
            if (colNames.ContainsKey(colName)) continue;
            colNames.Add(colName, i);
        }
        return colNames;
    }

    private int GetColumPositionByColumnName(Dictionary<string, int> columnNames, string columnName)
    {
        if (columnNames.Count == 0) return 0;
        foreach (KeyValuePair<string,int> position in columnNames)
            if (position.Key == columnName)
                return position.Value;
        return 0;
    }

    private DataTable GetSheetObjectByName(DataTableCollection sheets, string sheetName)
    {
        foreach (DataTable sheet in sheets)
            if (sheet.TableName == sheetName)
                return sheet;
        return null;
    }
}