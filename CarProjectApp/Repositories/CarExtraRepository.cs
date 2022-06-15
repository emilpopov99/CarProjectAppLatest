using CarProjectApp.Models;
using CarProjectApp.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CarProjectApp.Repositories
{
    public class CarExtraRepository : ICarExtraRepository
    {
        private readonly ApplicationDbContext _appDbContext;
        public CarExtraRepository(ApplicationDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public IEnumerable<CarExtra> GetAllCarExtras => _appDbContext.CarExtras.AsNoTracking().ToList();
        public void AddCarExtra(CarExtra carextra)
        {
            _appDbContext.CarExtras.Add(carextra);
            _appDbContext.SaveChanges();
        }
        public void UpdateCarExtra(CarExtra carextra)
        {
            _appDbContext.Entry(carextra).State = EntityState.Modified;
            _appDbContext.CarExtras.Update(carextra);
            _appDbContext.SaveChanges();
        }
        public void DeleteCarExtra(CarExtra carextra)
        {
            _appDbContext.CarExtras.Remove(carextra);
            _appDbContext.SaveChanges();
        }

    }
}
