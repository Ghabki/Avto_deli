using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace Avto_deli
{
    /// <summary>
    /// Interaction logic for Register.xaml
    /// </summary>
    public partial class Register : Window
    {
        public Register()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Database db = Login.Share_DB;

            string username = Add_Username.Text;
            string Pass1 = Add_Password.Password;
            string Pass2 = Add_RePassword.Password;

            List<string> Vsi_usernami = db.Vrni_Username();
            foreach (var item in Vsi_usernami)
            {
                Console.WriteLine(item);
            }
            
            Lable_Notificarion.Visibility = Visibility.Visible;
            if (Vsi_usernami.Contains(username))
            {
                Lable_Notificarion.Foreground = new SolidColorBrush(Colors.Red);
                Lable_Notificarion.Content = "Usename že obstaja";
                return;
            }
            if (Pass1 != Pass2)
            {
                Lable_Notificarion.Foreground = new SolidColorBrush(Colors.Red) ; 
                Lable_Notificarion.Content = "Gesla nista enaka" ;
                return;
            }

            db.Dodaj_Uporabnika(username, Pass1);
            
            Lable_Notificarion.Foreground = new SolidColorBrush(Colors.Green);
            Lable_Notificarion.Content = "Uspešna registracija";

        }
    }
}
