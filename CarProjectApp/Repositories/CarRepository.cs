using CarProjectApp.Models;
using CarProjectApp.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CarProjectApp.Repositories
{
    public class CarRepository : ICarRepository
    {
        private readonly ApplicationDbContext _appDbContext;
        public CarRepository(ApplicationDbContext appDbContext)
        {
            _appDbContext = appDbContext;
            
        }
        public IEnumerable<Car> GetAllCars => _appDbContext.Cars.AsNoTracking().ToList();
        public Car GetCarById(int id) => _appDbContext.Cars.AsNoTracking().SingleOrDefault(x => x.Id == id);
        public void AddCar(Car car)
        {
            _appDbContext.Cars.Add(car);
            _appDbContext.SaveChanges();
            _appDbContext.Entry(car).State = EntityState.Detached;
        }
        public void UpdateCar(Car car)
        {
            _appDbContext.Cars.Update(car);
            _appDbContext.SaveChanges();
            _appDbContext.Entry(car).State = EntityState.Detached;
            
        }
        public void DeleteCar(Car car)
        {
            _appDbContext.Cars.Remove(car);
            _appDbContext.SaveChanges();
            _appDbContext.Entry(car).State = EntityState.Detached;
        }
    }
}
