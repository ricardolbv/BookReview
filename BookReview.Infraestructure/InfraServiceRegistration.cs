using BookReview.Application.Contracts.Infraestructure;
using BookReview.Infraestructure.FileExporter;
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

            return services;
        }
    }
}
