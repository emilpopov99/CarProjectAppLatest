using CarProjectApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarProjectApp.Repositories.Interfaces
{
   public interface IEmissionRepository
    {
        IEnumerable<Emission> GetAllEmissions { get; }
        Emission GetEmissionById(int id);
        void UpdateEmission(Emission emission);
    }
}
