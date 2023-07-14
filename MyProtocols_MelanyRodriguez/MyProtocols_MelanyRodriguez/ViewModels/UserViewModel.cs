using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MyProtocols_MelanyRodriguez.Models;

namespace MyProtocols_MelanyRodriguez.ViewModels
{
    public class UserViewModel: BaseViewModel
    {
        //el VM funciona como puente entre el modelo y la vista en sentido teorico el vm
        //"siente" los cambios de la vista y los pasa al modelo de forma automatica o viseversa 
        //segun se use en uno o dos sentidos. Tambien se puede usar como mediador de procesos
        //mas adelante se usara commands y blings en dos sentidos 

        //primero en formatos de funciones clasicas

        public User MyUser { get; set; }

        public UserViewModel() 
        { 
            MyUser = new User();
        }

        //funciones 

        //funcion para validar el ingreso del usuario al app por medio del login

        public async Task<bool> UserAccessValidation(string pEmail, string pPassword)
        {
            //debemos controlar que no se ejecute la operacion mas de una vez en este caso 
            //hay una funcionalidad para eso en BaseViewModel que fue heredada al definie
            //esta clase. Se usara una propiedad llamada "IsBusy" para indicar que esta en 
            //proceso de ejecucion mientras su valor sea verdadero

            //control de bloqueo de funcionalidad
            if (IsBusy) return false;
            IsBusy = true;

            try
            {
                MyUser.Email = pEmail;
                MyUser.Password = pPassword;

                bool R = await MyUser.ValidateUserLogin();

                return R;
            }
            catch (Exception ex)
            {
                string msg = ex.Message;

                return false;

                throw;
            }
            finally 
            
            {
                IsBusy = false;
            }
        }

    }
}
