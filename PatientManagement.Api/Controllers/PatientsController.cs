using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PatientManagement.Core.DTO;
using PatientManagement.Core.ServiceContracts;

namespace PatientManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        private readonly IPatientService _patientService;

        public PatientsController(IPatientService patientService)
        {
            _patientService = patientService;
        }

        [HttpGet]
        public async Task<ActionResult<List<PatientResponse>>> GetAllPatients()
        {
            var patients = await _patientService.GetAllPatients();
            return Ok(patients);
        }
        [HttpGet("{patientID}")]
        public async Task<ActionResult<PatientResponse>> GetPatientById(Guid patientID)
        {
            var patient = await _patientService.GetPatientById(patientID);
            if (patient == null)
                return NotFound();

            return Ok(patient);
        }

        [HttpPost]
        public async Task<ActionResult<PatientResponse>> AddPatient(PatientAddRequest patientAddRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var addedPatient = await _patientService.AddPatient(patientAddRequest);
            return CreatedAtAction(nameof(GetPatientById), new { patientID = addedPatient.PatientID }, addedPatient);
        }

        [HttpPut("{patientID}")]
        public async Task<ActionResult<PatientResponse>> UpdatePatient(Guid patientID, PatientUpdateRequest patientUpdateRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updatedPatient = await _patientService.UpdatePatient(patientID, patientUpdateRequest);
            if (updatedPatient == null)
                return NotFound();

            return Ok(updatedPatient);
        }

        [HttpDelete("{patientID}")]
        public async Task<IActionResult> DeletePatient(Guid patientID)
        {
            var result = await _patientService.DeletePatient(patientID);
            if (!result)
                return NotFound();

            return NoContent();
        }
}
}
