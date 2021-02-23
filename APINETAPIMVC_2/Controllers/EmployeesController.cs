using API.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace APINETAPIMVC_2.Controllers
{
    public class EmployeesController : Controller
    {
        readonly HttpClient client = new HttpClient
        {
            BaseAddress = new Uri("https://localhost:44373/API/")
        };
        // GET: Employees
        public ActionResult Details(int id)
        {
            IEnumerable<Employee> employees = PopulateEmployees();
            var record = employees.FirstOrDefault(e => e.EmployeeID == id);

            return View(record);
        }

        public ActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Registration(VM_RegistrationModel registrationModel)
        {
            //tdk usah savechanges, nt d lempar ke API
            HttpResponseMessage response = client.PostAsJsonAsync("Employees", registrationModel).Result;

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("../Accounts/Login");
            }
            else
            {
                RedirectToAction("../Employees/Failed", new { sc = (int)response.StatusCode });
            }

            //return View();
            return RedirectToAction("Index");
        }

        private IEnumerable<Employee> PopulateEmployees()
        {
            IEnumerable<Employee> employees = null;

            var respondTask = client.GetAsync("Employees");
            respondTask.Wait();
            var result = respondTask.Result;

            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<IList<Employee>>();
                readTask.Wait();
                employees = readTask.Result;
            }

            return employees;
        }

        //private IEnumerable<Account> PopulateAccounts()
        //{
        //    IEnumerable<Account> employees = null;

        //    var respondTask = client.GetAsync("Accounts");
        //    respondTask.Wait();
        //    var result = respondTask.Result;

        //    if (result.IsSuccessStatusCode)
        //    {
        //        var readTask = result.Content.ReadAsAsync<IList<Account>>();
        //        readTask.Wait();
        //        employees = readTask.Result;
        //    }

        //    return employees;
        //}
    }
}