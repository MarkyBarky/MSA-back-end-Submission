using Microsoft.EntityFrameworkCore;
using MSA.backend.Api.Model;


namespace MSA.backend.Data
{
    public interface IWebAPIDBContext
    {
        
        int SaveChanges();
        DbSet<Move> moves { get; set; }
    }
}