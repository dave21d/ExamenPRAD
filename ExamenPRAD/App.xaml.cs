using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.IO;
using static ExamenPRAD.CONTROLLER.ContactosControllers;
using ExamenPRAD.CONTROLLER;

namespace ExamenPRAD
{
    public partial class App : Application
    {
        public static SqliteHelper dbcontac;
        public static SqliteHelper sqlitedb
        {
            get
            {
                if (dbcontac == null)
                {
                    dbcontac = new SqliteHelper(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Personasr.db3"));
                }
                return dbcontac;
            }
        }

        
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new VIEWS.PageContactos());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
