using Dominio;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositorio
{
    public class RepositorioClass : IRepositorio
    {
        public readonly ProcessamentoContext context;
        public RepositorioClass(ProcessamentoContext _context)
        {
            context = _context;
        }

        public void Add<T>(T entity) where T : class
        {
            context.Add(entity);
        }

        public void Update<T>(T entity) where T : class
        {
            context.Update(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            context.Remove(entity);
        }

        public async Task<bool> SaveChangeAsync()
        {
            return (await context.SaveChangesAsync()) > 0;
        }

        public async Task<Processamento[]> GetUltimoProcessamento()
        {
            IQueryable<Processamento> query = context.Processamento.Where(p => p.Status == 0).AsNoTracking().OrderByDescending(p => p.Id);

            return await query.ToArrayAsync();
        }
    }
}
