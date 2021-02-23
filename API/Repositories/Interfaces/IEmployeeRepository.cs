using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Repositories.Interfaces
{
    interface IEmployeeRepository
    {
        IEnumerable<Employee> Get();
        Task<Employee> Get(int AccountID);
        int Create(VM_RegistrationModel rm);
        int Update(int EmployeeID, string EmployeeName, string EmployeePhone, string EmployeeEmail);
        int Delete(int EmployeeID);
    }
}
