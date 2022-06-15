using CarProjectApp.Models;
using CarProjectApp.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CarProjectApp.Repositories
{
   public class ExtraRepository : IExtraRepository
    {
        private readonly ApplicationDbContext _appDbContext;
        public ExtraRepository(ApplicationDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public IEnumerable<Extra> GetAllExtras => _appDbContext.Extras.AsNoTracking().ToList();
        public Extra GetExtraById(int id) => _appDbContext.Extras.AsNoTracking().SingleOrDefault(x => x.Id == id);
        public void AddExtra(Extra extra)
        {
            _appDbContext.Extras.Add(extra);
            _appDbContext.SaveChanges();
        }
        public void UpdateExtra(Extra extra)
        {
            _appDbContext.Entry(extra).State = EntityState.Modified;
            _appDbContext.Extras.Update(extra);
            _appDbContext.SaveChanges();
        }
        public void DeleteExtra(Extra extra)
        {
            _appDbContext.Extras.Remove(extra);
            _appDbContext.SaveChanges();
        }
    }
}
