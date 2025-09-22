namespace GRF_AppCRC.Api.Historico.Domain.Interfaces
{
    public interface ILogService
    {
        void Info(string message);
        void Error(string message, Exception? ex = null);
    }
}
