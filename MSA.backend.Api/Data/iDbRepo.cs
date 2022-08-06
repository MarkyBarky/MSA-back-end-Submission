using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MSA.backend.Api.Model;



namespace MSA.backend.Data
{
    public interface iDbRepo
    {
        
        //pokemon GetPokemonByName(string name);
        void SaveChanges();

        Move addMoves(Move moves);

        Move GetMoveByName(string name);
        
    }
}