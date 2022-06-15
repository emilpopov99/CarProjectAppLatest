using CarProjectApp.Models;
using CarProjectApp.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CarProjectApp.Repositories
{
    public class OwnerRepository : IOwnerRepository
    {
        private readonly ApplicationDbContext _appDbContext;
        public OwnerRepository(ApplicationDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public IEnumerable<Owner> GetAllOwners => _appDbContext.Owners.AsNoTracking().ToList();
        public Owner GetOwnerById(int id) => _appDbContext.Owners.AsNoTracking().SingleOrDefault(x => x.Id == id);
        public void AddOwner(Owner owner)
        {
            _appDbContext.Owners.Add(owner);
            _appDbContext.SaveChanges();
        }
        public void UpdateOwner(Owner owner)
        {
            _appDbContext.Entry(owner).State = EntityState.Modified;
            _appDbContext.Owners.Update(owner);
            _appDbContext.SaveChanges();
            
        }
        public void DeleteOwner(Owner owner)
        {
            _appDbContext.Owners.Remove(owner);
            _appDbContext.SaveChanges();
        }

       
    }
}
