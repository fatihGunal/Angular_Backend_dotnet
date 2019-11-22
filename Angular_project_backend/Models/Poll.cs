using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Angular_project_backend.Models
{
    public class Poll
    {
        public int PollID { get; set; }
        public string Titel { get; set; }
        public string Beschrijving { get; set; }

        public ICollection<PollGebruiker> PollGebruikers { get; set; }
        public ICollection<Antwoord> Antwoorden { get; set; }
    }
}
