using App.Data.Entities;
using App.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace App.Data
{
    public class SampleDbContext : DbContext, ISampleDbContext
    {
        public DbSet<Person> Persons { get; set; }
        public DbSet<Hobby> Hobbies { get; set; }
        public DbSet<Pet> Pets { get; set; }
    }
}
