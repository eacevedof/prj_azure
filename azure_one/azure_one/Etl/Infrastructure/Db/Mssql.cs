using Microsoft.Extensions.Logging;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;

namespace azure_one.Etl.Infrastructure.Db
{
	public class Mssql
	{
		private readonly StructDb1 _dbConfig1;
		private readonly SqlConnection _connection;
		
		public Mssql()
		{
			this._dbConfig1 = new StructDb1();
			this._connection = new SqlConnection(this._dbConfig1.GetConnectionString());
		}

		private void Open()
		{
			this._connection.Open();
		}
		
		private void Close()
		{
			this._connection.Close();
		}

		public List<object> Query(string query)
		{
			query = query.Trim();
			List<object> rowsResult = new List<object>();
			if (query == "")
			{
				return rowsResult;
			}

			SqlCommand sql = new SqlCommand(query, this._connection);
			if (this._connection.State == ConnectionState.Closed)
			{
				this.Open();
			}

			SqlDataReader sqlReader = sql.ExecuteReader();
			List<string> columnsNames = this.GetColumnsNames(sqlReader);
			while (sqlReader.Read())
			{
				List<object> objRow = new List<object>();
				for (int i = 0; i < columnsNames.Count; i++)
				{
					dynamic objColumn = new ExpandoObject();
					objColumn.position = i;
					objColumn.column = columnsNames[i];
					objColumn.value = sqlReader.GetValue(i).ToString();
					objRow.Add(objColumn);
				}
				rowsResult.Add(objRow);
			}
			sqlReader.Close();
			this._connection.Close();
			return rowsResult;
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

