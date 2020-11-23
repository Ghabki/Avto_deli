using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Avto_deli
{
     
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class Login : Window
    {
        private static Database db = new Database();


        public Login()
        {
            InitializeComponent();
            Innit();


        }

        private void Login_Button_Click(object sender, RoutedEventArgs e)
        {
            string ime = Username_Text.Text;
            string geslo = Password_Text.Password;

            if (db.Preveri_ime_geslo(ime, geslo))
            {
                if ((bool)Database_Radiobuton.IsChecked)
                {
                    Console.WriteLine("1");
                }
                else if ((bool)GeneričnaZbirka_Radiobuton.IsChecked)
                {
                    Console.WriteLine("2");
                }
                else if ((bool)VerižniSeznam_Radiobuton.IsChecked)
                {
                    Console.WriteLine("3");
                }
            }
            


        }


    

        private void Register_Button_Click(object sender, RoutedEventArgs e)
        {
            Register reg = new Register();
            reg.Show();
        }

        private void Innit()
        {

            #region check_direktoriji_in_fili
            //Database direktory in njegova inicializacija
            try
            {
                if (Directory.Exists(@".\Data"))
                {
                    db.Start_Database();
                    Console.WriteLine("That path exists already. \n in zaza startana");
                }
                else
                {
                    DirectoryInfo di = Directory.CreateDirectory(@".\Data");
                    File.Create(@"./Data/Database.db");
                    Console.WriteLine("Narejen direktorij Data");

                    Innit_DB();

                    //TODO ----------------------------------------------------------dodaj se tabelo z dejanskimi podatki in tabelo z nastavitve in pridobivanje usernama
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("ou yea NE baza dela");
                MessageBox.Show("Kreacija Data error: " + ex, " Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Close();
            }

            try
            {
                if (Directory.Exists(@".\Xml"))
                {
                    Console.WriteLine("xml path exists already.");
                }
                else
                {
                    DirectoryInfo di = Directory.CreateDirectory(@".\Xml");
                    Console.WriteLine("Narejen direktorij Xml");
                }

                //Xml ko se vse prekopira gre xml file v smeti
                if (Directory.Exists(@".\Xml_kanta"))
                {
                    Console.WriteLine("xml kanta path exists already.");
                }
                else
                {
                    DirectoryInfo di = Directory.CreateDirectory(@".\Xml_kanta");
                    Console.WriteLine("Narejen direktorij Xml_kanta");
                }

                //Xml izhod  kamor izvori xml podatke
                if (Directory.Exists(@".\Xml_Izhod"))
                {
                    Console.WriteLine("xml Izhod path exists already.");
                }
                else
                {
                    DirectoryInfo di = Directory.CreateDirectory(@".\Xml_Izhod");
                    Console.WriteLine("Narejen direktorij xml_izhod");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("kreacija direktorijec xml napaka");
                MessageBox.Show("Kreacija direktorijev error: " + ex, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Close();
            }
            #endregion


        }

        private void Innit_DB()
        {
            try
            {
                db.Start_Database(); //-------------------------------------------------------------------------------------------------------- pol ko rabis prižgi
                db.Create_Login_base();// to je tukaj za setat prvo tabelo. na to mesto dodaj se ostalo
                Console.WriteLine("ou yea baza dela");
            }
            catch (Exception ex)
            {
                Console.WriteLine("ou yea NE baza dela");
                MessageBox.Show("Database error: " + ex, "Database Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Close();
            }
        }

        public static Database Share_DB
        {
            get { return db; }
        }


    }
}
