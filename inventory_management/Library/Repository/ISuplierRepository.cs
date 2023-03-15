using Library.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Repository
{
    public interface ISuplierRepository : IGenericRepository<Suplier>
    {
        public DbSet<Suplier> GetListSuplier();
    }
}
