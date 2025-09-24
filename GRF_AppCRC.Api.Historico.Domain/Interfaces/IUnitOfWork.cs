namespace GRF_AppCRC.Api.Historico.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<T> Repository<T>() where T : class;
        Task CommitAsync();
        Task RollbackAsync();
    }
}
