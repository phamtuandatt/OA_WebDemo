using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using OA.Dataa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Repo
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        // Create DbContext to get tables in DB
        private readonly ApplicationContext _context;
        // Get Table
        private DbSet<T> entities;

        public Repository(ApplicationContext applicationContext)
        {
            this._context = applicationContext;
            entities = _context.Set<T>();
        }
        public void Delete(T entity)
        {   
            entities.Remove(entity);
            _context.SaveChanges();
        }

        public T Get(long id)
        {
            // Get Object
            return entities.Find(id);
        }

        public IEnumerable<T> GetAll()
        {
            // Return List T -> Ex: T : Employee => IEnumertable<Employee>
            return entities.AsEnumerable();
        }

        public void Insert(T entity)
        {
            entities.Add(entity);
            _context.SaveChanges();
        }

        public void Remove(T entity)
        {
            entities.Remove(entity);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public void Update(T entity)
        {
            _context.Update(entity);
            _context.SaveChanges();
        }
    }
}
