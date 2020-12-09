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
    /// Interaction logic for Main_Genericna_zbirka_Stran.xaml
    /// </summary>
    public partial class Main_Genericna_zbirka_Stran : Window
    {
        
        public Main_Genericna_zbirka_Stran()
        {
            InitializeComponent();

            






            Deli nice = new Deli(1);
            GeneričnaZbirka<Deli> aaa = new GeneričnaZbirka<Deli>(0);
            //aaa.Add(nice);
            //aaa.Add(new Deli(2));
            //aaa.Add(new Deli(3));
            //aaa.Add(new Deli(4));
            //Serializacija(aaa, "loll.xml");               //to je za 
            aaa.IzpisZbirke();

            List<Deli> NovaZbirka = new List<Deli>();
            Deserializacija<Deli>(NovaZbirka, "./loll.xml");
            for (int i = 0; i < NovaZbirka.Count; i++)
            {
                aaa.Add(NovaZbirka[i]);
            }


            aaa.IzpisZbirke();

        }


        private void Innit_Genericna()
        {


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
