using PatientManagement.Core.DTO;
using PatientManagement.Core.Entities;
using PatientManagement.Core.RepositoryContracts;
using PatientManagement.Core.ServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientManagement.Core.Services;

public class UsersServices : IUsersService
{
    private readonly IUserRepository _userRepository;
    public UsersServices(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    public async Task<AuthenticationResponse> Login(LoginRequest loginRequest)
    {
       ApplicationUser? user =await _userRepository.GetUserByEmailAndPassword(loginRequest.Email, loginRequest.Password);
        if (user == null)
        {
            return null;
        }
        return new AuthenticationResponse
            (user.UserID, user.Email, user.PersonName,user.Gender,"token",Success:true);
    }

    public async Task<AuthenticationResponse?> Register(RegisterRequest registerRequest)
    {
        ApplicationUser user = new ApplicationUser()
        {
            PersonName = registerRequest.PersonName,
            Email = registerRequest.Email,
            Password = registerRequest.Password,
            Gender = registerRequest.Gender.ToString(),
        };
       ApplicationUser? registeredUser= await _userRepository.AddUser(user);
        if (registeredUser != null)
        {
            return null;
        }
        return new AuthenticationResponse(registeredUser.UserID,
            registeredUser.Email, registeredUser.PersonName,
            registeredUser.Gender,
            "token", Success: true);
    }
}
