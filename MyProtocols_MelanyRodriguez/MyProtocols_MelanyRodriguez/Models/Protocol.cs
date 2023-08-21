using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MyProtocols_MelanyRodriguez.Models
{
    public class Protocol
    {
        public Protocol()
        {
            //ProtocolStepProtocolSteps = new HashSet<ProtocolStep>();
        }
        [Newtonsoft.Json.JsonIgnore]
        public RestRequest Request { get; set; }
        public int ProtocolId { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public DateTime CreationDate { get; set; }
        public TimeSpan? AlarmHour { get; set; }
        public bool? AlarmActive { get; set; }
        public bool? AlarmJustInWeekDays { get; set; }
        public bool? Active { get; set; }
        public int UserId { get; set; }
        public int ProtocolCategory { get; set; }

        //public virtual ProtocolCategory? ProtocolCategoryNavigation { get; set; } = null!;
        //public virtual User? User { get; set; } = null!;

        public virtual ICollection<ProtocolStep> ProtocolStepProtocolSteps { get; set; }
        //Funciones del modelo
        public async Task<ObservableCollection<Protocol>> GetProtocolListByUserID()
        {
            try
            {

                string RouteSufix = string.Format("Protocols/GetProtocolListByUser?id={0}", this.UserId);

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
                    var list = JsonConvert.DeserializeObject<ObservableCollection<Protocol>>(response.Content);
                    var Item = list[0];

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

