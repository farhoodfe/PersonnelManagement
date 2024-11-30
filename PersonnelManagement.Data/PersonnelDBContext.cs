using Microsoft.EntityFrameworkCore;
using PersonnelManagement.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonnelManagement.Data
{
    public class PersonnelDBContext : DbContext
    {
        public PersonnelDBContext(DbContextOptions<PersonnelDBContext> options) : base(options)
        {

        }

        public DbSet<DynamicFieldDefinition> Fields { get; set; }
        public DbSet<FieldSubmission> Submissions { get; set; }
        public DbSet<PersonInfo> PersonInfos { get; set; }
        public DbSet<Formula> Formulas { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //setting DB Connection string
            optionsBuilder.UseSqlServer(@"server=localhost; Database=Personnel;Trusted_Connection = True;");
            //optionsBuilder.UseLazyLoadingProxies();

        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(PersonnelDBContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
