using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientManagement.Core.DTO;

public record PatientResponse(Guid PatientID, string? FullName, int Age,
    string? Gender, string? ContactInfo, bool? IsActive,string? Address, DateTime DateOfBirth)
{
    public PatientResponse():this(default,default,default,default,default,default,default,default)
    {
        
    }
}
