using Angular_project_backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Angular_project_backend.Services
{
    public interface IGebruikerService
    {
        Gebruiker Authenticate(string Email, string Gebruikersnaam);
    }
}
