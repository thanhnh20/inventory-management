﻿using Library.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Repository.RepositoryImpl
{
    public class ConsignmentDetailRepository : GenericRepository<ConsignmentDetail>, IConsignmentDetailRepository
    {
        public ConsignmentDetailRepository(InventoryManagementContext context) : base(context)
        {
        }
    }
}