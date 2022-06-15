using CarProjectApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarProjectApp.Repositories.Interfaces
{
     public interface ICarExtraRepository
    {
        IEnumerable<CarExtra> GetAllCarExtras { get; }
        void AddCarExtra(CarExtra carextra);
        void UpdateCarExtra(CarExtra carextra);
        void DeleteCarExtra(CarExtra carextra);
    }
}
