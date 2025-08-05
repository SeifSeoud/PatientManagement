using PatientManagement.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientManagement.Core.RepositoryContracts;

public interface IPatientRepository
{
    Task<Patient> AddPatient(Patient patient);
    Task<List<Patient>> GetAllPatients();
    Task<Patient?> GetPatientById(Guid patientID);
    Task<Patient?> UpdatePatient(Patient patient);
    Task<bool> DeletePatient(Guid patientID);
}
