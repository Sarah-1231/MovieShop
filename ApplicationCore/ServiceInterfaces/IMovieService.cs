using ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.ServiceInterfaces
{
    public interface IMovieService
    {
        //models services return models
        IEnumerable<MovieCardResponseModel> Get30HighestGrossingMovies();
    }
}
