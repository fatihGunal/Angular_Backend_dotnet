using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Angular_project_backend.Models
{
    public class Stem
    {
        public int StemID { get; set; }
        public int AntwoordID { get; set; }
        public int GebruikerID { get; set; }

        public Gebruiker Gebruiker { get; set; }
        public Antwoord Antwoord { get; set; }
    }
}
