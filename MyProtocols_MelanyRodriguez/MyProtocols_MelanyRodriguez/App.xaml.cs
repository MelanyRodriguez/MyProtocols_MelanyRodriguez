using MyProtocols_MelanyRodriguez.Services;
using MyProtocols_MelanyRodriguez.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyProtocols_MelanyRodriguez
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();

            //definimos la forma de apilar paginas en la pantalla y cual es la primera pagina 
            //que mostraremos

            MainPage = new NavigationPage (new AppLoginPage());
          
            
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
