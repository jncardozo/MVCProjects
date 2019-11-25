using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public interface IGenericRepository<T> /*: IDisposable where T : class*/
    {
        IEnumerable<T> GetAll();
        T GetById(object id);
        T GetByUserNameAndPass(T obj);
        void Insert(T obj);
        void Update(T obj);
        void Delete(object id);
        void Save();
    }
}
