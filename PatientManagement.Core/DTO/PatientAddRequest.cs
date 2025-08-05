using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientManagement.Core.DTO;

public record PatientAddRequest(string? FirstName,string? LastName, DateTime DateOfBirth,
    string? Gender,string? Address, string? PhoneNumber, string? Email)
{
    public PatientAddRequest() :this (default, default, default, default, default, default, default)
    {
        
    }
}
