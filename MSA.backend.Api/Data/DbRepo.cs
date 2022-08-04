using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.AspNetCore.Mvc;
using MSA.backend.Model;

namespace MSA.backend.Data
{
    public class DbRepo : iDbRepo
    {
        private readonly IWebAPIDBContext _dbContext;

        public DbRepo(IWebAPIDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<pokemon> GetPokemon()
        {
            IEnumerable<pokemon> pokemons = _dbContext.pokemons.ToList<pokemon>();
            return pokemons;
        }
        public pokemon GetPokemonByName(string name)
        {
            pokemon pokemon = _dbContext.pokemons.FirstOrDefault(e => e.name == name);
            return pokemon;
        }

       

        

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }

    }
}
