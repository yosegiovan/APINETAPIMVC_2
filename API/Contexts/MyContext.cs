using API.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace API.Contexts
{
    public class MyContext : DbContext
    {
        public MyContext() : base("ASPNETAPIMVC_2")
        {

        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Account> Accounts { get; set; }
    }
}