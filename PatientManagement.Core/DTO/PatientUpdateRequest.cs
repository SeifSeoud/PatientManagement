using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientManagement.Core.DTO;

public record PatientUpdateRequest(string? FirstName, string? LastName, DateTime DateOfBirth, string?Gender
    ,string? Address, string? PhoneNumber, string? Email)
{
    public PatientUpdateRequest():this(default, default, default, default, default, default, default)
    {
        
    }
}

