using MyProtocols_MelanyRodriguez.Models;
using MyProtocols_MelanyRodriguez.ViewModels;
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
    public partial class PasswordChange : ContentPage
    {
        UserViewModel viewModel;
        public PasswordChange()
        {
            InitializeComponent();
            BindingContext = viewModel = new UserViewModel();
            LoadUserPassword();
        }
        private void LoadUserPassword()
        {

            TxtPassword.Text = GlobalObjects.MyLocalUser.Contrasenia;

        }
        private async void BtnChange_Clicked(object sender, EventArgs e)
        {
            if (ValidateFields())
            {
                //sacer un respaldo del usuario global por si algo sale mal 
                //en el proceso del update, reversar los cambios

                UserDTO BackUpLocalPassword = new UserDTO();
                BackUpLocalPassword = GlobalObjects.MyLocalUser;
            }

            try
            {
           
                GlobalObjects.MyLocalUser.Contrasenia = TxtPassword.Text.Trim();

                var answer =await DisplayAlert("???", "Are you sure to continue change user password", "Yes", "No");

                if (answer)
                {
                    bool R = await viewModel.UpdatePasswordUser(GlobalObjects.MyLocalUser);

                    if (R)
                    {
                        await DisplayAlert(":)", "Password Changed!!!", "OK");
                        await Navigation.PopAsync();
                    }
                    else
                    {
                        await DisplayAlert(":(", "Something went wrong", "OK");
                        await Navigation.PopAsync();
                    }
                }
            }
            catch (Exception)
            {
                //si algo sale mal reversamos los cambios
                GlobalObjects.MyLocalUser = BackUpLocalPassword;
                throw;
            }
        }
        private bool ValidateFields()
        {
            bool R = false;
            if (Txt.Text != null && !string.IsNullOrEmpty(TxtPassword.Text.Trim()))
            {
                // es este caso estan todos los datos requeridos

                R = true;
            }

            else
            {
                //si falta algun dato requerido
                if (TxtPassword.Text == null || string.IsNullOrEmpty(TxtPassword.Text.Trim()))
                {
                    DisplayAlert("Validation Failed!", "the new password is required", "OK");
                    TxtPassword.Focus();
                    return false;
                }
            }

            return R;
        }
    }
}
