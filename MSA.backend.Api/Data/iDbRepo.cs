using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MSA.backend.Api.Model;
using MSA.backend.Model;


namespace MSA.backend.Data
{
    public interface iDbRepo
    {
        IEnumerable<pokemon> GetPokemon();
        pokemon GetPokemonByName(string name);
        void SaveChanges();

        Move addMoves(Move moves);
        
    }
}