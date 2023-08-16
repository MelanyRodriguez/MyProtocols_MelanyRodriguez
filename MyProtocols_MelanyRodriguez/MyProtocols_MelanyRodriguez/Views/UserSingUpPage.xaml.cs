using MyProtocols_MelanyRodriguez.ViewModels;
using MyProtocols_MelanyRodriguez.Models;
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
    public partial class UserSingUpPage : ContentPage
    {
        UserViewModel viewModel;
        public UserSingUpPage()
        {
            InitializeComponent();
            BindingContext = viewModel = new UserViewModel();
            LoadUserRolesAsync();
        }
        //funcion que permite la carga de los roles de usuario
        private async void LoadUserRolesAsync()
        {
            PkrUserRole.ItemsSource = await viewModel.GetUserRolesAsync();
        }

        private async void BtnAdd_Clicked(object sender, EventArgs e)
        {
            //capturar el rol que se haya seleccionado en el picker
            UserRole SelectedUserRole = PkrUserRole.SelectedItem as UserRole;

            bool R = await viewModel.AddUserAsync(TxtEmail.Text.Trim(),
                                                  TxtPassword.Text.Trim(),
                                                  TxtName.Text.Trim(),
                                                  TxtBackUpEmail.Text.Trim(),
                                                  TxtPhoneNumber.Text.Trim(),
                                                  TxtAddress.Text.Trim(),
                                                  SelectedUserRole.UserRoleId);
                if (R) 
            {
                await DisplayAlert("(:", "user created ok!", "OK");
                await Navigation.PopAsync();
            }
                else
            {
                await DisplayAlert("):", "something went wrong...", "OK");
            }
        }

        //private void BtnCncel_Clicked(object sender, EventArgs e)
        //{
        //}

        private async void BtnCancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}