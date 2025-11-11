using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreSystem.Core.Interfaces
{
    public interface IUniteOfWork : IDisposable
    {
        Task<int> CompleteAsync();

        void BeginTransaction();
        void Commit();
        void Rollback();
        
    }
}