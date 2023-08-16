using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using System.Net;
using System.Threading.Tasks;

namespace MyProtocols_MelanyRodriguez.Models
{
    public class UserDTO
    {
        [JsonIgnore]
        public RestRequest Request { get; set; }
        public int UsuarioId { get; set; }
        public string Correo { get; set; } = null!;
        public string Contrasenia { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public string RespaldoCorreo { get; set; } = null!;
        public string Telefono { get; set; } = null!;
        public string? Direccion { get; set; }
        public bool? Activo { get; set; }
        public bool? EstaBloqueado { get; set; }
        public int IDRolUsuario { get; set; }
        public string DescripcionRol { get; set; } = null!;

        //funciones 
         public async Task<UserDTO> GetUserInfo(string PEmail)
        {
            try
            {

                string RouteSufix = string.Format("Users/GetUserInfoByEmail?Pemail={0}",PEmail);

                //armamos la ruta completa al endpoint en el API 
                string URL = Services.APIConection.ProductionPrefijxURL + RouteSufix;

                RestClient client = new RestClient(URL);

                Request = new RestRequest(URL, Method.Get);

                //agregamos mecanismos de seguridad, en este caso API Key
                Request.AddHeader(Services.APIConection.ApiKeyName, Services.APIConection.ApiKeyValue);
                Request.AddHeader(GlobalObjects.ContentType, GlobalObjects.MimeType);

                //ejecutar la llamada al API
                RestResponse response = await client.ExecuteAsync(Request);

                //saber si las cosas salieron bien
                HttpStatusCode statusCode = response.StatusCode;

                if (statusCode == HttpStatusCode.OK)
                {
                    //en el api dise;amos el end point de forma que retorne un 
                    //list<UserDTO>, pero esta funcion retorna solo un objeto de
                    //UserDTO, por lo tanto debemos seleccionar de la lista 
                    //el item con indix 0
                    var list = JsonConvert.DeserializeObject<List<UserDTO>>(response.Content);
                    var Item = list[0];

                    return Item;
                }
                else
                {
                    return null;
                }
            }

            catch (Exception ex)
            {
                string message = ex.Message;
                throw;
            }
        }
        public async Task<bool> UpdateUserAsync()
        {
            try
            {

                string RouteSufix = string.Format("Users/{0}",this.UsuarioId);

                //armamos la ruta completa al endpoint en el API 
                string URL = Services.APIConection.ProductionPrefijxURL + RouteSufix;

                RestClient client = new RestClient(URL);

                Request = new RestRequest(URL, Method.Put);

                //agregamos mecanismos de seguridad, en este caso API Key
                Request.AddHeader(Services.APIConection.ApiKeyName, Services.APIConection.ApiKeyValue);

                //en el caso de una operacion POST debemos serializarel objeto para pasarlo 
                //como json al API
                string SerializedModelObject = JsonConvert.SerializeObject(this);

                //agregamos el objeto serializado el cuerpo del request
                Request.AddBody(SerializedModelObject);

                //ejecutar la llamada al API
                RestResponse response = await client.ExecuteAsync(Request);

                //saber si las cosas salieron bien
                HttpStatusCode statusCode = response.StatusCode;

                if (statusCode == HttpStatusCode.OK)
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

