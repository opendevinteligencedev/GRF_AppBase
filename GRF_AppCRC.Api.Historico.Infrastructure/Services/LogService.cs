using GRF_AppCRC.Api.Historico.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRF_AppCRC.Api.Historico.Infrastructure.Services
{
    public class LogService : ILogService
    {
        private readonly ILogger<LogService> _logger;

        public LogService(ILogger<LogService> logger)
        {
            _logger = logger;
        }

        public void Info(string message)
        {
            _logger.LogInformation(message);
        }

        public void Error(string message, Exception? ex = null)
        {
            _logger.LogError(ex, message);
        }
    }
}
