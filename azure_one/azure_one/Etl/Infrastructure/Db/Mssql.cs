using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;

namespace azure_one.Etl.Infrastructure.Db
{
	public class Mssql
	{
		private readonly StructDb1 _db1;
		private readonly SqlConnection _connection;
		
		public Mssql()
		{
			this._db1 = new StructDb1();
			this._connection = new SqlConnection(this._db1.GetConnectionString());
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
			List<object> result = new List<object>();
			if (query == "")
			{
				return result;
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
				for (int i = 0; i < columnsNames.Count; i++)
				{
					
				}
			}
			sqlReader.Close();
			this._connection.Close();
			return result;
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

