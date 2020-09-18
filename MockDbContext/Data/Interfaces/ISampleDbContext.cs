using App.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace App.Data.Interfaces
{
    public interface ISampleDbContext : IDbContext
    {
        DbSet<Person> Persons { get; set; }
        DbSet<Hobby> Hobbies { get; set; }
        DbSet<Pet> Pets { get; set; }
    }
}