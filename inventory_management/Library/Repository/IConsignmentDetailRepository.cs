﻿using Library.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Repository
{
    public interface IConsignmentDetailRepository : IGenericRepository<ConsignmentDetail>
    {

        public List<ConsignmentDetail> getConsignmentIDByProductID(int productID);

        public ConsignmentDetail getConsignmentByID(int consID);

        public IEnumerable<IGrouping<int, ConsignmentDetail>> GetConsignmentDetails();
    }
}
