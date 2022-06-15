using CarProjectApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarProjectApp.Repositories.Interfaces
{
    public interface IExtraRepository
    {
        IEnumerable<Extra> GetAllExtras { get; }
        Extra GetExtraById(int id);
        void AddExtra(Extra extra);
        void UpdateExtra(Extra extra);
        void DeleteExtra(Extra extra);
    }
}
