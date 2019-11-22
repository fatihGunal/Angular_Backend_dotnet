using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Angular_project_backend.Models
{
    public class Antwoord
    {
        public int AntwoordID { get; set; }
        public string AntwoordPoll { get; set; }
        public int PollID { get; set; }

        public Poll Poll { get; set; }
        public ICollection<Stem> Stemmen { get; set; }
    }
}
