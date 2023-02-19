using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;

namespace azure_one.Etl.Infrastructure.Db
{
	public class Mssql
	{
		private readonly StructDb1 _dbConfig1;
		private readonly SqlConnection _connection;
		private Int64 rowsAffected;
		private Int64 lastInsertId;
		
		public Mssql()
		{
			this._dbConfig1 = new StructDb1();
			this._connection = new SqlConnection(this._dbConfig1.GetConnectionString());
		}

		public List<Dictionary<string, string>> Query(string query)
		{
			query = query.Trim();
			List<Dictionary<string, string>> rowsResult = new List<Dictionary<string, string>>();
			if (query == "")
			{
				return rowsResult;
			}

			SqlCommand cmdSql = new SqlCommand(query, this._connection);
			if (this._connection.State == ConnectionState.Closed)
			{
				this.Open();
			}

			SqlDataReader sqlReader = cmdSql.ExecuteReader();
			List<string> columnsNames = this.GetColumnsNames(sqlReader);
			while (sqlReader.Read())
			{
				Dictionary<string, string> dicRow = new Dictionary<string, string>();
				for (int i = 0; i < columnsNames.Count; i++)
				{
					dicRow.Add(columnsNames[i], sqlReader.GetValue(i).ToString());
				}
				rowsResult.Add(dicRow);
			}
			sqlReader.Close();
			this._connection.Close();
			return rowsResult;
		}

		public Boolean Execute(string query)
		{
			query = query.Trim();
			if (query == "")
			{
				return false;
			}

			query += ";SELECT SCOPE_IDENTITY();";
			SqlCommand cmdSql = new SqlCommand(query, this._connection);
			if (this._connection.State == ConnectionState.Closed)
			{
				this.Open();
			}			
			//string query = "INSERT INTO MijnTabel (Naam, Leeftijd) VALUES ('" + naam + "', " + leeftijd + ")";
			using (cmdSql)
			{
				this.rowsAffected = Convert.ToInt64(cmdSql.ExecuteScalar());
			}
			return true;
		}

		public Int64 GetRowsAffected()
		{
			return this.rowsAffected;
		}
		
		public Int64 GetLastInsertId()
		{
			return this.lastInsertId;
		}
		
		private void Open()
		{
			this._connection.Open();
		}
		private void Close()
		{
			this._connection.Close();
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

