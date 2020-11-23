using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace Avto_deli
{
    class Database
    {
        public static SQLiteConnection con;

        public void Start_Database()
        {
            con = new SQLiteConnection("Data Source=./Data/Data.db; Version = 3");
            con.Open();
        }

        public void Stop_Database()
        {
            con.Close();
        }

        public void Create_Login_base()
        {
            using (SQLiteCommand cmd = new SQLiteCommand(con))
            {
                cmd.CommandText = "DROP TABLE IF EXISTS Registrirani";
                cmd.ExecuteNonQuery();

                cmd.CommandText = @"CREATE TABLE Registrirani (ID INTEGER PRIMARY KEY, Username TEXT, Password TEXT);"; //Intiger primary key avtomatsko naredi da se poveca za 1
                cmd.ExecuteNonQuery();
            }
        }


    }
}
