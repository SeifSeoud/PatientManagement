using PatientManagement.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientManagement.Core.ServiceContracts;

public interface IPatientService
{
    Task<PatientResponse> AddPatient(PatientAddRequest patientAddRequest);
    Task<List<PatientResponse>> GetAllPatients();
    Task<PatientResponse?> GetPatientById(Guid patientID);
    Task<PatientResponse?> UpdatePatient(Guid patientID, PatientUpdateRequest patientUpdateRequest);
    Task<bool> DeletePatient(Guid patientID);
}
