using System.Data;

namespace GRF_AppCRC.Api.Historico.Infrastructure.Data
{
    public class DapperContext
    {
        private readonly string _connectionString;
        private IDbConnection? _connection;
        private IDbTransaction? _transaction;

        public DapperContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IDbConnection Connection
        {
            get
            {
                if (_connection == null)
                {
                    _connection = new SqlConnection(_connectionString);
                    _connection.Open();
                }
                return _connection;
            }
        }

        public IDbTransaction? Transaction => _transaction;

        public void BeginTransaction()
        {
            if (_transaction == null)
                _transaction = Connection.BeginTransaction();
        }

        public void Commit()
        {
            _transaction?.Commit();
            DisposeTransaction();
        }

        public void Rollback()
        {
            _transaction?.Rollback();
            DisposeTransaction();
        }

        private void DisposeTransaction()
        {
            _transaction?.Dispose();
            _transaction = null;
        }
    }
}
