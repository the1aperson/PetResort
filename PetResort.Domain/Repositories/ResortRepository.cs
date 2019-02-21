using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using PetResort.DATA;

namespace PetResort.Domain.Repositories
{
   public class ResortRepository : GenericRepository<ResortLocation>
    {
        public ResortRepository(DbContext db) : base(db) { }

    }
}
