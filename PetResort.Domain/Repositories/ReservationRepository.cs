using PetResort.DATA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace PetResort.Domain.Repositories
{
    public class ReservationRepository : GenericRepository<Reservation>
    {
        public ReservationRepository(DbContext db) : base(db) { }

        public List<Reservation> GetReservations(string UserId = "")
        {
            List<Reservation> reservations = new List<Reservation>();
            if (UserId == "")
            {
                reservations = Get().ToList();
            }
            else
            {
                reservations = Get().Where(x => x.Pet.OwnerID == UserId).ToList();
            }

            return reservations;
        }
    }
    //public class ReservationRepository
    //{
    //    private PetResortEntities db = new PetResortEntities();

    //    public List<Reservation> Get()
    //    {
    //        return db.Reservations.ToList();
    //    }

    //    public Reservation Find(int? id)
    //    {
    //        return db.Reservations.Find(id);
    //    }

    //    public void Add(Reservation reservation)
    //    {
    //        db.Reservations.Add(reservation);
    //        db.SaveChanges();
    //    }

    //    public void Remove(Reservation reservation)
    //    {
    //        db.Reservations.Remove(reservation);
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
    //        public void Dispose()
    //    {
    //        Dispose(true);
    //        GC.SuppressFinalize(this);
    //    }
    //    }
    //}
}
