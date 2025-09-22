using GRF_AppCRC.Api.Historico.Domain.Interfaces;
using GRF_AppCRC.Api.Historico.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRF_AppCRC.Api.Historico.Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly DapperContext _context;
        private readonly string _tableName;

        public GenericRepository(DapperContext context)
        {
            _context = context;
            _tableName = typeof(T).Name + "s"; // Convenção simples (Customer -> Customers)
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            var sql = $"SELECT * FROM {_tableName} WHERE Id = @Id";
            return await _context.Connection.QueryFirstOrDefaultAsync<T>(sql, new { Id = id }, _context.Transaction);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            var sql = $"SELECT * FROM {_tableName}";
            return await _context.Connection.QueryAsync<T>(sql, transaction: _context.Transaction);
        }

        public async Task AddAsync(T entity)
        {
            // Para simplificar, usa Dapper.Contrib ou Reflection
            var props = typeof(T).GetProperties().Where(p => p.Name != "Id");
            var columns = string.Join(",", props.Select(p => p.Name));
            var values = string.Join(",", props.Select(p => "@" + p.Name));

            var sql = $"INSERT INTO {_tableName} ({columns}) VALUES ({values})";
            await _context.Connection.ExecuteAsync(sql, entity, _context.Transaction);
        }

        public async Task UpdateAsync(T entity)
        {
            var props = typeof(T).GetProperties().Where(p => p.Name != "Id");
            var setClause = string.Join(",", props.Select(p => $"{p.Name} = @{p.Name}"));

            var sql = $"UPDATE {_tableName} SET {setClause} WHERE Id = @Id";
            await _context.Connection.ExecuteAsync(sql, entity, _context.Transaction);
        }

        public async Task DeleteAsync(int id)
        {
            var sql = $"DELETE FROM {_tableName} WHERE Id = @Id";
            await _context.Connection.ExecuteAsync(sql, new { Id = id }, _context.Transaction);
        }
    }
