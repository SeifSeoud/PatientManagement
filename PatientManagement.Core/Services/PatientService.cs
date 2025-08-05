using AutoMapper;
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

public class PatientService:IPatientService
{
    private readonly IPatientRepository _patientRepository;
    private readonly IMapper _mapper;

    public PatientService(IPatientRepository patientRepository, IMapper mapper)
    {
        _patientRepository = patientRepository;
        _mapper = mapper;
    }

    public async Task<PatientResponse> AddPatient(PatientAddRequest patientAddRequest)
    {
        var patient = _mapper.Map<Patient>(patientAddRequest);
        var addedPatient = await _patientRepository.AddPatient(patient);
        return _mapper.Map<PatientResponse>(addedPatient);
    }


    public async Task<bool> DeletePatient(Guid patientID)
    {
        return await _patientRepository.DeletePatient(patientID);
    }

    public async Task<List<PatientResponse>> GetAllPatients()
    {
        var patients = await _patientRepository.GetAllPatients();
        return _mapper.Map<List<PatientResponse>>(patients);
    }
    public async Task<PatientResponse?> GetPatientById(Guid patientID)
    {
        var patient = await _patientRepository.GetPatientById(patientID);
        return patient != null ? _mapper.Map<PatientResponse>(patient) : null;
    }
    public async Task<PatientResponse?> UpdatePatient(Guid patientID, PatientUpdateRequest patientUpdateRequest)
    {
        var existingPatient = await _patientRepository.GetPatientById(patientID);
        if (existingPatient == null) return null;

        _mapper.Map(patientUpdateRequest, existingPatient);
        var updatedPatient = await _patientRepository.UpdatePatient(existingPatient);
        return _mapper.Map<PatientResponse>(updatedPatient);
    }
}
