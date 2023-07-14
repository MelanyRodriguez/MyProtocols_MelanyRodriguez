using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using MyProtocols_MelanyRodriguez.ViewModels;
using Acr.UserDialogs;

namespace MyProtocols_MelanyRodriguez.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AppLoginPage : ContentPage
    {
        //se realiza el anclaje entre esta vista y el VM que le da la funcionalidad

        UserViewModel viewModel;
        public AppLoginPage()
        {
            InitializeComponent();
             
            //esto vincula la vista con el viewmodel y ademas crea la instancia del obj
            this.BindingContext = viewModel = new UserViewModel();
        }

        private void SwShowPassword_Toggled(object sender, ToggledEventArgs e)
        {
            if (SwShowPassword.IsToggled)
            {
                TxtPassword.IsPassword = false;
            }
            else
            {
                TxtPassword.IsPassword = true;
            }
        }

        private async void BtnLogin_Clicked(object sender, EventArgs e)

        {
            //validacion de ingreso del usuario a la app

            if (TxtUserName.Text!=null && !string.IsNullOrEmpty(TxtUserName.Text.Trim()) &&
               TxtPassword.Text != null && !string.IsNullOrEmpty(TxtPassword.Text.Trim()))

            {
                //si hay info en los cuadros de texto de email y pass se procede 
                try
                {

                    //hacemos una animacion de espera
                    UserDialogs.Instance.ShowLoading("Cheking User Access...");
                    await Task.Delay(2000);


                    string username = TxtUserName.Text.Trim();
                    string password = TxtPassword.Text.Trim();
                    
                    bool R = await viewModel.UserAccessValidation(username, password);

                    if (R)
                    {
                        //si la validacion es correcta permite el ingreso al sistema 
                        await Navigation.PushAsync(new StarPage());
                    }
                    else
                    {
                        //algo salio mal

                        await DisplayAlert("User Acced Dinied", "username or password are incorrect", "OK");
                        return;
                    }
                }
                catch (Exception)
                {
                    throw;
                }

                finally
                {
                    //apagamos la animacion de carga
                    UserDialogs.Instance.HideLoading();
                }

            }

            else
            {
                // si no digito datos indicarle al usuario del requerimiento 
                await DisplayAlert("Data Requeries", "username and password are requeried...", "OK");
                return;
            }


        }

        private async void BtnSingUp_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new UserSingUpPage());
        }
    }
}