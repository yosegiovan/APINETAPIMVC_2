using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Repositories.Interfaces
{
    interface IAccountRepository
    {
        IEnumerable<Account> Get();
        Task<IEnumerable<Account>> Get(int AccountID);
        int Create(VM_RegistrationModel rm);
        int Update(int AccountID, string AccountPassword);
        int Delete(int AccountID);
    }
}
