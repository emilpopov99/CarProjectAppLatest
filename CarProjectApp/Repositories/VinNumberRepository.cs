using CarProjectApp.Models;
using CarProjectApp.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CarProjectApp.Repositories
{
   public class VinNumberRepository : IVinNumberRepository
    {
        private readonly ApplicationDbContext _appDbContext;
        public VinNumberRepository(ApplicationDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public IEnumerable<VinNumber> GetAllVinNumbers => _appDbContext.VinNumbers.AsNoTracking().ToList();
        public VinNumber GetVinNumberById(int id) => _appDbContext.VinNumbers.AsNoTracking().SingleOrDefault(x => x.Id == id);
        public void AddVinNumber(VinNumber vinNumber)
        {
            _appDbContext.VinNumbers.Add(vinNumber);
            _appDbContext.SaveChanges();
        }
        public void UpdateVinNumber(VinNumber vinNumber)
        {
            _appDbContext.Entry(vinNumber).State = EntityState.Modified;
            _appDbContext.VinNumbers.Update(vinNumber);
            _appDbContext.SaveChanges();
            _appDbContext.Entry(vinNumber).State = EntityState.Detached;
        }
        public void DeleteVinNumber(VinNumber vinNumber)
        {
            _appDbContext.VinNumbers.Remove(vinNumber);
            _appDbContext.SaveChanges();
        }
    }
}
