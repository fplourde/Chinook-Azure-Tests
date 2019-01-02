using System;
using Microsoft.EntityFrameworkCore;

namespace Albums.Database
{
    public class ChinookDatabase : DbContext
    {
        public DbSet<Album> Albums { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            

            var path = AppDomain.CurrentDomain.BaseDirectory;


            optionsBuilder.UseSqlite($"Data Source={path}\\chinook.db");
        }
    }
}