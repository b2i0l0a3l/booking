using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingSystem.Infrastructure.presistence;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using StoreSystem.Core.Interfaces;

namespace StoreSystem.Infrastructure.presistence
{
    public class UniteOfWork : IUniteOfWork
    {
        private readonly AppDbContext _context;
        private ILogger<UniteOfWork> _Logger;
        private IDbContextTransaction? _transaction;
        public UniteOfWork(AppDbContext context,ILogger<UniteOfWork> Logger)
        {
            _Logger = Logger;
            _context = context;
        }


        public void BeginTransaction()
        {
            _transaction = _context.Database.BeginTransaction();
                _Logger.LogInformation("BeginTransaction");

        }

        public void Commit()
        {
            try
            {
                _context.SaveChanges();
                _transaction?.Commit();
                _Logger.LogInformation("Commit");
            }
            catch
            {

                Rollback();
                throw;
            }
            finally
            {
                _transaction?.Dispose();
            }

        }

        public async Task<int> CompleteAsync()
        =>
              await _context.SaveChangesAsync();
        

        public void Dispose()
        {
            _transaction?.Dispose();
            _context.Dispose();
            _Logger.LogInformation("Dispose");

        }

        public void Rollback()
        {
            _transaction?.Rollback();
            _transaction?.Dispose();
            _Logger.LogError("Rollback");

        }
    }
}