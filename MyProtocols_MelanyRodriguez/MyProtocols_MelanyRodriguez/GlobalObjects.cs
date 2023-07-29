using MyProtocols_MelanyRodriguez.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyProtocols_MelanyRodriguez
{
    public static class GlobalObjects
    {
        //definimos las propiedades de sodificacion de los json que usaremos en los modelos
        public static string MimeType = "aplication/json";
        public static string ContentType = "Content-Type";

        //crear el objeto local (global) de usuario
        public static UserDTO MyLocalUser = new UserDTO();

    }
}
