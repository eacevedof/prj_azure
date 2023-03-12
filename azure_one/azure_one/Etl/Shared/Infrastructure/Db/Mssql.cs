using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;

using azure_one.Etl.Shared.Infrastructure.Global;
using azure_one.Etl.Shared.Infrastructure.Db.Contexts;

namespace azure_one.Etl.Shared.Infrastructure.Db
{
	public sealed class Mssql
	{
		private readonly string _stringConnection;
		private readonly SqlConnection _connection;
		
		private int _rowsAffected;
		private int _lastInsertId;
		
		public Mssql()
		{
			ContextDto dto = ContextFinder.GetById(Global.Global.ContextId);
			_stringConnection = (new DbConnectionString(dto)).ToString();
			_connection = new SqlConnection(_stringConnection);
		}

		public static Mssql GetInstance()
		{
			return new Mssql();
		}
		
		public List<Dictionary<string, string>> Query(string query)
		{
			query = query.Trim();
			List<Dictionary<string, string>> rowsResult = new List<Dictionary<string, string>>();
			if (query == "") return rowsResult;

			SqlCommand cmdSql = new SqlCommand(query, _connection);
			if (_connection.State == ConnectionState.Closed) _connection.Open();

			SqlDataReader sqlReader = cmdSql.ExecuteReader();
			List<string> columnsNames = GetColumnsNames(sqlReader);
			while (sqlReader.Read())
			{
				Dictionary<string, string> dicRow = new Dictionary<string, string>();
				for (int i = 0; i < columnsNames.Count; i++)
					dicRow.Add(columnsNames[i], sqlReader.GetValue(i).ToString());
				rowsResult.Add(dicRow);
			}
			sqlReader.Close();
			_connection.Close();
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
	}
}

