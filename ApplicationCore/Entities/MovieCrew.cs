using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Entities
{
    [Table("MovieCrew")]
    public class MovieCrew
    {
        public int CrewId { get; set; }
        public string Department { get; set; }
        public string Job { get; set; }
        public int MovieId { get; set; }

        public Movie Movie { get; set; }
        public Crew Crew { get; set; }

    }


}