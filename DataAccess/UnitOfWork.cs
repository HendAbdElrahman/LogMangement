using IDataAccess;
using System.Data.Entity;

namespace DataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        private DbContextTransaction _transaction;

        public DbContext DbContext { get; private set; }

        public UnitOfWork()
        {
            DbContext = new LoggerDBEntities();
        }

        public void BeginTransaction()
        {
            _transaction = DbContext.Database.BeginTransaction();
        }

        public void Commit()
        {
            _transaction?.Commit();
        }

        public void Rollback()
        {
            _transaction?.Rollback();
        }

        public void Dispose()
        {
            DbContext?.Dispose();
        }
    }
}
