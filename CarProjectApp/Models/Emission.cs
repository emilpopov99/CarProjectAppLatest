using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CarProjectApp.Models
{
    public class Emission
    {
        [Key]
        public int Id { get; set;  }
        public string EuroStandart { get; set; }
        public IList<Car> Cars { get; set; }
    }
}
