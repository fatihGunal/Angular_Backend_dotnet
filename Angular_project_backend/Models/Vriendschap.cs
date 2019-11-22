using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Angular_project_backend.Models
{
    public class Vriendschap
    {
        public int VriendschapID { get; set; }
        public int GebruikerEenID { get; set; }
        public int GebruikerTweeID { get; set; }
        public int Status { get; set; }

        
        public virtual Gebruiker GebruikerEen { get; set; }
        public virtual Gebruiker GebruikerTwee { get; set; }
    }
}
