using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avto_deli
{
    public class Deli
    {
        private int Idd;
        private string Naziv;
        private string Opis;
        private string Tip_avta;
        private string Model_avta;
        private int Cena;
        private int Kolicina;

        public Deli() { }//tega ne rabi
        public Deli(int id, string Naziv = "Genericen_avto", string Opis= "Izdelek", string Tip_avta="Vsi_tipi", string Model_avta="Vsi modeli", int Cena=1, int Kolicina=1) {

            this.Idd = id;
            this.Naziv =Naziv;
            this.Opis =Opis;
            this.Tip_avta =Tip_avta;
            this.Model_avta = Model_avta;
            this.Cena = Cena;
            this.Kolicina = Kolicina;
        }
        public int TaId { get { return this.Idd; } set { Idd = value; } }
        public string TaNaziv { get { return this.Naziv; } set { Naziv = value; } }
        public string TaOpis { get { return this.Opis; } set { Opis = value; } }
        public string TaTip_avta { get { return this.Tip_avta; } set { Tip_avta= value; } }
        public string TaModel_avta { get { return this.Model_avta; } set { Model_avta = value; } }
        public int TaCena { get { return this.Cena; } set { Cena = value; } }
        public int TaKolicina { get { return this.Kolicina; } set { Kolicina = value; } }

        public override string ToString()
        {
            return "ID:" + Idd.ToString() + "  Naziv:" + Naziv + "  Opis:" + Opis + "  Tip Avta:" + Tip_avta + "  Model avta:" + Model_avta+ "  Cena:" + Cena+ "  Kolicina:" + Kolicina;
        }
    }






    public class ListViewData //samo za vpis v listview
    {
        public int Id { get; set; }
        public string Naziv { get; set; }
        public string Opis { get; set; }
        public string Tip_avta { get; set; }
        public string Model_Avta { get; set; }
        public int Cena { get; set; }
        public int Količina { get; set; }
    }

    public class GeneričnaZbirka<T>
    {

        private T[] elementi;  //tabelarično polje
        private int velikost;   //polje hrani trenutno število podatkov v tabeli    

        public GeneričnaZbirka(){}
        public GeneričnaZbirka(int n = 0)  //konstruktor
        { elementi = new T[n]; velikost = n; }//začetna dimenzija tabele/zbirke  
        public T this[int indeks]   //indeksiranje 
        {
            get { return elementi[indeks]; } //dostop do posameznih polj
            set { elementi[indeks] = value; }  //prirejanje vrednostim poljem
        }
        //napišimo property, s katerim pridobimo atribut velikost
        public int Velikost
        {
            get { return velikost; }
        }

        //še get metoda za prodobivanje polja velikost
        public int VrneVelikost()
        {
            return velikost;
        }

        public void OdstraniVse()
        {
            elementi = new T[0];
            velikost = 0;
        }
        public void Add(T podatek)  //metoda za dodajanje novega elementa 
        {
            Array.Resize(ref elementi, elementi.Length + 1);
            elementi[velikost] = podatek;  //podatek zapišemo v prvo prosto celico
            velikost = velikost + 1; //število zasedenih celic
        }
        //generična metoda za brisanje celice z določenim indeksom
        public void Brisanje(int indeksCelice)
        {
            if (velikost == 0)
                Console.WriteLine("Zbirka je prazna, brisanje NI možno!");
            //celico brišemo le, če je njen indeks manjši od dimenzije zbirke  
            // if (indeksCelice < elementi.Length && indeksCelice >= 0)
            else if (indeksCelice < elementi.Length)
            {
                T[] zacasna = new T[elementi.Length - 1];
                int j = 0;
                for (int i = 0; i < elementi.Length; i++)
                {
                    if (i != indeksCelice)
                    {
                        zacasna[j] = elementi[i];
                        j++;
                    }
                }
                elementi = zacasna;
                velikost = velikost - 1;//zmanjšamo velikost zbirke
            }
            else
            {
                Console.WriteLine("Brisanje NI možno, ker indeks št +" + indeksCelice + " NE obstaja!");
            }
        }
            //generična metoda za izpis poljubne zbirke
        public void IzpisZbirke()
        {
            if (velikost == 0)
                Console.WriteLine("Zbirka je prazna!");
            else
            {
                Console.WriteLine("Izpis ZBIRKE: ");
                for (int i = 0; i < elementi.Length; i++)
                    Console.WriteLine(elementi[i].ToString() + " ");
                Console.WriteLine();
            }
        }

    }



    public class Vozel<T>
    {
        private T podatek; // hranimo podatke tipa T
        private Vozel<T> naslednji; // referenca na naslednjega
                                    //začetek seznama je shranjen v objektu Zacetek
        private Vozel<T> Zacetek;
        public Vozel<T> ZacetekSeznama  //lastnost, ki vrne začetek seznama
        {
            get { return Zacetek; }
        }
        public Vozel()  // privzeti konstruktor nastavi privzete vrednosti
        {
            this.Vsebina = default(T);
            // kazalec na naslednji element
            this.Nasl = null;
            Zacetek = this;
        }
        public Vozel(T podatek)  //dodatni konstruktor
            : this()//dedujemo bazični konstruktor
        {
            this.Vsebina = podatek;
        }
        public T Vsebina  //Lastnost/property
        {
            get { return this.podatek; }
            set { this.podatek = value; }
        }
        public Vozel<T> Nasl  //Lastnost/property
        {
            get { return this.naslednji; }
            set { this.naslednji = value; }
        }
        public void Dodaj(T podatek)//Metoda za dodajanje elementa na začetek seznama
        {
            Vozel<T> nov = new Vozel<T>(podatek);
            nov.Nasl = Zacetek;
            Zacetek = nov;
        }
        public void Izpis<T>(Vozel<T> Zacetek) // Metoda za izpis seznama
        {
            //shranim začetek seznama
            Console.Write("ZAČETEK->");
            //premikamo se toliko časa, da je naslov naslednjega podatka null
            while (Zacetek != null)
            {
                Console.Write(Zacetek.Vsebina + " ");
                Zacetek = Zacetek.naslednji;
            }
            Console.Write("->KONEC\n");
        }

        
        public void Izpis() //običajna metoda za izpis poljubnega seznama
        {
            //shranim začetek seznama
            if (this != null)
            //lahko tudi takole:  if (Zacetek!=null)
            {
                //zapomnim si (shranim) začetek seznama
                Vozel<T> zacasni = ZacetekSeznama;
                Console.Write("ZAČETEK->");
                // premikamo se toliko časa, da je naslov naslednjega podatka null
                while (zacasni != null)
                {
                    Console.Write(zacasni.Vsebina + " ");
                    zacasni = zacasni.naslednji;
                }
                Console.Write("->KONEC\n");
            }
            else Console.WriteLine("Seznam je prazen!");
        }
        /*metoda za dodajanje novega podatka na konec seznama: drugi parameter je klican  po referenci, saj se bo lahjo spremenil začetek seznama*/
        public void VstaviNaKonec(T podatek)
        {
            Vozel<T> kjeSmo = Zacetek;//pomožni objekt za premik po seznamu 
            while (kjeSmo.Nasl != null)/*dokler ne pridemo do konca seznama, oz. dokler ne najdemo podatka za brisanje*/
            {
                kjeSmo = kjeSmo.Nasl;//prestavimo se na naslednji objekt
            }
            //ko pridemo iz zanke kazalec tekoči zagotovo kaže na zadnji objekt
            Vozel<T> nov = new Vozel<T>();
            nov.Vsebina = podatek;
            kjeSmo.Nasl = nov;
        }
        public int PrestejElemente() //Metoda, ki vrne število elementov seznama
        {
            int stevec = 0;
            if (Zacetek != null)
            {
                Vozel<T> kjeSmo = Zacetek;//začetek seznama 
                stevec = 1;
                while (kjeSmo.Nasl != null)
                {
                    stevec++;
                    kjeSmo = kjeSmo.Nasl;
                }
            }
            return stevec;
        }
        /*metoda ki v seznamu poišče podatek "podatek" in ga odstrani: prvi parameter je klican po referenci, saj se bo lahjo spremenil začetek seznama*/
        public void Izbriši(T podatek)
        {
            if (Zacetek.Vsebina != null)//če seznam ni prazen
            {
                //preverimo, če je iskani podatek že na začetku seznama
                if (Zacetek.Vsebina.Equals(podatek))
                {
                    if (Zacetek.naslednji != null)
                        Zacetek = Zacetek.Nasl;
                    else Zacetek = null;
                }
                else
                {
                    Vozel<T> tekoči = Zacetek;//dodatni objekt za premik po seznamu 
                    while (tekoči.Nasl != null) /*dokler ne pridemo do konca seznama, oz. dokler ne najdemo podatka za brisanje*/
                    {
                        //ali se vsebina NASLEDNJEGA objekta ujema z našim podatkom
                        if (tekoči.Nasl.Vsebina.Equals(podatek))
                        {
                            //preverimo, če ne gre slučajno za zadnji podatek v seznamu
                            if (tekoči.Nasl.Nasl != null)
                            {
                                //ker to ni zadnji objekt,prestavimo kazalec za ena naprej
                                tekoči.Nasl = tekoči.Nasl.Nasl;
                            }
                            else tekoči.Nasl = null;/*ker gre za ZADNJI objekt, nastavimo kazalec na null*/
                            break;//izhod iz zanke while
                        }
                        else tekoči = tekoči.Nasl;//
                    }
                }
            }
        }
    }

}

