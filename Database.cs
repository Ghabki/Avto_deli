using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace Avto_deli
{
    public class Database
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

        public List<string> Vrni_Username()
        {
            List<string> vrni = new List<string>();
            using (SQLiteCommand cmd = new SQLiteCommand(@"SELECT Username FROM Registrirani;", con))
            {
                using (SQLiteDataReader rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        vrni.Add(rdr.GetString(0));
                        
                    }
                    
                }
            }
            return vrni;

        }

        public void Dodaj_Uporabnika(string ime, string geslo)
        {
            using (SQLiteCommand cmd = new SQLiteCommand(con))
            {
                cmd.CommandText = @"INSERT INTO Registrirani (Username, Password) VALUES (@Ime, @Geslo);";
                cmd.Parameters.AddWithValue("@Ime", ime);
                cmd.Parameters.AddWithValue("@Geslo", geslo);
                cmd.Prepare();
                cmd.ExecuteNonQuery();
            }
        }


        public bool Preveri_ime_geslo(string ime, string geslo)
        {
            string pass = "@dminPassword";
            List<string> check = Vrni_Username();
            if (check.Contains(ime))
            {
                
                using (SQLiteCommand cmd = new SQLiteCommand(@"SELECT Password FROM Registrirani WHERE Username = (@ime);", con))
                {
                    cmd.Parameters.AddWithValue("@Ime", ime);
                    using (SQLiteDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            pass = rdr.GetString(0); 
                        }
                    }

                }
                if (geslo.Equals(pass))
                {
                    return true;
                }
            }
            return false;
        }











        public SQLiteConnection Povezava_db
        {
            get{ return con;}
        }


    }
}
