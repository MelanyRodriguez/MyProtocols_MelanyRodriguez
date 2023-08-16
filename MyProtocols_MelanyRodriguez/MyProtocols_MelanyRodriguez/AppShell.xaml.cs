using MyProtocols_MelanyRodriguez.ViewModels;
using MyProtocols_MelanyRodriguez.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace MyProtocols_MelanyRodriguez
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            //Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            //Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
        }

    }
}
