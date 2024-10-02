using Library.Data.Entities;
using Library.Data.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Library.Data.Context
{
    public class LibraryDbContext : DbContext, IContext
    {

        public LibraryDbContext(DbContextOptions<LibraryDbContext> options) : base(options)
        {

        }

        public virtual DbSet<Books> Books { get; set; }

    }
}
