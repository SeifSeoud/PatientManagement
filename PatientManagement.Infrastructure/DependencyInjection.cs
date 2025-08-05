using Microsoft.Extensions.DependencyInjection;
using PatientManagement.Core.RepositoryContracts;
using PatientManagement.Infrastructure.DapperDb;
using PatientManagement.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientManagement.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection
        AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IPatientRepository, PatientRepository>();
        services.AddScoped<IUserRepository, UserRepository>();

        // Register DbContext
        services.AddScoped<DapperDbContext>();

        return services;
    }
}
