using Microsoft.Extensions.DependencyInjection;
using PatientManagement.Core.ServiceContracts;
using PatientManagement.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientManagement.Core;

public static class DependencyInjection
{
    public static IServiceCollection AddCore(this IServiceCollection services)
    {
        services.AddScoped<IUsersService, UsersService>();
        services.AddScoped<IJwtService, JwtService>();
        services.AddScoped<IPatientService, PatientService>();
        return services;
    }
}
