using AutoMapper;
using PatientManagement.Core.DTO;
using PatientManagement.Core.Entities;
using PatientManagement.Core.RepositoryContracts;
using PatientManagement.Core.ServiceContracts;
using System.Security.Authentication;

namespace PatientManagement.Core.Services;

public class UsersService : IUsersService
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly IJwtService _jwtService;

    public UsersService(
        IUserRepository userRepository,
        IMapper mapper,
        IJwtService jwtService)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _jwtService = jwtService;
    }

    public async Task<AuthenticationResponse> Login(LoginRequest loginRequest)
    {
        if (loginRequest == null)
            throw new ArgumentNullException(nameof(loginRequest));

        if (string.IsNullOrWhiteSpace(loginRequest.Email))
            throw new ArgumentException("Email is required", nameof(loginRequest.Email));

        if (string.IsNullOrWhiteSpace(loginRequest.Password))
            throw new ArgumentException("Password is required", nameof(loginRequest.Password));

        ApplicationUser? user = await _userRepository.GetUserByEmailAndPassword(
            loginRequest.Email,
            loginRequest.Password);

        if (user == null)
            throw new InvalidCredentialException("Invalid email or password");

        string token = _jwtService.GenerateToken(user);

        return _mapper.Map<AuthenticationResponse>(user) with
        {
            Success = true,
            Token = token
        };
    }

    public async Task<AuthenticationResponse?> Register(RegisterRequest registerRequest)
    {
        if (registerRequest == null)
            throw new ArgumentNullException(nameof(registerRequest));

        // Input validation
        if (string.IsNullOrWhiteSpace(registerRequest.PersonName))
            throw new ArgumentException("Person name is required", nameof(registerRequest.PersonName));

        if (string.IsNullOrWhiteSpace(registerRequest.Email))
            throw new ArgumentException("Email is required", nameof(registerRequest.Email));

        if (string.IsNullOrWhiteSpace(registerRequest.Password))
            throw new ArgumentException("Password is required", nameof(registerRequest.Password));

        var existingUser = await _userRepository.GetUserByEmailAndPassword(
            registerRequest.Email,
            registerRequest.Password);

        if (existingUser != null)
            throw new ArgumentException("User with this email already exists");

        // Map request to entity
        ApplicationUser user = new ApplicationUser()
        {
            UserID = Guid.NewGuid(),
            PersonName = registerRequest.PersonName,
            Email = registerRequest.Email,
            Password = registerRequest.Password, 
            Gender = registerRequest.Gender.ToString(),
        };

        ApplicationUser? registeredUser = await _userRepository.AddUser(user);

        if (registeredUser == null)
            throw new Exception("Failed to register user");

        string token = _jwtService.GenerateToken(registeredUser);

        return _mapper.Map<AuthenticationResponse>(registeredUser) with
        {
            Success = true,
            Token = token
        };
    }
}