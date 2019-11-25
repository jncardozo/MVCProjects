using DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Aplication.Services.UserAccount.Interfaces
{
    public interface IUserAccountAppService : IGenericRepository<Domain.Models.UserAccount>
    {

    }    
}
