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
    public class PasswordsController : ApiController
    {
        readonly AccountRepository accountRepository = new AccountRepository();
        readonly EmployeeRepository employeeRepository = new EmployeeRepository();

        public IHttpActionResult ForgotPassword(Employee employee)
        {
            var user = employeeRepository.Get().Where(r => r.EmployeeEmail == employee.EmployeeEmail).FirstOrDefault();

            if (user != null)
            {
                string to = employee.EmployeeEmail.ToString(); //To address    
                string from = "xianlanzou@gmail.com"; //From address    
                string resetPassword = Guid.NewGuid().ToString();
                MailMessage message = new MailMessage(from, to);

                string mailbody = "<h1>Your Account's Password was reset!</h1> Please use this temporary password : " + resetPassword;
                message.Subject = "Reset Password Code";
                message.Body = mailbody;
                message.BodyEncoding = Encoding.UTF8;
                message.IsBodyHtml = true;

                SmtpClient client = new SmtpClient("smtp.gmail.com", 587); //Gmail smtp    
                System.Net.NetworkCredential basicCredential1 = new
                System.Net.NetworkCredential(from, "akiyam4mio");

                client.EnableSsl = true;
                client.UseDefaultCredentials = false;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.Credentials = basicCredential1;

                try
                {
                    client.Send(message);
                    accountRepository.Update(user.EmployeeID, resetPassword);
                    return Ok("Please check your email!");
                }
                catch
                {
                    return BadRequest("Failed to sent!");
                }
            }
            else
            {
                return BadRequest("Account not exist!");
            }

        }
    }
}
