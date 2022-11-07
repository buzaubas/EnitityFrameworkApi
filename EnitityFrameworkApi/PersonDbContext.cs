using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnitityFrameworkApi
{
    public class PersonDbContext : DbContext
    {
        public DbSet<PersonAddress> peopleAddresses { get; set; }
        public DbSet<Person> people { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.UseSqlServer("Server=223-10;Database=PeopleDb;" +
                "Trusted_Connection=true;Encrypt=false");
        }
    }
}
