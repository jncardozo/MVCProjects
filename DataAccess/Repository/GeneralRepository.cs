using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class GeneralReposity<T> : IGenericRepository<T> where T : class
    {
        private DatabaseFirstMovieDBEntities _context;
        private DbSet<T> _table;

        public GeneralReposity()
        {
            this._context = new DatabaseFirstMovieDBEntities();
            _table = _context.Set<T>();
        }

        public GeneralReposity(DatabaseFirstMovieDBEntities _context)
        {
            this._context = _context;
            _table = _context.Set<T>();
        }

        public void Delete(object id)
        {
            T existing = _table.Find(id);
            _table.Remove(existing);
        }

        //public void Dispose()
        //{
        //    throw new NotImplementedException();
        //}

        public IEnumerable<T> GetAll()
        {
            return _table.ToList();
        }

        public T GetById(object id)
        {            
            return _table.Find(id);            
        }

        public T GetByUserNameAndPass(T t)
        {            
            return _table.Find(t);            
        }

        public void Insert(T obj)
        {
            _table.Add(obj);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(T obj)
        {
            _table.Attach(obj);
            _context.Entry(obj).State = EntityState.Modified;
        }
    }
}
