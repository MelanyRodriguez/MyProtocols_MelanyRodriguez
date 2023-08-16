using MyProtocols_MelanyRodriguez.ViewModels;
using System;
using MyProtocols_MelanyRodriguez.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyProtocols_MelanyRodriguez.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserManadgmentPage : ContentPage
    {
        UserViewModel viewModel;
        public UserManadgmentPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new UserViewModel();

            LoadUserInfo();
        }

        private void LoadUserInfo()
        {
          TxtID.Text = GlobalObjects.MyLocalUser.UsuarioId.ToString();
            TxtEmail.Text = GlobalObjects.MyLocalUser.Correo;
            TxtName.Text = GlobalObjects.MyLocalUser.Nombre;
            TxtPhoneNumber.Text = GlobalObjects.MyLocalUser.Telefono;
            TxtBackUpEmail.Text = GlobalObjects.MyLocalUser.RespaldoCorreo;
            TxtAddress.Text = GlobalObjects.MyLocalUser.Direccion;

        }

        private async void BtnUpdate_Clicked(object sender, EventArgs e)
        {
            //primero hacemos la validacion de los campos requeridos
            if (ValidateFields())
            {
                //sacer un respaldo del usuario global por si algo sale mal 
                //en el proceso del update, reversar los cambios

                UserDTO BackUpLocalUser = new UserDTO();
                BackUpLocalUser = GlobalObjects.MyLocalUser;
            }

            try
            {
                GlobalObjects.MyLocalUser.Nombre = TxtName.Text.Trim();
                GlobalObjects.MyLocalUser.RespaldoCorreo = TxtBackUpEmail.Text.Trim();
                GlobalObjects.MyLocalUser.Telefono = TxtPhoneNumber.Text.Trim();
                GlobalObjects.MyLocalUser.Direccion = TxtAddress.Text.Trim();

                var answer = await DisplayAlert("???", "Are you sure to continue updating user info", "Yes", "No");

                if (answer)
                {
                    bool R = await viewModel.UpdateUser(GlobalObjects.MyLocalUser);

                    if (R)
                    {
                        await DisplayAlert(":)", "User updated!!!", "OK");
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
                GlobalObjects.MyLocalUser = BackUpLocalUser;
                throw;
            }
        }

        private bool ValidateFields()
        {
            bool R = false;

            if (TxtName.Text != null && !string.IsNullOrEmpty(TxtName.Text.Trim()) &&
                TxtBackUpEmail.Text != null && !string.IsNullOrEmpty(TxtBackUpEmail.Text.Trim()) &&
                TxtPhoneNumber.Text != null && !string.IsNullOrEmpty(TxtPhoneNumber.Text.Trim()))
            {
                // es este caso estan todos los datos requeridos
               
                R= true;
            }

            else
            {
                //si falta algun dato requerido
                if(TxtName.Text ==null || string.IsNullOrEmpty(TxtName.Text.Trim()))
                {
                    DisplayAlert("Validation Failed!", "the name is required", "OK");
                    TxtName.Focus();
                    return false;
                }

                if (TxtBackUpEmail.Text == null || string.IsNullOrEmpty(TxtBackUpEmail.Text.Trim()))
                {
                    DisplayAlert("Validation Failed!", "the BackUp Email is required", "OK");
                    TxtBackUpEmail.Focus();
                    return false;
                }

                if (TxtPhoneNumber.Text == null || string.IsNullOrEmpty(TxtPhoneNumber.Text.Trim()))
                {
                    DisplayAlert("Validation Failed!", "the Phone Number is required", "OK");
                    TxtPhoneNumber.Focus();
                    return false;
                }
            }

            return R;
        }
    }
}