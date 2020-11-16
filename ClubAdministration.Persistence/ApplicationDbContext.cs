using System;
using System.Diagnostics;
using ClubAdministration.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ClubAdministration.Persistence
{
    public class ApplicationDbContext: DbContext
    {
        public DbSet<Member> Members { get; set; }
        public DbSet<Section> Sections { get; set; }
        public DbSet<MemberSection> MemberSections { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Environment.CurrentDirectory)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            var configuration = builder.Build();
            Debug.Write(configuration.ToString());
            string connectionString = configuration["ConnectionStrings:DefaultConnection"];
            optionsBuilder.UseSqlServer(connectionString);

        }
    }
}
