using Microsoft.EntityFrameworkCore;
using PhoneBookPoC.Entities;
using System;

namespace PhoneBookPoC.DataAcess.DbContexts
{
    public class PhoneBookDbContext : DbContext
    {
        public DbSet<Person> People { get; set; }

        private readonly string _dbPath;

        public PhoneBookDbContext(IDbConfiguration dbConfiguration)
        {
            _dbPath = dbConfiguration.GetPath();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Filename={_dbPath}");
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Person>()
                .Ignore(p => p.FullName)
                .Ignore(p => p.PhoneNumber)
                .HasData(
                new Person()
                {
                    Id = Guid.Parse("d28888e9-2ba9-473a-a40f-e38cb54f9b35"),
                    FirstName = "Pawel",
                    LastName = "Rudnicki",
                    Phone = "+48 721 310 342"
                });

            base.OnModelCreating(builder);
        }
    }
}
