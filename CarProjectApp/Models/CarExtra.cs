using System;
using System.Collections.Generic;
using System.Text;

namespace CarProjectApp.Models
{
     public class CarExtra
    {
        public int CarId { get; set; }
        public Car Car { get; set; }
        public int ExtraId { get; set; }
        public Extra Extra { get; set; }
    }
}
