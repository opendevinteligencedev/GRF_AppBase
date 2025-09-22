using FluentValidation;
using GRF_AppCRC.Api.Historico.Domain.Interfaces;
using GRF_AppCRC.Api.Historico.Infrastructure.Data;
using GRF_AppCRC.Api.Historico.Infrastructure.Repositories;
using GRF_AppCRC.Api.Historico.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace GRF_AppCRC.Api.Historico.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString)
        {
            // Dapper
            services.AddSingleton(new DapperContext(connectionString));

            // Repositórios
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            // Unit of Work
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            // Logging
            services.AddScoped<ILogService, LogService>();

            // Validadores
            services.AddScoped<IValidator<Application.DTOs.CustomerDto>, CustomerValidator>();

            return services;
        }
    }
}
