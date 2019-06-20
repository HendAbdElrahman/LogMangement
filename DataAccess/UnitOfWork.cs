using IDataAccess;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
