using tarea2_4movil2.Controllers;
using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace tarea2_4movil2
{
    public partial class App : Application
    {
        static videoDB BaseDatos;

        public static videoDB BaseDatosObject
        {
            get
            {
                if (BaseDatos == null)
                {
                    BaseDatos = new videoDB(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "VideosDB.db3"));
                }
                return BaseDatos;
            }
        }
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MainPage());
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
