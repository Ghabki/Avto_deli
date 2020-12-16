﻿using System;
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
using System.Windows.Shapes;

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
            }
            catch (Exception ex)
            {
                MessageBox.Show("Napaka pri shranjevanju" + ex, " Error", MessageBoxButton.OK, MessageBoxImage.Error);


            }

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
