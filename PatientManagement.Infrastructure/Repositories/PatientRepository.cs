using Dapper;
using PatientManagement.Core.Entities;
using PatientManagement.Core.RepositoryContracts;
using PatientManagement.Infrastructure.DapperDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientManagement.Infrastructure.Repositories;

public class PatientRepository:IPatientRepository
{
    private readonly DapperDbContext _dbContext;
    public PatientRepository(DapperDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Patient> AddPatient(Patient patient)
    {
        try
        {
            string query = @"INSERT INTO Patients 
                            (PatientID, FirstName, LastName, DateOfBirth, Gender, 
                             Address, PhoneNumber, Email, CreatedAt, IsActive)
                            VALUES 
                            (@PatientID, @FirstName, @LastName, @DateOfBirth, @Gender, 
                             @Address, @PhoneNumber, @Email, @CreatedAt, @IsActive);
                            SELECT * FROM Patients WHERE PatientID = @PatientID;";

            // Use _dbContext.DbConnection directly
            var result = await _dbContext.DbConnection.QuerySingleAsync<Patient>(query, patient);
            return result;
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    public async Task<bool> DeletePatient(Guid patientID)
    {
        try
        {
            string query = "UPDATE Patients SET IsActive = 0 WHERE PatientID = @PatientID";
            var affectedRows = await _dbContext.DbConnection.ExecuteAsync(query, new { PatientID = patientID });
            return affectedRows > 0;
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public async Task<List<Patient>> GetAllPatients()
    {
        try
        {
            string query = "SELECT * FROM Patients WHERE IsActive = 1";
            var patients = await _dbContext.DbConnection.QueryAsync<Patient>(query);
            return patients.ToList();
        }
        catch(Exception ex)
        {
            throw;
        }
    }

    public async Task<Patient?> GetPatientById(Guid patientID)
    {
        try
        {
            string query = "SELECT * FROM Patients WHERE PatientID = @PatientID AND IsActive = 1";
            var patient = await _dbContext.DbConnection.QueryFirstOrDefaultAsync<Patient>(query, new { PatientID = patientID });
            return patient;
        }
        catch (Exception)
        {
            throw;
        }
    }
    public async Task<Patient?> UpdatePatient(Patient patient)
    {
        try
        {
            string query = @"UPDATE Patients SET 
                            FirstName = @FirstName,
                            LastName = @LastName,
                            DateOfBirth = @DateOfBirth,
                            Gender = @Gender,
                            Address = @Address,
                            PhoneNumber = @PhoneNumber,
                            Email = @Email,
                            UpdatedAt = @UpdatedAt
                            WHERE PatientID = @PatientID;
                            SELECT * FROM Patients WHERE PatientID = @PatientID;";

            var result = await _dbContext.DbConnection.QuerySingleOrDefaultAsync<Patient>(query, patient);
            return result;
        }
        catch(Exception ex)
        {
            throw;
        }
    }
}
