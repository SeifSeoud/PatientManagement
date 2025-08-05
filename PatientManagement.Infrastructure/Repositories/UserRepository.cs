using PatientManagement.Core.DTO;
using PatientManagement.Core.Entities;
using PatientManagement.Core.RepositoryContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientManagement.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    public async Task<ApplicationUser?> AddUser(ApplicationUser user)
    {
        throw new NotImplementedException();
    }

    public async Task<ApplicationUser?> GetUserByEmailAndPassword(string? email, string? password)
    {
        return new ApplicationUser()
        {
            UserID = Guid.NewGuid(),
            Email = email,
            Password = password,
            PersonName="Person name",
            Gender=GenderOptions.Male.ToString(),
        };
    }
}
