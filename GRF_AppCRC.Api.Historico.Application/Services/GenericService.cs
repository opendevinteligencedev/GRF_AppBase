using GRF_AppCRC.Api.Historico.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRF_AppCRC.Api.Historico.Application.Services
{
    public class GenericService<T> where T : class
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<T> _repository;
        private readonly ILogService _logger;

        public GenericService(IUnitOfWork unitOfWork, IGenericRepository<T> repository, ILogService logger)
        {
            _unitOfWork = unitOfWork;
            _repository = repository;
            _logger = logger;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            _logger.Info($"Buscando todos os registros de {typeof(T).Name}");
            return await _repository.GetAllAsync();
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            _logger.Info($"Buscando {typeof(T).Name} pelo Id {id}");
            return await _repository.GetByIdAsync(id);
        }

        public async Task AddAsync(T entity)
        {
            _logger.Info($"Adicionando novo {typeof(T).Name}");
            await _repository.AddAsync(entity);
            await _unitOfWork.CommitAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            _logger.Info($"Atualizando {typeof(T).Name}");
            await _repository.UpdateAsync(entity);
            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteAsync(int id)
        {
            _logger.Info($"Removendo {typeof(T).Name} Id {id}");
            await _repository.DeleteAsync(id);
            await _unitOfWork.CommitAsync();
        }
    }
}
