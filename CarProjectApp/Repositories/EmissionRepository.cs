using CarProjectApp.Models;
using CarProjectApp.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CarProjectApp.Repositories
{
   public class EmissionRepository : IEmissionRepository
    {
        private readonly ApplicationDbContext _appDbContext;
        public EmissionRepository(ApplicationDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public IEnumerable<Emission> GetAllEmissions => _appDbContext.Emissions.AsNoTracking().ToList();
        public Emission GetEmissionById(int id) => _appDbContext.Emissions.AsNoTracking().SingleOrDefault(x => x.Id == id);

        public void UpdateEmission(Emission emission)
        {
            _appDbContext.Entry(emission).State = EntityState.Modified;
            _appDbContext.Emissions.Update(emission);
            _appDbContext.SaveChanges();
        }
    }
}
