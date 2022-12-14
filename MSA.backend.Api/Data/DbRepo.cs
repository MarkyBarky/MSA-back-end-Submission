using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.ChangeTracking;

using MSA.backend.Api.Model;

namespace MSA.backend.Data
{
    public class DbRepo : iDbRepo
    {
        private readonly IWebAPIDBContext _dbContext;

        public DbRepo(IWebAPIDBContext dbContext)
        {
            _dbContext = dbContext;
        }


        public Move GetMoveByName(string name)
        {
            
            Move moves = _dbContext.moves.FirstOrDefault(e => e.move == name);
            if (moves == null)
            {
                return null;
            }
            return moves;
        }
        public Move addMoves(Move move)
        {
            EntityEntry<Move> e = _dbContext.moves.Add(move);
            Move c = e.Entity;
            _dbContext.SaveChanges();
            return c;
        }   

        public void deleteMoves(Move move)
        {
           
            _dbContext.moves.Remove(move);
            _dbContext.SaveChanges();
            
        }
        public Move updateMoves(Move move)
        {

            Move m = GetMoveByName(move.move);
            if (m != null)
            {
                m.name = move.name;
                _dbContext.SaveChanges();
            }
            return m;
        }

        

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }

    }
}
