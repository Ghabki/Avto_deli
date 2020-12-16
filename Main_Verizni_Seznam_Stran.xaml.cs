using System;
using System.IO;
using System.Windows;
using System.Windows.Media;

namespace Avto_deli
{
    /// <summary>
    /// Interaction logic for Main_Verizni_Seznam_Stran.xaml
    /// </summary>
    public partial class Main_Verizni_Seznam_Stran : Window
    {

        public string vsebinaa = "";
        public Vozel<Deli> Glavni_Vozel = new Vozel<Deli>();

        public Main_Verizni_Seznam_Stran()
        {
            InitializeComponent();
            Init_Vozel();

            Izpis(Glavni_Vozel.ZacetekSeznama);
            Console.WriteLine(Glavni_Vozel.PrestejElemente());//test
            naZaslon();
        }

        private void Button_cena10_Click(object sender, RoutedEventArgs e)
        {
            ŠteviloElementov(Glavni_Vozel.ZacetekSeznama);
            naZaslon();
        }

        private void Button_Shrani_Click(object sender, RoutedEventArgs e)
        {
            shrani(Glavni_Vozel.ZacetekSeznama);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int a = Glavni_Vozel.PrestejElemente() - 1;
            Lable_Vozel.Visibility = Visibility.Visible;
            Lable_Vozel.Foreground = new SolidColorBrush(Colors.White);
            Lable_Vozel.Content = "Stevilo elementov v razredu= "+a;

        }



        private void Init_Vozel()
        {
            string[] lines = File.ReadAllLines("./VozelInZbirka_Podatki/tekst.txt");
            foreach (string line in lines)
            {
                try
                {
                    string[] vnesi = line.Split(';');
                    Glavni_Vozel.Dodaj(new Deli(Convert.ToInt32(vnesi[0]), vnesi[1], vnesi[2], vnesi[3], vnesi[4], Convert.ToInt32(vnesi[5]), Convert.ToInt32(vnesi[6])));
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Napaka pri branju datoteke " + ex, "Napaka", MessageBoxButton.OK, MessageBoxImage.Error);
                    
                }
            }     
        }
        //samo za vajo je rekruzivno 
        private void Izpis<T>(Vozel<T> voz)
        {
            if (voz != null)
            {
                if (voz.Vsebina != null) //prej sem narove vstavljal pa je to blo potrebno dodati. ta if ni več potreben
                {
                    vsebinaa += voz.Vsebina.ToString() + "\n";
                    voz = voz.Nasl;
                    Izpis(voz);
                }

            }
        }

        private void ŠteviloElementov(Vozel<Deli> voz) {
            int st = 0;
            vsebinaa = "";
            while (voz != null)
            {
                if (voz.Vsebina != null)
                {
                    if (voz.Vsebina.TaKolicina >= 10)
                    {
                        st++;
                    }
                    voz = voz.Nasl;
                }
                else
                {
                    break;
                }

            }
            vsebinaa = "Število artiklov ki imajo ceno večjo od 10: " + st;
            
        }

        //samo shrani brez pravilnega izpisa za 
        private void shrani<T>(Vozel<T> voz)
        {

            string izpis = "";
            string fil = DateTime.UtcNow.TimeOfDay.ToString().Replace(":", "");
            //Console.WriteLine(DateTime.UtcNow.TimeOfDay.ToString().Replace(".", ""));
            FileStream fs = File.Create(@"./VozelInZbirka_Podatki/" + fil + ".txt");
            fs.Close();

            if (voz != null)
            {
                if (voz.Vsebina != null)
                {
                    izpis += voz.Vsebina.ToString() + "\n";
                    voz = voz.Nasl;
                    Izpis(voz);
                }

            }
            StreamWriter file = new StreamWriter(@"./VozelInZbirka_Podatki/" + fil + ".txt");
            file.WriteLine(izpis);
            MessageBox.Show("Izpisano ", "Izpisano", MessageBoxButton.OK, MessageBoxImage.Information);

            file.Close();// lahko bi uporabu using stavek
        }

        private void naZaslon()
        {
            Lable_Vozel.Visibility = Visibility.Visible;
            Lable_Vozel.Foreground = new SolidColorBrush(Colors.White);
            Lable_Vozel.Content = vsebinaa;
        }


    }

}


