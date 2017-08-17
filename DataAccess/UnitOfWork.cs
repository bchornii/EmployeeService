using System;
using DataAccess.Infrastructure;
using DataAccess.Repositories;

namespace DataAccess
{
    public interface IUnitOfWork : IDisposable
    {
        IEmployeeRepository EmployeeRepository { get; }
    }

    public class UnitOfWork : IUnitOfWork
    {
        private bool _disposed;

        private readonly INorthwindContext _northwindContext;
        private IEmployeeRepository _employeeRepository;

        public IEmployeeRepository EmployeeRepository => _employeeRepository ?? new EmployeeRepository(_northwindContext);

        public UnitOfWork(INorthwindDbContextFactory northwindDbContextFactory)
        {
            _northwindContext = northwindDbContextFactory.CreateContext();                        
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _northwindContext.Dispose();
                }
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}