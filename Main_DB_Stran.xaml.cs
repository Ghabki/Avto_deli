using System;
using System.Collections.Generic;
using System.Data.SQLite;
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
using System.Windows.Shapes;

namespace Avto_deli
{
    /// <summary>
    /// Interaction logic for Main_DB_Stran.xaml
    /// </summary>
    public partial class Main_DB_Stran : Window
    {
        private Database db = Login.Share_DB;
        public Main_DB_Stran()
        {
            InitializeComponent();
            db.Db_Preveri_Table();
            XML_Innit();
            Sestavi_ListView();
        }

        private void Uvozi_iz_datoteke_Click(object sender, RoutedEventArgs e)
        {
            XML_Innit();
        }

        private void Izvozi_iz_datoteke_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                db.Izvozi();
                MessageBox.Show("Uspešno kreirana datoteka", "Usprešno", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Napaka pri izdelavi txt datoteke: " + ex, "Izdelava txt Error", MessageBoxButton.OK, MessageBoxImage.Error);
                throw;
            }
        }

        private void Izbrisi_Click(object sender, RoutedEventArgs e) //brisi glede na ID
        {
            string primerjava= ID_izbrisat.Text;
            if (!primerjava.Equals(null))
            {
                try
                {
                    try
                    {
                        int id = Convert.ToInt32(primerjava);
                        db.Izbrisi(id);
                        Sestavi_ListView();
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Nisi vnesel pravilno stevilke", "information", MessageBoxButton.OK, MessageBoxImage.Information);
                        return;
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Prazno polje", "information", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
            }
        }

        private void Spremeni_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                int idd = Convert.ToInt32(ID_Spremeniti.Text);
                int nova_cena = Convert.ToInt32(kolicina_Sprememba.Text);
                
                db.Spremeni_kolicino(idd, nova_cena);
                Sestavi_ListView();
            }
            catch (Exception)
            {
                MessageBox.Show("Nisi vnesel pravilno stevilk", "information", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
        }

        private void Dodaj_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(ID_TextBox.Text);
                string Naziv = Naziv_TextBox.Text;
                string Opis = Opis_TextBox.Text;
                string Tip_avta = Tip_avta_TextBox.Text;
                string Model_avta = Model_avta_TextBox.Text;
                int Cena = Convert.ToInt32(Cena_TextBox.Text);
                int Kolicina = Convert.ToInt32(Kolicina_TextBox.Text);

                db.Dodaj(id, Naziv, Opis, Tip_avta, Model_avta, Cena, Kolicina);
                Sestavi_ListView();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Nekaj je narobe pri vpisu v okenca \n \n"+ex  , "Error", MessageBoxButton.OK, MessageBoxImage.Error);

                return;
            }
        }

        private void XML_Innit()
        {
            bool isEmpty = !Directory.EnumerateFiles(@"./Text").Any();
            if (isEmpty)
            {
                MessageBox.Show("Ni datoteke v mapi. Oziroma Ob zagonu ni datoteke z podatki", "information", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else 
            {

                string[] datoteka = Directory.GetFiles(@"./Text");
                string ext = datoteka[0].Split('.')[2];
                Console.WriteLine(ext);

                if (datoteka.Length==1 && ext == "txt")
                {
                    db.Xml_v_Bazo();
                }
                else
                {
                    MessageBox.Show("Datoteke v mapi ne obstaja ali ni prave končnice", "information", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            return;
        }

        private void Sestavi_ListView()
        {
            ListView_DB.ItemsSource = db.Vrni_vse();
        }

    }
}
