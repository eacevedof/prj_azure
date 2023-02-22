using azure_one.Etl.Infrastructure.Db;
using azure_one.Etl.Infrastructure.Log;
using azure_one.Etl.Infrastructure.Generators;
using System.Collections.Generic;

namespace azure_one.Etl.Infrastructure.Repositories;

public sealed class UsersRepository: AbsRepository
{
    public UsersRepository(Mssql db) : base(db)
    {
    }

    public void printAllUsers()
    {
        string sql = "SELECT * FROM users";
        List<Dictionary<string, string>> users = this._db.Query(sql);
        Lg.PrRows(users);
    }
    
    public bool randomInsert()
    {
        string userCode = Rnd.Number(100, 10000).ToString();
        string domain = Rnd.Text(8);
        string sqlInsert = @$"
        INSERT INTO users (user_types_id, tenant_id, user_code, name, email, password) 
        values (1,1,'{userCode}','eaf','eaf@{domain}.com','1234')
        ";
        
        bool r = this._db.Execute(sqlInsert);
        if (!r)
        {
            Lg.Pr("no se ha insertado");
        }
        else
        {
            Lg.Pr($"id obtenido {this._db.GetLastInsertId()}");
        }

        return r;
    }
}