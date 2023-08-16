using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyProtocols_MelanyRodriguez.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StarPage : ContentPage
    {
        public StarPage()
        {
            InitializeComponent();
            LoadUserName();
        }

        private void LoadUserName()
        {
            LblUserName.Text = GlobalObjects.MyLocalUser.Nombre.ToUpper();
        }

        private async void BtnUserManadgment_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new UserManadgmentPage());
        }
    }
}