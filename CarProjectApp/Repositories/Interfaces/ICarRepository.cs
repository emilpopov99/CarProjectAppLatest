using CarProjectApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarProjectApp.Repositories.Interfaces
{
    public interface ICarRepository
    {
        IEnumerable<Car> GetAllCars { get; }
        Car GetCarById(int id);
        void AddCar(Car car);
        void UpdateCar(Car car);
        void DeleteCar(Car car);
    }
}
