using API.Models;
using API.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Text;
using System.Web.Http;

namespace API.Controllers
{
    public class AccountsController : ApiController
    {
        readonly AccountRepository accountRepository = new AccountRepository();
        readonly EmployeeRepository employeeRepository = new EmployeeRepository();

        public IHttpActionResult Get()
        {
            IEnumerable<Account> accounts = accountRepository.Get();

            try
            {
                //return Ok();
                return Ok(accounts);
            }
            catch
            {
                //return Content(HttpStatusCode.BadRequest, "Failed to Get Supplier");
                //return BadRequest("Failed to Get Employee");
                return NotFound();
            }

        }

        public IHttpActionResult Put(Account account)
        {
            accountRepository.Update(account.AccountID, account.AccountPassword);
            try
            {
                return Ok();
            }
            catch
            {
                //return Content(HttpStatusCode.BadRequest, "Failed to Update Supplier");
                return BadRequest("Failed to Update Accounts");
            }
        }

        public IHttpActionResult Post(VM_RegistrationModel registrationModel)
        {
            var employees = employeeRepository.Get();
            var accounts = accountRepository.Get();

            var record = employees
                .Join(accounts, emp => emp.EmployeeID,
                acc => acc.AccountID,
                (emp, acc) => new
                {
                    emp.EmployeeID,
                    emp.EmployeeEmail,
                    acc.AccountPassword,
                }
                ).ToList();

            var user = record.FirstOrDefault(r => r.EmployeeEmail == registrationModel.EmployeeEmail
            && r.AccountPassword == registrationModel.AccountPassword);

            if (user != null)
            {
                return Ok("Berhasil!");
            }
            else
            {
                return BadRequest("Account not exist!");
            }
        }

    }
}