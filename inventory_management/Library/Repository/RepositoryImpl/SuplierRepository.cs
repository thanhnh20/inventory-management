﻿using Library.DataAccess;
using Library.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Repository.RepositoryImpl
{
    public class SuplierRepository : GenericRepository<Suplier>, ISuplierRepository
    {
        public SuplierRepository(InventoryManagementContext context) : base(context)
        {
        }

        public DbSet<Suplier> GetListSuplier() => SuplierDAO.Instance.GetListSuplier();
    }
}
