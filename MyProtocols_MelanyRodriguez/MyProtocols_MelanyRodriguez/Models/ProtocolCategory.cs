using System;
using System.Collections.Generic;
using System.Text;

namespace MyProtocols_MelanyRodriguez.Models
{
    public class ProtocolCategory
    {
        public ProtocolCategory()
        {
            //Protocols = new HashSet<Protocol>();
        }

        public int ProtocolCategory1 { get; set; }
        public string Description { get; set; } = null!;
        public int UserId { get; set; }

    }
}

