using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Angular_project_backend.Models
{
    public class Gebruiker
    {
        public int GebruikerID { get; set; }
        public string Email { get; set; }
        public string Wachtwoord { get; set; }
        public string Gebruikersnaam { get; set; }
        [NotMapped]
        public string token { get; set; }

        [InverseProperty("GebruikerEen")]
        public virtual ICollection<Vriendschap> GebruikersEen { get; set; }
        [InverseProperty("GebruikerTwee")]
        public virtual ICollection<Vriendschap> GebruikersTwee { get; set; }
        
        public ICollection<Stem> Stemmen { get; set; }
        public ICollection<PollGebruiker> PollGebruikers { get; set; }
        public ICollection<Poll> Polls { get; set; }
    }
}
