using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Parcial1_apd1_20180906.Entidades;

namespace Parcial1_apd1_20180906.Dal
{
    public class Contexto : DbContext
    {
        public DbSet<Ciudad> Ciudad { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source = GestionCiudades.Db");
        }
    }
}
