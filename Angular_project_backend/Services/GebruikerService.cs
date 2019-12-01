﻿using Angular_project_backend.Helpers;
using Angular_project_backend.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Angular_project_backend.Services
{
    public class GebruikerService : IGebruikerService
    {
        private readonly AppSettings _appSettings;
        private readonly ApiContext _apiContext;

        public GebruikerService(IOptions<AppSettings> appSettings, ApiContext apiContext)
        {
            _appSettings = appSettings.Value;
            _apiContext = apiContext; 
        }

        public Gebruiker Authenticate(string email, string wachtwoord)
        {
            var gebruiker = _apiContext.Gebruikers.SingleOrDefault(x => x.Email == email && x.Wachtwoord == wachtwoord);

            if (gebruiker == null)
            {
                return null;
            }

            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("GebruikerID", gebruiker.GebruikerID.ToString()),
                    new Claim("Email", gebruiker.Email),
                    new Claim("Gebruikersnaam", gebruiker.Gebruikersnaam)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            gebruiker.token = tokenHandler.WriteToken(token);

            //remove password before returning
            gebruiker.Wachtwoord = null;

            return gebruiker;
        }
    }
}
