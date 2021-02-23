using API.Models;
using API.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace API.Controllers
{
    public class EmployeesController : ApiController
    {
        readonly EmployeeRepository employeeRepository = new EmployeeRepository();

        public IHttpActionResult Post(VM_RegistrationModel registrationModel)
        {
            employeeRepository.Create(registrationModel);
            try
            {
                return Ok();
            }
            catch
            {
                //return Content(HttpStatusCode.BadRequest, "Failed to Create Supplier");
                return BadRequest("Failed to Add User");
            }
        }

        public IHttpActionResult Get()
        {
            IEnumerable<Employee> employees = employeeRepository.Get();

            try
            {
                //return Ok();
                return Ok(employees);
            }
            catch
            {
                //return Content(HttpStatusCode.BadRequest, "Failed to Get Supplier");
                //return BadRequest("Failed to Get Employee");
                return NotFound();
            }

        }

        public async Task<IHttpActionResult> Get(int EmployeeID)
        {
            var record = await employeeRepository.Get(EmployeeID);

            try
            {
                //return Ok();
                return Ok(record);
            }
            catch
            {
                //return Content(HttpStatusCode.BadRequest, "Failed to Get Supplier By ID");
                //return BadRequest("Failed to Get Employee By ID");
                return NotFound();
            }
        }

        public IHttpActionResult Put(Employee employee)
        {
            employeeRepository.Update(employee.EmployeeID, employee.EmployeeName, employee.EmployeePhone, employee.EmployeeEmail);
            try
            {
                return Ok();
            }
            catch
            {
                //return Content(HttpStatusCode.BadRequest, "Failed to Update Supplier");
                return BadRequest("Failed to Update Employee");
            }
        }

        public IHttpActionResult Delete(Employee employee)
        {
            employeeRepository.Delete(employee.EmployeeID);
            try
            {
                return Ok();
            }
            catch
            {
                //return Content(HttpStatusCode.BadRequest, "Failed to Delete Supplier");
                return BadRequest("Failed to Delete Employee");
            }
        }
    }
}
