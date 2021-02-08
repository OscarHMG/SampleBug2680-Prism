using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ProyectoBaseXF.Data
{
    public class PBDbContext : DbContext
    {
        
        private const string databaseName = "pb.db";

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            String databasePath = "";
            databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), databaseName);
            // Specify that we will use sqlite and the path of the database here
            optionsBuilder.UseSqlite($"Filename={databasePath}");
        }

    }
}
