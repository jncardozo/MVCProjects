using Aplication.Services.Movie.Interfaces;
using DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Services.Movie.Classes
{
    public class MoviesAppService : GeneralReposity<Domain.Models.Movies>, IMoviesAppService
    {
    }
}
