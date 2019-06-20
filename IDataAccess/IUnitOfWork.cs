using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDataAccess
{
    public interface IUnitOfWork : IDisposable
    {
        DbContext DbContext { get; }
        void BeginTransaction();
        void Commit();
        void Rollback();
    }
}
