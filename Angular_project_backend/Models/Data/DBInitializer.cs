using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Angular_project_backend.Models.Data
{
    public class DBInitializer
    {
        public static void Initialize(ApiContext context)
        {
            context.Database.EnsureCreated();

            if (context.Gebruikers.Any())
            {
                return;
            }

            context.Gebruikers.AddRange(
                new Gebruiker { Email = "Fatihgunal2018@gmail.com", Wachtwoord = "admin", Gebruikersnaam = "admin"},
                new Gebruiker { Email = "murat@gmail.com", Wachtwoord = "murat", Gebruikersnaam = "murat" },
                new Gebruiker { Email = "Mehmet@gmail.com", Wachtwoord = "mehmet", Gebruikersnaam = "mehmet" },
                new Gebruiker { Email = "yasin@gmail.com", Wachtwoord = "yasin", Gebruikersnaam = "yasin" }
            );

            context.SaveChanges();
        }
    }
}
