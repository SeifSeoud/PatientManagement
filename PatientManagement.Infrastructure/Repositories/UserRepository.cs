using Dapper;
using Microsoft.EntityFrameworkCore;
using PatientManagement.Core.DTO;
using PatientManagement.Core.Entities;
using PatientManagement.Core.RepositoryContracts;
using PatientManagement.Infrastructure.DapperDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace PatientManagement.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly DapperDbContext _dbContext;
    public UserRepository(DapperDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<ApplicationUser?> AddUser(ApplicationUser user)
    {
        try
        {
            // SQL Server syntax - using square brackets and no 'public' schema
            string query = @"INSERT INTO [Users] (
                            [UserID],
                            [Email],
                            [PersonName],
                            [Gender],
                            [Password]
                        ) VALUES (
                            @UserID,
                            @Email,
                            @PersonName,
                            @Gender,
                            @Password
                        )";

            int rowCountAffected = await _dbContext.DbConnection.ExecuteAsync(query, user);
            return rowCountAffected > 0 ? user : null;
        }
        catch (Exception ex)
        {
            // Consider logging the error here
            throw;
        }
    }
    public async Task<ApplicationUser?> GetUserByEmailAndPassword(string? email, string? password)
    {
        try
        {
            string query = @"SELECT [UserID], [Email], [PersonName], [Gender], [Password] 
                FROM [Users] 
                WHERE [Email] = @Email AND [Password] = @Password";
            ApplicationUser? user = await _dbContext.DbConnection.QueryFirstOrDefaultAsync<ApplicationUser>(
                query,
                new
                {
                    Email = email?.Trim(),
                    Password = password
                });

            return user;
        }
        catch (Exception ex)
        {
            throw;
        }
    }
}
