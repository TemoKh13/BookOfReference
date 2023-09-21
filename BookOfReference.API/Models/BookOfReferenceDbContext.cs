using BookOfReference.API.DTO;
using Microsoft.EntityFrameworkCore;

namespace BookOfReference.API.Models
{
    public class BookOfReferenceDbContext : DbContext
    {
     public BookOfReferenceDbContext(DbContextOptions<BookOfReferenceDbContext> options) : base(options)
        {

        }
        public DbSet<Person> Person { get; set; }
        public DbSet<RelatedPerson> RelatedPeople { get; set; }
        

        
        
    }
}
