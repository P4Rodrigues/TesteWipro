using Dominio;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repositorio
{
    public class ProcessamentoContext : DbContext
    {
        public ProcessamentoContext(DbContextOptions<ProcessamentoContext> options) : base(options) {}
        public DbSet<Processamento> Processamento { get; set; }
        public DbSet<Moeda> Moeda { get; set; }


    }
}
