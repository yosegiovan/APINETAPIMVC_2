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
    public class AccountsController : Controller
    {
        readonly HttpClient client = new HttpClient
        {
            BaseAddress = new Uri("https://localhost:44373/API/")
        };
        // GET: Accounts

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(VM_RegistrationModel registrationModel)
        {
            var loginTask = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("https://localhost:44373/API/Accounts"),
                Content = new StringContent(JsonConvert.SerializeObject(registrationModel), Encoding.UTF8, "application/json")
            };

            var response = client.SendAsync(loginTask);
            var result = response.Result;
            if (result.IsSuccessStatusCode)
            {
                IEnumerable<Employee> employees = PopulateEmployees();
                var id = employees.Where(a => a.EmployeeEmail == registrationModel.EmployeeEmail).FirstOrDefault().EmployeeID;

                return RedirectToAction("../Employees/Details/" + id);
            }
            else
            {
                RedirectToAction("../Accounts/Failed", new { sc = (int)result.StatusCode });
            }

            return View();
        }

        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ForgotPassword(Employee employee)
        {
            var Task = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("https://localhost:44373/API/Passwords"),
                Content = new StringContent(JsonConvert.SerializeObject(employee), Encoding.UTF8, "application/json")
            };

            var response = client.SendAsync(Task);
            var result = response.Result;
            if (result.IsSuccessStatusCode)
            {
                ViewBag.Message = "Password has been reset! Please check your email!";
                RedirectToAction("../Accounts/ForgotPassword");
            }
            else
            {
                //RedirectToAction("../Accounts/Failed", new { sc = (int)result.StatusCode });
                ViewBag.error = "Account doesn't Exist! Register maybe?";
                RedirectToAction("../Accounts/ForgotPassword");
            }

            return View();
        }

        public ActionResult UpdatePassword(int id)
        {
            VM_RegistrationModel registrationModel = new VM_RegistrationModel
            {
                EmployeeID = id
            };

            return View(registrationModel);
        }

        [HttpPost]
        public ActionResult UpdatePassword(VM_RegistrationModel registrationModel)
        {
            IEnumerable<Account> acc = PopulateAccounts();
            var record = acc.FirstOrDefault(a => a.AccountID == registrationModel.EmployeeID).AccountPassword.ToString();

            Account new_rm = new Account
            {
                AccountID = registrationModel.EmployeeID,
                AccountPassword = registrationModel.AccountNewPassword
            };

            if (record == registrationModel.AccountPassword)
            {
                var Task = new HttpRequestMessage
                {
                    Method = HttpMethod.Put,
                    RequestUri = new Uri("https://localhost:44373/API/Accounts"),
                    Content = new StringContent(JsonConvert.SerializeObject(new_rm), Encoding.UTF8, "application/json")
                };

                var response = client.SendAsync(Task);
                var result = response.Result;
                if (result.IsSuccessStatusCode)
                {
                    ViewBag.Message = "Password had been changed!";
                }
                else
                {
                    ViewBag.error = "Failed to changed password!";
                }
                return RedirectToAction("../Employees/Details/" + registrationModel.EmployeeID);
            }
            else
            {
                ViewBag.error = "Your current password is incorrect!";
            }

            return View();
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

        private IEnumerable<Account> PopulateAccounts()
        {
            IEnumerable<Account> accounts = null;

            var respondTask = client.GetAsync("Accounts");
            respondTask.Wait();
            var result = respondTask.Result;

            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<IList<Account>>();
                readTask.Wait();
                accounts = readTask.Result;
            }

            return accounts;
        }
    }
}