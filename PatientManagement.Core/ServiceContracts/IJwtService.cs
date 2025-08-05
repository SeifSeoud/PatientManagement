using PatientManagement.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientManagement.Core.ServiceContracts;

public interface IJwtService
{
    string GenerateToken(ApplicationUser user);

}
