using System;
using System.Collections;
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
        public UserRole MyUserRole { get; set; }
        public UserDTO MyUserDTO { get; set; }
        public UserViewModel() 
        { 
            MyUser = new User();
            MyUserRole = new UserRole();
            MyUserDTO = new UserDTO();
        }

        //funciones 

        //funcion que carga los datos del objeto del usuario global
        public async Task<UserDTO> GetUserDataAsync(string PEmail)
        {
            if (IsBusy) return null;
            IsBusy = true;

            try
            {
                UserDTO userDTO = new UserDTO();
                userDTO= await MyUserDTO.GetUserInfo(PEmail);
                if (userDTO == null) return null;
                return userDTO;
              
            }
            catch (Exception)
            {
                return null;
                throw;
            }
            finally { IsBusy = false; }
        }

        public async Task<bool> UpdateUser(UserDTO pUser)
        {
            if (IsBusy) return false;
            IsBusy = true;

            try
            {
                MyUserDTO = pUser;
                bool R = await MyUserDTO.UpdateUserAsync();
                
                return R;

            }
            catch (Exception)
            {
                return false;
                throw;
            }
            finally { IsBusy = false; }
        }


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
        //carga la lista de roles, que se usaran en el picker de roles en la creacion
        //de un usuario nuevo
        public async Task<List<UserRole>> GetUserRolesAsync()
        {
            try
            {
                List<UserRole> roles = new List<UserRole>();

                roles= await MyUserRole.GetAllUserRoleAsync();

                if (roles==null)
                {
                    return null;
                }

                return roles; 

            }
            catch (Exception)
            {

                throw;
            }

        }
        public async Task<bool> AddUserAsync(string pEmail,
                                             string pPassword,
                                             string pName,
                                             string pBackUpEmail,
                                             string pPhoneNumber,
                                             string pAddress,
                                             int pUserRoleID)
        { 
            if (IsBusy) return false;
            IsBusy = true;

            try
            {
                MyUser = new User();

                MyUser.Email = pEmail;
                MyUser.Password = pPassword;
                MyUser.Name = pName;
                MyUser.BackUpEmail = pBackUpEmail;
                MyUser.PhoneNumber = pPhoneNumber;
                MyUser.Address = pAddress;
                MyUser.UserRoleId = pUserRoleID;

                bool R = await MyUser.AddUserAsync();

                return R;

            }
            catch (Exception)
            {

                throw;
            }
            finally { IsBusy = false; }
        }

        public async Task<bool> UpdatePasswordUser(UserDTO pPassword)
        {
            if (IsBusy) return false;
            IsBusy = true;

            try
            {
                MyUserDTO = pPassword;
                bool R = await MyUserDTO.UpdatePasswordUserAsync();

                return R;

            }
            catch (Exception)
            {
                return false;
                throw;
            }
            finally { IsBusy = false; }
        }






    }
}
