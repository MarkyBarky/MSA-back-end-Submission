using Microsoft.EntityFrameworkCore;
using MSA.backend.Api.Model;
using MSA.backend.Model;

namespace MSA.backend.Data
{
    public interface IWebAPIDBContext
    {
        DbSet<pokemon> pokemons { get; set; }
        int SaveChanges();
        DbSet<Move> moves { get; set; }
    }
}