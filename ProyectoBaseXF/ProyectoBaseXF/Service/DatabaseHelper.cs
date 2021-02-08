using Microsoft.EntityFrameworkCore;
using ProyectoBaseXF.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoBaseXF.Service
{
    public class DatabaseHelper<T> where T : PBDbContext
    {
        protected PBDbContext CrateContext()
        {
            PBDbContext context = (T)Activator.CreateInstance(typeof(T));
            //rfsContext.Database.EnsureCreated();
            try
            {
                context.Database.Migrate();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return context;
        }

        public void DeleteDatabase()
        {
            using PBDbContext context = (T)Activator.CreateInstance(typeof(T));
            context.Database.EnsureDeleted();
        }

    }
}
