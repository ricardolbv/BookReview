using BookReview.Application.Contracts.Infraestructure;
using BookReview.Infraestructure.FileExporter;
using BookReview.Infraestructure.ServiceBus;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookReview.Infraestructure
{
    public static class InfraServiceRegistration
    {
        public static IServiceCollection AddInfraServices(this IServiceCollection services)
        {
            services.AddTransient<ICsvExporter, CsvExporter>();
            services.AddSingleton<IMessageBus, AzServiceBusMessageSender>();

            return services;
        }
    }
}
