using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProEventos.Domain;
using ProEventos.Persistence.Contextos;
using ProEventos.Persistence.Contratos;

namespace ProEventos.Persistence
{
    public class GeralPersist : IGeralPersist
    {
        public ProEventosContext Context { get; }
        public GeralPersist(ProEventosContext context) //CONSTRUCTOR
        {
            this.Context = context;
            
        }
        public void Add<T>(T entity) where T : class
        {
            Context.Add(entity);
        }

        public void Update<T>(T entity) where T : class
        {
            Context.Update(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            Context.Remove(entity);
        }

        public void DeleteRange<T>(T[] entityArray) where T : class
        {
            Context.RemoveRange(entityArray);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await Context.SaveChangesAsync()) > 0;
        }
 
    }
}