using System;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Threading;

using EtlPowerBi.PowerBiShared.Infrastructure.Global;
using EtlPowerBi.PowerBiShared.Infrastructure.Db.Contexts;
using EtlPowerBi.PowerBiShared.Infrastructure.Log;

namespace EtlPowerBi.PowerBiShared.Infrastructure.Db
{
    public sealed class Mssql
    {
        private readonly string _stringConnection;
        private readonly SqlConnection _connection;

        private int _rowsAffected;
        private int _lastInsertId;

        public Mssql(ContextDto contextDto)
        {
            _stringConnection = (new DbConnectionString(contextDto)).GetStringOrFail();
            Console.WriteLine($"Mssql.stringConnection: {_stringConnection}");
            _connection = new SqlConnection(_stringConnection);
        }

        public static Mssql GetPowerBiInstanceByEnv()
        {
            var contextDto = ContextFinder.GetPowerBiInstanceByAppEnv();
            return new Mssql(contextDto);
        }

        public static Mssql GetInstanceByReqTenantContextId()
        {
            var contextDto = ContextFinder.GetTenantDbContextByPowerBiId(Req.TenantContextId);
            return new Mssql(contextDto);
        }

        public static Mssql GetInstanceByContextDto(ContextDto contextDto)
        {
            return new Mssql(contextDto);
        }


        public List<Dictionary<string, string>> Query(string query)
        {
            query = query.Trim();
            List<Dictionary<string, string>> rowsResult = new List<Dictionary<string, string>>();
            if (query == "") return rowsResult;

            SqlCommand cmdSql = new SqlCommand(query, _connection);
            cmdSql.CommandTimeout = 120;
            if (_connection.State == ConnectionState.Closed) _connection.Open();

            using (SqlDataReader sqlReader = cmdSql.ExecuteReader())
            {
                List<string> columnsNames = GetColumnsNames(sqlReader);
                while (sqlReader.Read())
                {
                    Dictionary<string, string> dicRow = columnsNames
                        .ToDictionary(
                            columnName => columnName,
                            columnName => sqlReader[columnName] == DBNull.Value ? null : sqlReader[columnName].ToString()
                        );
                    rowsResult.Add(dicRow);
                }
                sqlReader.Close();
            }
            CloseConnection();
            return rowsResult;
        }

        public bool Execute(string query)
        {
            query = query.Trim();
            if (query == "")
                return false;

            bool isInsert = query.Contains("INSERT INTO ");
            if (isInsert) query += ";SELECT SCOPE_IDENTITY();";

            SqlCommand cmdSql = new SqlCommand(query, _connection);
            if (_connection.State == ConnectionState.Closed) _connection.Open();

            using (cmdSql)
            {
                if (isInsert)
                    _lastInsertId = Convert.ToInt16(cmdSql.ExecuteScalar());
                else
                    _rowsAffected = cmdSql.ExecuteNonQuery();
            }
            CloseConnection();
            return true;
        }

        public void ExecuteRaw(string query)
        {
            query = query.Trim();
            if (query == "")
                return;

            SqlCommand cmdSql = new SqlCommand(query, _connection);
            if (_connection.State == ConnectionState.Closed) _connection.Open();

            using (cmdSql) cmdSql.ExecuteNonQuery();
            CloseConnection();
        }

        public void ExecuteBulkInsert(string bulkInsertSql)
        {
            Console.WriteLine($"ExecuteBulkInsert start");
            string[] insertQueries = bulkInsertSql.Split("/*end chunk*/");
            if (insertQueries.Length == 0)
                return;
            Console.WriteLine($"ExecuteBulkInsert total inserts: {insertQueries.Length}");
            int index = 0;
            foreach (string query in insertQueries)
            {
                Console.WriteLine($"ExecuteBulkInsert insert index {index}");
                try
                {
                    SqlCommand cmdSql = new SqlCommand(query, _connection);
                    cmdSql.CommandTimeout = 60;
                    if (_connection.State == ConnectionState.Closed) _connection.Open();
                    using (cmdSql)
                        cmdSql.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    Lg.error(e.Message, $"ExecuteBulkInsert insert index {index}");
                }
                index++;
            }
            CloseConnection();
        }

        public void ExecuteBulkInsertFromDataTable(DataTable dataTable)
        {
            //to-do habría que pasar el mapping para que sea más flexible
            //actualmente fuerza que la query en origen tenga los alias de la tabla destino
            var conx = new SqlConnection(_stringConnection);
            conx.Open();
            using (conx)
            {
                using (SqlBulkCopy bulkCopy = new SqlBulkCopy(conx))
                {
                    bulkCopy.DestinationTableName = dataTable.TableName;
                    _AddBulkMappingsFromDataTable(bulkCopy, dataTable);
                    bulkCopy.WriteToServer(dataTable);
                }
            }
            conx.Close();
        }

        private void _AddBulkMappingsFromDataTable(SqlBulkCopy bulkCopy, DataTable dataTable)
        {
            var columns = _GetColumnNames(dataTable);
            foreach (string columnName in columns)
                bulkCopy.ColumnMappings.Add(new SqlBulkCopyColumnMapping(columnName, columnName));
        }

        private string[] _GetColumnNames(DataTable dataTable)
        {
            string[] columnNames = new string[dataTable.Columns.Count];
            for (int i = 0; i < dataTable.Columns.Count; i++)
            {
                columnNames[i] = dataTable.Columns[i].ColumnName;
            }
            return columnNames;
        }

        public int GetRowsAffected()
        {
            return _rowsAffected;
        }

        public int GetLastInsertId()
        {
            return _lastInsertId;
        }

        private List<string> GetColumnsNames(SqlDataReader sqlReader)
        {
            List<string> columnNames = new List<string>();
            for (int i = 0; i < sqlReader.FieldCount; i++)
            {
                columnNames.Add(sqlReader.GetName(i));
            }
            return columnNames;
        }

        private void CloseConnection()
        {
            if (_connection.State == ConnectionState.Closed)
                return;
            
            if (_connection.State == ConnectionState.Open)
                _connection.Close();
        }
    }
}

