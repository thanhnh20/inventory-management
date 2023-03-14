using Library.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DataAccess
{
    public class SuplierDAO
    {
        InventoryManagementContext db = new InventoryManagementContext();

        private static SuplierDAO instance = null;
        private static readonly object instancelock = new object();
        private SuplierDAO() { }
        public static SuplierDAO Instance
        {
            get
            {
                lock (instancelock)
                {
                    if (instance == null)
                    {
                        instance = new SuplierDAO();
                    }
                    return instance;
                }
            }
        }

        public DbSet<Suplier> GetListSuplier() => db.Supliers;
    }
}
