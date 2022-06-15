using CarProjectApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarProjectApp.Repositories.Interfaces
{
   public interface IOwnerRepository
    {
        IEnumerable<Owner> GetAllOwners { get; }
        Owner GetOwnerById(int id);
        void AddOwner(Owner owner);
        void UpdateOwner(Owner owner);
        void DeleteOwner(Owner owner);
    }
}
