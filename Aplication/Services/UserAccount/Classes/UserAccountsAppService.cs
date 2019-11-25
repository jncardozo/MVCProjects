using Aplication.Services.UserAccount.Interfaces;
using DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Aplication.Services.UserAccount.Classes
{
    public class UserAccountsAppService : GeneralReposity<Domain.Models.UserAccount>, IUserAccountAppService
    {
    }
}
