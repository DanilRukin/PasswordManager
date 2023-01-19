using Microsoft.Extensions.DependencyInjection;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using PasswordManager.Application.Services.Interfaces;
using PasswordManager.Domain.DatabaseAgregate;
using PasswordManager.Application.Dtos;
using PasswordManager.Application.Services.Mappers;
using PasswordManager.Domain.GroupAgregate;
using PasswordManager.Domain.RecordEntity;

namespace PasswordManager.Application
{
    public static class ApplicationServices
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            return services.AddMediatR(typeof(ApplicationServices).GetTypeInfo().Assembly)
                .AddMappers();
        }

        private static IServiceCollection AddMappers(this IServiceCollection services)
        {
            return services.AddScoped<IMapper<Database, DatabaseDto>, DatabaseToDatabaseDtoMapper>()
                .AddScoped<IMapper<Group, GroupDto>, GroupToGroupDtoMapper>()
                .AddScoped<IMapper<Record, RecordDto>, RecordToRecordDtoMapper>();
        }
    }
}
