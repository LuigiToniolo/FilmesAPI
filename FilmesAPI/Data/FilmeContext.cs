using FilmesAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FilmesAPI.Data
{
    public class FilmeContext : DbContext
    {

        public FilmeContext(DbContextOptions<FilmeContext> opt) : base(opt)
        {

        }   

        public DbSet<Filme> Filmes { get; set; } //Irá fazer o acesso dos dados desse banco
    }
}
