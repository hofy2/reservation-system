using Application.Reservations.Commands.CreateReservation;
using Domin.Entities;
using Domin.interfaces;
using FluentValidation;
using Infrastructure.Reposatry;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateReservationCommandHandler).Assembly));
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly()); // Registers all validators
            services.AddScoped<IGenericRepository<Reservation>, Repository<Reservation>>();

            return services;

        }


    }
}
