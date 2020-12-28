using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;

using System.Data.Entity;



namespace Avto_deli
{
    /// <summary>
    /// Interaction logic for Main_Genericna_zbirka_Stran.xaml
    /// </summary>
    public partial class Main_Genericna_zbirka_Stran : Window
    {
        GeneričnaZbirka<Deli> Gen = new GeneričnaZbirka<Deli>(0);//0 pove začetno velikost

        public Main_Genericna_zbirka_Stran()
        {
            InitializeComponent();
            
            Innit_Genericna();
            //Linq1();
        }

        private void Sort_Button_n(object sender, RoutedEventArgs e)
        {
            Deli[] tm = Gen.IzpisArray();

            // korak ponavljamo tolikokrat, kot je elementov v tabeli
            //spremenljivka neurejeni kaže na prvi element v neurejenem delu
            for (int neurejeni = 0; neurejeni < tm.Length; neurejeni++)
            {
                // izberemo najmanjši element iz neurejenega dela
                int najmanjsiPozicija = neurejeni;
                // zapomnimo si tudi njegov položaj
                Deli najmanjsi = tm[neurejeni];
                for (int i = neurejeni + 1; i < tm.Length; i++)
                {
                    if (tm[i].TaCena < najmanjsi.TaCena)
                    {
                        najmanjsiPozicija = i;
                        najmanjsi = tm[i];
                    }
                }
                /* zamenjamo prvi element v neurejenem delu z najmanjšim 
                    elementom iz neurejenega dela*/
                tm[najmanjsiPozicija] = tm[neurejeni];
                tm[neurejeni] = najmanjsi;
            }

            Izpis_zaslon();

        }

        private void Shrani_Button_n(object sender, RoutedEventArgs e)
        {
            try
            {
                if (File.Exists(@"./Gen_Mapa/Gen_data"))
                {
                    File.Delete(@"./Gen_Mapa/Gen_data");
                }
                
                Serializacija(Gen, @"./Gen_Mapa/Gen_data.xml");
                MessageBox.Show("Shranjeno", "Shranjeno", MessageBoxButton.OK, MessageBoxImage.Information);


            }
            catch (Exception ex)
            {
                MessageBox.Show("Napaka pri shranjevanju" + ex, " Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void Izbris_ID_Click(object sender, RoutedEventArgs e)
        {
            int ste = -1;
            try
            {
                ste = ListBox_Gen.SelectedIndex;
            }
            catch (Exception)
            {
                MessageBox.Show("", " Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            if (!(ste == -1))
            {
                Gen.Brisanje(ste);
            }
            else
            {
                MessageBox.Show("Nisi izbral", " Error", MessageBoxButton.OK, MessageBoxImage.Information);
            }

            Izpis_zaslon();

        }

        private void Random_button_Click(object sender, RoutedEventArgs e)
        {
            List<Deli> izpis = Randomizer(Gen);
            ListBox_Gen.Items.Clear();

            for (int i = 0; i < izpis.Count; i++)
            {
                ListBox_Gen.Items.Add(izpis[i]);
            }
        }

        private void Izpiši_Button_Click(object sender, RoutedEventArgs e)
        {
            Izpis_zaslon();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Linq1();
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Linq2();
        }
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Linq3();
        }
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            Linq4();
        }
        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            Linq5();
        }
        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            Linq6();
        }
        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            Linq7();
        }
        private void Button_Click_7(object sender, RoutedEventArgs e)
        {
            Linq8();
        }
        private void Button_Click_8(object sender, RoutedEventArgs e)
        {
            Linq9();
        }
        private void Button_Click_9(object sender, RoutedEventArgs e)
        {
            Linq10();
        }

        private Stack<T> Stack_primer<T>(GeneričnaZbirka<T> zbirka) {
            Stack<T> stak = new Stack<T>();

            T[] zac = zbirka.IzpisArray();
            foreach (var item in zac)
            {
                stak.Push(item);
            }
            return stak;
        }

        private List<T> Randomizer<T>(GeneričnaZbirka<T> zbirka) {
            T[] vse = zbirka.IzpisArray();
            

            Random rnd = new Random();
            for (int i = vse.Length - 1; i > 0; i--)
            {
                int randomIndex = rnd.Next(0, vse.Length);

                T temp = vse[i];
                vse[i] = vse[randomIndex];
                vse[randomIndex] = temp;
            }
            return vse.ToList();
        }

        private void Innit_Genericna()
        {
            try
            {
                List<Deli> NovaZbirka = new List<Deli>();

                Deserializacija<Deli>(NovaZbirka, "./Gen_Mapa/Gen_data.xml");//mora imeti tako ime
                for (int i = 0; i < NovaZbirka.Count; i++)
                {
                    Gen.Add(NovaZbirka[i]);
                    //ListBox_Gen.Items.Add(NovaZbirka[i]);
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Ni Prave datoteke v mapi da izpise na zaslon" + ex, " Error", MessageBoxButton.OK, MessageBoxImage.Error);

            }
                //aaa.IzpisZbirke();
            Izpis_zaslon();
        }

        private void Izpis_zaslon()
        {
            Deli[] tm = Gen.IzpisArray();

            ListBox_Gen.Items.Clear();

            for (int i = 0; i < tm.Length; i++)
            {
                ListBox_Gen.Items.Add(tm[i]);
            }

        }

        private void Linq1()//bmw
        {
            var zac = from Deli a in Gen where a.TaTip_avta=="BMW" select a;

            //foreach (var item in maxsalary)
            //{
            //    Console.WriteLine(item.TaCena.ToString());
            //}

            ListBox_Gen.Items.Clear();

            foreach (var item in zac)
            {
                ListBox_Gen.Items.Add(item);
            }

        }

        private void Linq2()
        {
            var zac = from Deli a in Gen where a.TaCena >= 5 select a;
            
            ListBox_Gen.Items.Clear();
            foreach (var item in zac)
            {
                ListBox_Gen.Items.Add(item);
            }
        }

        private void Linq3()
        {
            var zac = from Deli a in Gen where a.TaCena < 5 && a.TaModel_avta[0] == 'C' select a;

            ListBox_Gen.Items.Clear();
            foreach (var item in zac)
            {
                ListBox_Gen.Items.Add(item);
            }
        }

        private void Linq4()
        {
            var zac = from Deli a in Gen where a.TaModel_avta.Length > 3 select a;

            ListBox_Gen.Items.Clear();
            foreach (var item in zac)
            {
                ListBox_Gen.Items.Add(item);
            }
        }
        private void Linq5()
        {
            var zac = from Deli a in Gen where a.TaId < 10 select a;


            ListBox_Gen.Items.Clear();
            foreach (var item in zac)
            {
                ListBox_Gen.Items.Add(item);
            }
        }
        private void Linq6()//lambda
        {
            List<Deli>  tab= Gen.IzpisArray().ToList();//zelo nepotrebno but well majhna tabelca
            var zac = tab.Where(n => n.TaId % 2 ==0);
            ListBox_Gen.Items.Clear();
            foreach (var item in zac)
            {
                ListBox_Gen.Items.Add(item);
            }
        }
        private void Linq7()
        {
            List<Deli> tab = Gen.IzpisArray().ToList();//zelo nepotrebno but well majhna tabelca
            var zac = tab.Where(n => n.TaOpis.ToLower().Contains("zavore"));

            ListBox_Gen.Items.Clear();
            foreach (var item in zac)
            {
                ListBox_Gen.Items.Add(item);
            }
        }
        private void Linq8()
        {
            List<Deli> tab = Gen.IzpisArray().ToList();//zelo nepotrebno but well majhna tabelca
            var zac = tab.Count(n => n.TaId>=0);
            ListBox_Gen.Items.Clear();

            ListBox_Gen.Items.Add(zac);
            
        }
        private void Linq9()
        {
            List<Deli> tab = Gen.IzpisArray().ToList();//zelo nepotrebno but well majhna tabelca
            var zac = tab.Count(n => n.TaTip_avta=="BMW");
            ListBox_Gen.Items.Clear();
            
            
            ListBox_Gen.Items.Add(zac);
            
        }
        private void Linq10()
        {
            List<Deli> tab = Gen.IzpisArray().ToList();//zelo nepotrebno but well majhna tabelca
            var zac = tab.Count(n => n.TaKolicina>=10);
            ListBox_Gen.Items.Clear();

            ListBox_Gen.Items.Add(zac);
            
        }






        static void Serializacija<T>(GeneričnaZbirka<T> zbirka, string fileName)
        {
            List<T> list = new List<T>();
            for (int i = 0; i < zbirka.Velikost; i++) {
                list.Add(zbirka[i]);
            }
                
            var serializer = new System.Xml.Serialization.XmlSerializer(typeof(List<T>));
            using (var stream = System.IO.File.OpenWrite(fileName))
            {
                serializer.Serialize(stream, list);
            }
        }

        static void Deserializacija<T>(List<T> list, string fileName)
        {
            var serializer = new System.Xml.Serialization.XmlSerializer(typeof(List<T>));
            using (var stream = System.IO.File.OpenRead(fileName))
            {
                var other = (List<T>)(serializer.Deserialize(stream));
                list.Clear();
                list.AddRange(other);
            }
        }


    }
}
