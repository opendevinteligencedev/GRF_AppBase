using GRF_AppCRC.Api.Historico.Domain.Interfaces;
using GRF_AppCRC.Api.Historico.Infrastructure.Data;

namespace GRF_AppCRC.Api.Historico.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DapperContext _context;

        public UnitOfWork(DapperContext context)
        {
            _context = context;
        }

        public IGenericRepository<T> Repository<T>() where T : class
        {
            return new Repositories.GenericRepository<T>(_context);
        }

        public async Task CommitAsync()
        {
            _context.Commit();
            await Task.CompletedTask;
        }

        public async Task RollbackAsync()
        {
            _context.Rollback();
            await Task.CompletedTask;
        }

        public void Dispose()
        {
            _context.Connection.Dispose();
        }
    }
}