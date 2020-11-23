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
        public Login()
        {
            InitializeComponent();
            Innit();

        }

        private void Register_Button_Click(object sender, RoutedEventArgs e)
        {
            Register abc = new Register();
            abc.Show();
        }


        private void Innit()
        {
            #region check_direktoriji_in_fili
            try
            {
                if (Directory.Exists(@".\Data"))
                {
                    Console.WriteLine("That path exists already.");
                }
                else
                {
                    DirectoryInfo di = Directory.CreateDirectory(@".\Data");
                    File.Create(@"./Data/Database.db");
                    Console.WriteLine("Narejen direktorij Data");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("ou yea NE baza dela");
                MessageBox.Show("Kreacija filov error: " + ex, "Database Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Close();
            }
            #endregion

            #region Database_start


            #endregion

        }

        private void Innit_Db()
        {

            Database db = new Database();
            try
            {
                db.Start_Database(); //-------------------------------------------------------------------------------------------------------- pol ko rabis prižgi

                Console.WriteLine("ou yea baza dela");
            }
            catch (Exception ex)
            {
                Console.WriteLine("ou yea NE baza dela");
                MessageBox.Show("Database error: " + ex, "Database Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Close();
            }

            Database db = new Database();
            try
            {
                db.Start_Database(); //-------------------------------------------------------------------------------------------------------- pol ko rabis prižgi

                Console.WriteLine("ou yea baza dela");
            }
            catch (Exception ex)
            {
                Console.WriteLine("NE dela prižig baze");
                MessageBox.Show("Database error: " + ex, "Database Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Close();
            }
        }

    }
}
