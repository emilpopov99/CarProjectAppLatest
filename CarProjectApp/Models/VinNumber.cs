using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CarProjectApp.Models
{
    public class VinNumber
    {   [Key]
        public int Id { get; set; }
        public string Vin { get; set; }
    }
}
