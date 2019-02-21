using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PetResort.DATA;
using System.Data.Entity.Validation;

namespace PetResort.Domain.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        PetRepository PetRepository { get; }
        ReservationRepository ReservationRepository { get; }
        ResortRepository ResortRepository { get; }
        void Save();
    }
    public class UnitOfWork : IUnitOfWork
    {
        internal PetResortEntities context = new PetResortEntities();

        private PetRepository _petRepository;

        public PetRepository PetRepository
        {
            get
            {
                if(this._petRepository == null)
                {
                    this._petRepository = new PetRepository(context);
                }
                return _petRepository;
            }
        }

        private ReservationRepository _reservationRepository;

        public ReservationRepository ReservationRepository
        {
            get
            {
                if(this._reservationRepository == null)
                {
                    this._reservationRepository = new ReservationRepository(context);
                }
                return _reservationRepository;
            }
        }

        private ResortRepository _resortRepository;

        public ResortRepository ResortRepository
        {
            get
            {
                if(this._resortRepository == null)
                {
                    this._resortRepository = new ResortRepository(context);
                }
                return _resortRepository;
            }
        }
        public void Save()
        {
                context.SaveChanges();
                
            
        }
        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
