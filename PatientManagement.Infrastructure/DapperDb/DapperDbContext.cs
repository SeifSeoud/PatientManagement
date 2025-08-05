using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientManagement.Infrastructure.DapperDb;
public class DapperDbContext
{
    private readonly IConfiguration _configuration;
    private readonly IDbConnection _connection;
    public DapperDbContext(IConfiguration configuration)
    {
        _configuration = configuration;
        string ? _connectionString = _configuration.GetConnectionString("DefaultConnection");
        _connection=new SqlConnection(_connectionString);
    }

    public IDbConnection DbConnection => _connection;

}
