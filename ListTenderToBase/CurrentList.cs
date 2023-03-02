using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListTenderToBase
{
    internal class CurrentList
    {
        public class ApplicationContext : DbContext
        {
            public DbSet<Procurement> Procurements { get; set; } = null!;
            public DbSet<Platform> Platforms { get; set; } = null!;
            public DbSet<Organization> Organizations { get; set; } = null!;
            public DbSet<TimeZone> TimeZones { get; set; } = null!;
            public DbSet<Act> Acts { get; set; } = null!;
            public DbSet<Method> Methods { get; set; } = null!;

            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                optionsBuilder.UseSqlServer(@"Server=ngknn.ru;Database=BaseForGraduationProject;User ID = 33П; Password = 12357; TrustServerCertificate = true");
            }
        }
    }
}
