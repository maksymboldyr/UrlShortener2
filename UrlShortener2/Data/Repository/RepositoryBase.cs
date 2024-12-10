using Microsoft.EntityFrameworkCore;
using UrlShortener2.Data.Entities;
using UrlShortener2.Data.Repository.Interfaces;

namespace UrlShortener2.Data.Repository
{
    public class RepositoryBase<T> : IRepository<T> where T : BaseEntity
    {
        protected readonly DbSet<T> _dbSet;
        protected readonly ApplicationDbContext _context;

        public RepositoryBase(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public void Add(T entity)
        {
            entity.CreateTimestamp();
            _dbSet.Add(entity);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            T? entity = _dbSet.Find(id) ?? throw new KeyNotFoundException();
            _dbSet.Remove(entity);
            _context.SaveChanges();
        }

        public IEnumerable<T> GetAll()
        {
            return [.. _dbSet];
        }

        public T GetById(int id)
        {
            return _dbSet.Find(id) ?? throw new KeyNotFoundException();
        }

        public void Update(T entity)
        {
            entity.UpdateTimestamp();
            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
