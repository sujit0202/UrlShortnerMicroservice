using static System.Net.Mime.MediaTypeNames;
using System;
using Microsoft.EntityFrameworkCore;
using UrlShortnerMicroservice.Model;

namespace UrlShortnerMicroservice.Data
{
    /// <summary>
    /// Database context class for CRUD operation on DB using EF.
    /// </summary>
    public class UrlShortnerContext : DbContext
    {
        /// <summary>
        /// Represent Url mappings table in the database.
        /// </summary>
        public DbSet<UrlMapping> UrlMappings { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Data Source=MSI\\MSSQLSERVER01;Integrated Security=True; Initial Catalog=UrlShortnerDb; Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
        }
    }
}
