using System;
using System.Collections.Generic;
using System.Text;

namespace MyProtocols_MelanyRodriguez.Services
{
    public static class APIConection
    {
        //aca definimos la direccion URL a la que el app debe apuntar, por comodidad la ruta 
        //URL completa para consumir los recursos del API se hara en formato prefijo+sufijo
        //donde el prefijo sera la parte del URL que no cambiara y el sufijo sera la parte que si cambuara

        public static string ProductionPrefijxURL = "http://192.168.100.146:4545/api/";
        public static string TestingPrefijxURL = "http://192.168.100.146:4545/api/";

        public static string ApiKeyName = "Progra6ApiKey";
        public static string ApiKeyValue = "MelanyProgra6Aszxc12345";
    }
}
