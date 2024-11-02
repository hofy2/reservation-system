using Application.Reservations.Commands.CreateReservation;
using Application.Reservations.Common;
using Application.Reservations.Common.DTOs;
using Domin.Entities;
using Domin.interfaces;
using FluentValidation;
using Infrastructure.Reposatry;
using MediatR;
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
      //      services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            services.AddMediatR(cfg => {
                cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
 
                cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            });
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            return services;

        }


    }
}
