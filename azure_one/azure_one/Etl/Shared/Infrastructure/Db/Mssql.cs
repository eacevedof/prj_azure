using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;


namespace azure_one.Etl.Shared.Infrastructure.Db
{
	public sealed class Mssql
	{
		private readonly StructDb1 _dbConfig1;
		private readonly SqlConnection _connection;
		private int _rowsAffected;
		private int _lastInsertId;
		
		public Mssql()
		{
			_dbConfig1 = new StructDb1();
			_connection = new SqlConnection(_dbConfig1.GetConnectionString());
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
			if (_connection.State == ConnectionState.Closed) Open();

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
			if (_connection.State == ConnectionState.Closed) Open();
			
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
			if (_connection.State == ConnectionState.Closed) Open();
			
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
		
		private void Open()
		{
			_connection.Open();
		}
		private void Close()
		{
			_connection.Close();
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

