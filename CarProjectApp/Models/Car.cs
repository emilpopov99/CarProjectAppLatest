using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CarProjectApp.Models
{
    
    public class Car
    {
        [Key]
        public int Id { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public DateTime ManifactoryDate { get; set; }
        public int CubicCapacity { get; set; }
        public int Horsepower { get; set; }
        public string RegNumber { get; set; }
        public string FuelType { get; set; }
        public string BodyType { get; set; }
        public int EmissionId { get; set; }
        public Emission Emission { get; set; }
        public int OwnerId { get; set; }
        public Owner Owner { get; set; }
        public IList<CarExtra> CarExtras { get; set; }
        public int VinNumberId { get; set; }
        public VinNumber VinNumber { get; set; }
    }
}
