using IDataAccess;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private IUnitOfWork _unitOfWork;
        public Repository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        protected DbContext dbContext { get { return _unitOfWork.DbContext; } }

        public IQueryable<T> GetAll()
        {
            return dbContext.Set<T>().AsQueryable();
        }

        public T Find(int id)
        {
            return dbContext.Set<T>().Find(id);
        }

        public T Add(T entity)
        {
            return dbContext.Set<T>().Add(entity);
        }

        public void Delete(T entity)
        {
            dbContext.Set<T>().Remove(entity);
        }

        public void Update(T entity)
        {
            dbContext.Set<T>().Attach(entity);
            dbContext.Entry(entity).State = EntityState.Modified;
        }


        public void SaveChanges()
        {
            dbContext.SaveChanges();
        }

        public void Dispose()
        {
            _unitOfWork?.Dispose();
        }

    }

}
