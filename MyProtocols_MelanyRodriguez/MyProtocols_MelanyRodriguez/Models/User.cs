using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;

namespace MyProtocols_MelanyRodriguez.Models
{
   public class User
    {
        //es mala idea tener un solo objeto de comunicacion contra el API recomiendo tener
        //uno por cada clase se comunique con el API

     public RestRequest Request { get; set; }



        //en este ejemplo usare los mismos atributos que en el modelo API posteriormente
        //en otra clase usare un DTO del usuario para simplificar el json que se envian y resibe en el API


        public int UserId { get; set; }
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string BackUpEmail { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string? Address { get; set; }
        public bool? Active { get; set; }
        public bool? IsBlocked { get; set; }
        public int UserRoleId { get; set; }

        public virtual UserRole? UserRole { get; set; } 

        public User ()
        {

        }

        //funciones especificas de llamada a end points del API

        //funcion que permite validar que los datos digitados en la pagina del applogin 
        //sean correctos o no

        public async Task<bool> ValidateUserLogin()
        {
            try
            {
                //usaremos el prefijo de la URL del API que se indica en services/APIConection
                //para agregar el sufijo y lograr la ruta completa de consumo del end point
                //que se quiere usar

                string RouteSufix = string.Format("", this.Email, this.Password);

                //armamos la ruta completa al endpoint en el API 
                string URL = Services.APIConection.ProductionPrefijxURL + RouteSufix;

                RestClient client = new RestClient(URL);

                Request = new RestRequest(URL, Method.Get);

                //agregamos mecanismos de seguridad, en este caso API Key
                Request.AddHeader(Services.APIConection.ApiKeyName, Services.APIConection.ApiKeyValue);

                //ejecutar la llamada al API
                RestResponse response = await client.ExecuteAsync(Request);

                //saber si las cosas salieron bien
                HttpStatusCode statusCode = response.StatusCode;

                if(statusCode == HttpStatusCode.OK)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            catch (Exception ex)
            {
                string message = ex.Message;
                throw;
            }
        }
    }
}
