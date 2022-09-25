using Microsoft.EntityFrameworkCore;
using TodoAPI.DataAccess.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TodoAPI.DataAccess.Concrete
{
    public class EFRepository<T> : IRepository<T> where T : class
    {
        private readonly AppDbContext _context;

        public EFRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Add(T entity)
        {
            var addedEntity = _context.Entry(entity);
            addedEntity.State = EntityState.Added;
            _context.SaveChanges();
        }

        public T Get(int id)
        {
            return _context.Set<T>().Find(id);
        }

        public List<T> GetAll(Expression<Func<T, bool>> filter = null, string properties = null)
        {
            IQueryable<T> entities = _context.Set<T>();

            if (filter != null)
            {
                entities = entities.Where(filter);
            }

            if (properties != null)
            {
                foreach (var property in properties.Split(new string[] { ",", ", " }, StringSplitOptions.RemoveEmptyEntries))
                {
                    entities = entities.Include(property);
                }
            }

            return entities.ToList();
        }

        public void Remove(T entity)
        {
            var deletedEntity = _context.Entry(entity);
            deletedEntity.State = EntityState.Deleted;
            _context.SaveChanges();
        }

        public void Update(T entity)
        {
            var updatedEntity = _context.Entry(entity);
            updatedEntity.State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
