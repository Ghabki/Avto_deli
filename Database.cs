using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;


namespace Avto_deli
{
    public class Database
    {
        public static SQLiteConnection con;

        public void Start_Database()
        {
            try
            {
                con = new SQLiteConnection("Data Source=./Data/Data.db; Version = 3");
                con.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

        }

        public void Create_Login_base()
        {
            using (SQLiteCommand cmd = new SQLiteCommand(con))
            {
                //cmd.CommandText = "DROP TABLE IF EXISTS Registrirani";
                //cmd.ExecuteNonQuery();

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

        public void Xml_v_Bazo()
        {
            
            string[] lines = File.ReadAllLines("./Text/tekst.txt");

            foreach (string line in lines)
            {
                string[] vnesi = line.Split(';');

                using (SQLiteCommand cmd = new SQLiteCommand(con))
                {
                    cmd.CommandText = @"INSERT INTO Items (Id, Naziv, Opis, Tip_Avta, Model_avta, Cena, Kolicina ) VALUES (@Id, @Naziv, @Opis, @Tip_avta, @Model_Avta, @Cena, @Kolicina);";
                    cmd.Parameters.AddWithValue("@Id", Convert.ToInt32(vnesi[0]) );
                    cmd.Parameters.AddWithValue("@Naziv", vnesi[1]);
                    cmd.Parameters.AddWithValue("@Opis", vnesi[2]);
                    cmd.Parameters.AddWithValue("@Tip_avta", vnesi[3]);
                    cmd.Parameters.AddWithValue("@Model_Avta", vnesi[4]);
                    cmd.Parameters.AddWithValue("@Cena", Convert.ToInt32(vnesi[5]));
                    cmd.Parameters.AddWithValue("@Kolicina", Convert.ToInt32(vnesi[6]));
                    
                    cmd.Prepare();
                    cmd.ExecuteNonQuery();
                }
            }
            if (File.Exists("./Text_kanta/tekst.txt"))
            {
                File.Delete("./Text_kanta/tekst.txt");
            }
            File.Move("./Text/tekst.txt", "./Text_kanta/tekst.txt");


            //File.Replace("./Text/tekst.txt", "./Text_kanta/tekst.txt");
            //File.Move(@"./Text/tekst.txt", @"./Text_kanta/tekst"+date+".txt");
        }

        public void Db_Preveri_Table()
        {
            using (SQLiteCommand cmd = new SQLiteCommand(con))
            {
                cmd.CommandText = @"CREATE TABLE IF NOT EXISTS Items (Id INTEGER, Naziv TEXT, Opis TEXT, Tip_Avta TEXT, model_avta TEXT, Cena INTEGER, Kolicina INTEGER);"; //Intiger primary key avtomatsko naredi da se poveca za 1
                cmd.ExecuteNonQuery();
            }
        }

        public List<ListViewData> Vrni_vse()
        {
            List<ListViewData> datoteke = new List<ListViewData>();

            using (SQLiteCommand cmd = new SQLiteCommand(@"SELECT * FROM Items;", con))
            {
                using (SQLiteDataReader rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {   
                        datoteke.Add(new ListViewData() {Id = rdr.GetInt32(0), Naziv = rdr.GetString(1), Opis = rdr.GetString(2), Tip_avta= rdr.GetString(3), Model_Avta = rdr.GetString(4), Cena = rdr.GetInt32(5), Količina = rdr.GetInt32(6)}); 
                    }
                }
            }
            return datoteke;
        }

        public void Izvozi() {
            string izpis = "";
            string fil = DateTime.UtcNow.TimeOfDay.ToString().Replace(":", "");
            //Console.WriteLine(DateTime.UtcNow.TimeOfDay.ToString().Replace(".", ""));
            FileStream fs = File.Create(@"./Text_Izhod/WriteText" + fil + ".txt");
            fs.Close();

            using (SQLiteCommand cmd = new SQLiteCommand(@"SELECT * FROM Items;", con))
            {
                using (SQLiteDataReader rdr = cmd.ExecuteReader())
                {
                    StreamWriter file = new StreamWriter(@"./Text_Izhod/WriteText" + fil + ".txt");
                    while (rdr.Read())
                    {
                        izpis = rdr.GetInt32(0).ToString() +";"+ rdr.GetString(1) + ";" + rdr.GetString(2) + ";" + rdr.GetString(3) + ";" + rdr.GetString(4) + ";" + rdr.GetInt32(5).ToString() + ";" + rdr.GetInt32(6).ToString();

                        file.WriteLine(izpis);
                     }

                    file.Close();
                }
            }
        }

        public void Izbrisi(int idd)
        {
            using (SQLiteCommand cmd = new SQLiteCommand(con))
            {
                cmd.CommandText = @"DELETE FROM Items WHERE Id = (@idd);";
                cmd.Parameters.AddWithValue("@idd", idd);
                cmd.Prepare();
                cmd.ExecuteNonQuery();
            }
        }

        public void Spremeni_kolicino(int Idd, int kolicina)
        {
            using (SQLiteCommand cmd = new SQLiteCommand(con))
            {
                cmd.CommandText = @"UPDATE Items SET Kolicina = (@kolicina) WHERE Id = (@idd);";
                cmd.Parameters.AddWithValue("@idd", Idd);
                cmd.Parameters.AddWithValue("@kolicina", kolicina);
                cmd.Prepare();
                cmd.ExecuteNonQuery();
            }
        }

        public void Dodaj(int a, string b, string c, string d, string e, int f, int g)
        {
            using (SQLiteCommand cmd = new SQLiteCommand(con))
            {
                cmd.CommandText = @"INSERT INTO Items (Id, Naziv, Opis, Tip_Avta, model_avta, Cena, Kolicina) VALUES (@a, @b, @c, @d, @e, @f, @g);";

                cmd.Parameters.AddWithValue("@a", a);
                cmd.Parameters.AddWithValue("@b", b);
                cmd.Parameters.AddWithValue("@c", c);
                cmd.Parameters.AddWithValue("@d", d);
                cmd.Parameters.AddWithValue("@e", e);
                cmd.Parameters.AddWithValue("@f", f);
                cmd.Parameters.AddWithValue("@g", g);

                cmd.Prepare();
                cmd.ExecuteNonQuery();

                //cmd.CommandText = @"INSERT INTO Items Naziv VALUES (@b);";
                //cmd.Parameters.AddWithValue("@b", b);
                //cmd.Prepare();
                //cmd.ExecuteNonQuery();

                //cmd.CommandText = @"INSERT INTO Items Opis VALUES (@c);";
                //cmd.Parameters.AddWithValue("@c", c);
                //cmd.Prepare();
                //cmd.ExecuteNonQuery();

                //cmd.CommandText = @"INSERT INTO Items Tip_Avta VALUES (@d);";
                //cmd.Parameters.AddWithValue("@d", d);
                //cmd.Prepare();
                //cmd.ExecuteNonQuery();

                //cmd.CommandText = @"INSERT INTO Items model_avta VALUES (@e);";
                //cmd.Parameters.AddWithValue("@e", e);
                //cmd.Prepare();
                //cmd.ExecuteNonQuery();

                //cmd.CommandText = @"INSERT INTO Items Cena VALUES (@f);";
                //cmd.Parameters.AddWithValue("@f", f);
                //cmd.Prepare();
                //cmd.ExecuteNonQuery();

                //cmd.CommandText = @"INSERT INTO Items Kolicina VALUES (@g);";
                //cmd.Parameters.AddWithValue("@g", g);
                //cmd.Prepare();
                //cmd.ExecuteNonQuery();
            }
        }

        public static SQLiteConnection Povezava_db
        {
            get{ return con;}
        }

    }
}
