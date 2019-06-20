using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDataAccess
{
    public interface IRepository<T> : IDisposable where T : class
    {
        IQueryable<T> GetAll();
        T Find(int id);
        T Add(T entity);
        void Update(T entity);
        void SaveChanges();
        void Delete(T entity);
    }
}
