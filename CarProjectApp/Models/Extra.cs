using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CarProjectApp.Models
{
    public class Extra
    {   [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public IList<CarExtra> CarExtras { get; set; }
    }
}
