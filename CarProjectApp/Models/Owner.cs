using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CarProjectApp.Models
{
    public class Owner
    {   [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Egn { get; set; }
        public IList<Car> Cars { get; set; }
    }
}
