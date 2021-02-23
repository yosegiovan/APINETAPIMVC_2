using API.Models;
using API.Repositories.Interfaces;
using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace API.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        readonly DynamicParameters parameters = new DynamicParameters();
        readonly SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["myConnection"].ConnectionString);

        public int Create(VM_RegistrationModel registrationModel)
        {
            //throw new NotImplementedException();
            var spName = "SP_InsertUser";
            parameters.Add("@EmployeeName", registrationModel.EmployeeName);
            parameters.Add("@EmployeePhone", registrationModel.EmployeePhone);
            parameters.Add("@EmployeeEmail", registrationModel.EmployeeEmail);
            parameters.Add("@AccountPassword", registrationModel.AccountPassword);

            var result = connection.Execute(spName, parameters, commandType: CommandType.StoredProcedure);

            return result;
        }

        public int Delete(int EmployeeID)
        {
            //throw new NotImplementedException();
            var spName = "SP_DeleteUser";
            parameters.Add("@EmployeeID", EmployeeID);

            var result = connection.Execute(spName, parameters, commandType: CommandType.StoredProcedure);

            return result;
        }

        public IEnumerable<Employee> Get()
        {
            //throw new NotImplementedException();
            var spName = "SP_GetEmployee";
            var result = connection.Query<Employee>(spName, commandType: CommandType.StoredProcedure);
            return result;
        }

        public async Task<Employee> Get(int AccountID)
        {
            //throw new NotImplementedException();
            var spName = "SP_GetEmployeeByID";
            parameters.Add("@AccountID", AccountID);
            var result = await connection.QueryAsync<Employee>(spName, parameters, commandType: CommandType.StoredProcedure);
            return result.FirstOrDefault();
        }

        public int Update(int EmployeeID, string EmployeeName, string EmployeePhone, string EmployeeEmail)
        {
            //throw new NotImplementedException();
            var spName = "SP_UpdateEmployeeByID";
            parameters.Add("@EmployeeID", EmployeeID);
            parameters.Add("@EmployeeName", EmployeeName);
            parameters.Add("@EmployeePhone", EmployeePhone);
            parameters.Add("@EmployeeEmail", EmployeeEmail);

            var result = connection.Execute(spName, parameters, commandType: CommandType.StoredProcedure);

            return result;
        }
    }
}