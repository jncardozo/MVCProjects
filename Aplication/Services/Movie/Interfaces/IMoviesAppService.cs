using DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Services.Movie.Interfaces
{
    public interface IMoviesAppService : IGenericRepository<Domain.Models.Movies>
    {
    }
}
