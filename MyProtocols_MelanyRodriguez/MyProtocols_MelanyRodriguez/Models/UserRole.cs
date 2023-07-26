using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MyProtocols_MelanyRodriguez.Models
{
    public class UserRole
    {
        public RestRequest Request { get; set; }
        public int UserRoleId { get; set; }
        public string Description { get; set; } = null!;

        //funciones
        public async Task<List<UserRole>> GetAllUserRoleAsync()
        {
            try
            {

                string RouteSufix = string.Format("UserRoles");

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

                if (statusCode == HttpStatusCode.OK)
                {
                    var list = JsonConvert.DeserializeObject<List<UserRole>>(response.Content);
                    return list;
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
    }
}
