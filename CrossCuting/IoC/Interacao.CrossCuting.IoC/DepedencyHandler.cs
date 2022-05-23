using Interacao.Domain.DAO;
using Interacao.Infrasctructure.DynamoDB.Repository;
using Interacao.Service.BusinessRules;
using Interacao.Service.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Interacao.CrossCuting.IoC
{
    public static class DepedencyHandler
    {
        public static void AddServicesDepedency(this IServiceCollection services)
        {
            services.AddScoped<IMedicamentoService, MedicamentoService>();
            services.AddScoped<IProntuarioService, ProntuarioService>();
            services.AddScoped<IMedicamentoRepository, MedicamentoRepository>();
        }

    }
}