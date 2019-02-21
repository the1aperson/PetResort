using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PetResort.DATA;
using System.Data.Entity;

namespace PetResort.Domain.Repositories
{
    public class PetRepository : GenericRepository<Pet>
    {
        public PetRepository(DbContext db) : base(db) { }

        public List<Pet> GetPets(string UserId = "")
        {
            List<Pet> pets = new List<Pet>();
            if (UserId == "")
            {
                pets = Get().ToList();
            }
            else
            {
                pets = Get().Where(x => x.OwnerID == UserId).ToList();
            }

            return pets;
        }

    }
    //public class PetRepository
    //{
    //    private PetResortEntities db = new PetResortEntities();

    //    public List<Pet> Get()
    //    {
    //        return db.Pets.ToList();
    //    }

    //    public Pet Find(int? id)
    //    {
    //        return db.Pets.Find(id);
    //    }

    //    public void Add(Pet pet)
    //    {
    //        db.Pets.Add(pet);
    //        db.SaveChanges();
    //    }

    //    public void Remove(Pet pet)
    //    {
    //        db.Pets.Remove(pet);
    //        db.SaveChanges();
    //    }

    //    private bool disposed = false;

    //    protected virtual void Dispose(bool disposing)
    //    {
    //        if (!this.disposed)
    //        {
    //            if (disposing)
    //            {
    //                db.Dispose();
    //            }
    //        }
    //        this.disposed = true;
    //    }

    //    public void Dispose()
    //    {
    //        Dispose(true);
    //        GC.SuppressFinalize(this);
    //    }

    //}
}