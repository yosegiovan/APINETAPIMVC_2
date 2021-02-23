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
    public class AccountRepository : IAccountRepository
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

        public int Delete(int AccountID)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Account> Get()
        {
            //throw new NotImplementedException();
            var spName = "SP_GetAccount";
            var result = connection.Query<Account>(spName, commandType: CommandType.StoredProcedure);
            return result;
        }

        public async Task<IEnumerable<Account>> Get(int AccountID)
        {
            //throw new NotImplementedException();
            var spName = "SP_GetAccountsByID";
            parameters.Add("@AccountID", AccountID);
            var result = await connection.QueryAsync<Account>(spName, parameters, commandType: CommandType.StoredProcedure);
            return result;
        }

        public int Update(int AccountID, string AccountPassword)
        {
            //throw new NotImplementedException();
            var spName = "SP_UpdateAccountByID";
            parameters.Add("@AccountID", AccountID);
            parameters.Add("@AccountPassword", AccountPassword);

            var result = connection.Execute(spName, parameters, commandType: CommandType.StoredProcedure);

            return result;
        }
    }
}