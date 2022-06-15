using CarProjectApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarProjectApp.Repositories.Interfaces
{
     public interface IVinNumberRepository
    {
        IEnumerable<VinNumber> GetAllVinNumbers { get; }
        VinNumber GetVinNumberById(int id);
        void AddVinNumber(VinNumber vinNumber);
        void UpdateVinNumber(VinNumber vinNumber);
        void DeleteVinNumber(VinNumber vinNumber);
    }
}
