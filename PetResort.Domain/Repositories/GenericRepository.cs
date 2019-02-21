using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using PetResort.DATA;
using System.Data;

namespace PetResort.Domain.Repositories
{
    public class GenericRepository<TEntity>: IGenericRepositable<TEntity> where TEntity: class 
    {
        internal DbContext db;
        public GenericRepository(DbContext context)
        {
            this.db = context;
        }

        public virtual List<TEntity> Get(string includeProperties = "")
        {
            IQueryable<TEntity> query = db.Set<TEntity>();

            foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }
            return query.ToList();
        }
        public TEntity Find(object id)
        {
            return db.Set<TEntity>().Find(id);
        }

        //-------------WRITE METHODS
        public void Add(TEntity entity)
        {
            db.Set<TEntity>().Add(entity);
            //db.SaveChanges();//removed for the unit of work
        }

        public void Update(TEntity entity)
        {
            db.Entry(entity).State = System.Data.Entity.EntityState.Modified;
            //db.SaveChanges();//removed for unit of work
        }

        public void Remove(TEntity entity)
        {
            db.Set<TEntity>().Remove(entity);
            //db.SaveChanges();//removed for the unit of work
        }
        //this overload lets you pass the id of the object you want to delete aka remove
        public void Remove(object id)
        {
            var entity = db.Set<TEntity>().Find(id);
            Remove(entity);//notice method overloads are often chained to build off each other
        }

        public int CountRecords()
        {
            return Get().Count;
        }

        //Dispose of the db object when the GenreRepository is disposed of
        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
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
