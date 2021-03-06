﻿using System;
using System.IO;
using System.Windows;
using System.Windows.Media;

//"Izjavljam, da sem nalogo opravil samostojno in da sem njen avtor. Zavedam se, da v primeru, če izjava prvega stavka ni resnična, kršim disciplinska pravila."
namespace Avto_deli
{

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
                    Main_DB_Stran MDBS = new Main_DB_Stran();
                    this.Close();
                    MDBS.Show();
                }
                else if ((bool)GeneričnaZbirka_Radiobuton.IsChecked)
                {
                    Main_Genericna_zbirka_Stran MDBS = new Main_Genericna_zbirka_Stran();
                    this.Close();
                    MDBS.Show();
                }
                else if ((bool)VerižniSeznam_Radiobuton.IsChecked)
                {
                    Main_Verizni_Seznam_Stran MDBS = new Main_Verizni_Seznam_Stran();
                    this.Close();
                    MDBS.Show();
                }
            }
            else
            {
                Login_Lable.Visibility = Visibility.Visible;
                Login_Lable.Foreground = new SolidColorBrush(Colors.Red);
                Login_Lable.Content = "Napačno ime ali geslo";
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
                if (Directory.Exists(@".\Text"))
                {
                    Console.WriteLine("Text path exists already.");
                }
                else
                {
                    DirectoryInfo di = Directory.CreateDirectory(@".\Text");
                    Console.WriteLine("Narejen direktorij Text");
                }

                //Xml ko se vse prekopira gre xml file v smeti
                if (Directory.Exists(@".\Text_kanta"))
                {
                    Console.WriteLine("Text kanta path exists already.");
                }
                else
                {
                    DirectoryInfo di = Directory.CreateDirectory(@".\Text_kanta");
                    Console.WriteLine("Narejen direktorij Text_kanta");
                }

                //Xml izhod  kamor izvori xml podatke
                if (Directory.Exists(@".\Text_Izhod"))
                {
                    Console.WriteLine("Text Izhod path exists already.");
                }
                else
                {
                    DirectoryInfo di = Directory.CreateDirectory(@".\Text_Izhod");
                    Console.WriteLine("Narejen direktorij Text_izhod");
                }

                //Vozel
                if (Directory.Exists(@".\VozelInZbirka_Podatki"))
                {
                    Console.WriteLine("Text VozelInZbirka path exists already.");
                }
                else
                {
                    DirectoryInfo di = Directory.CreateDirectory(@".\VozelInZbirka_Podatki");
                    Console.WriteLine("Narejen direktorij VozelInZbirka");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("kreacija direktorijec Text napaka");
                MessageBox.Show("Kreacija direktorijev error: " + ex, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Close();
            }


            try
            {
                if (Directory.Exists(@".\Gen_Mapa"))
                {
                    Console.WriteLine("That Gen_Mapa exists already.");
                }
                else
                {
                    DirectoryInfo di = Directory.CreateDirectory(@".\Gen_Mapa");
                    Console.WriteLine("Narejen direktorij Data");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Kreacija Gen_Mapa error: " + ex, " Error", MessageBoxButton.OK, MessageBoxImage.Error);
                
            }



            #endregion


        }

        private void Innit_DB()
        {
            try
            {
                db.Start_Database(); 
                db.Create_Login_base();
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
