﻿using Library.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DataAccess
{
    public class ConsignmentDetailDAO
    {
        private static ConsignmentDetailDAO instance = null;
        private static readonly object instancelock = new object();
        private ConsignmentDetailDAO() { }
        public static ConsignmentDetailDAO Instance
        {
            get
            {
                lock (instancelock)
                {
                    if (instance == null)
                    {
                        instance = new ConsignmentDetailDAO();
                    }
                    return instance;
                }
            }
        }

        public List<ConsignmentDetail> getConsignmentIDByProductID(int productID)
        {
            using (var db = new InventoryManagementContext())
            {
                return db.ConsignmentDetails.Where(c => c.ProductId == productID && c.Quantity > 0).ToList();
            }
        }

        public ConsignmentDetail getConsignmentByID(int consID)
        {
            using (var db = new InventoryManagementContext())
            {
                return db.ConsignmentDetails.Where(c => c.ConsignmentDetailId == consID).FirstOrDefault();
            }
        }

        public IEnumerable<IGrouping<int, ConsignmentDetail>> GetConsignmentDetails(int ConsignmentID)
        {
            using(var db = new InventoryManagementContext())
            {
                var listConsignmentDetails = db.ConsignmentDetails.Where(c => c.ConsignmentId == ConsignmentID)
                    .Include(i => i.InvoiceInputDetails)
                    .ToList();
                foreach (var cons in listConsignmentDetails)                   
                {
                    cons.Product = db.Products.Where(p => p.ProductId == cons.ProductId).Include(i => i.Category).FirstOrDefault();
                }
                var listCustom = listConsignmentDetails.GroupBy(l => l.ConsignmentId);
                return listCustom;
            }       
        }


        public int GetConsignmentIDDetailsByID(int id)
        {
            using (var db = new InventoryManagementContext())
            {
                return db.ConsignmentDetails.Where(c => c.ConsignmentDetailId == id).FirstOrDefault().ConsignmentId;
            }
        }

        public List<ConsignmentDetail> GetConsignmentDetails()
        {
            using (var db = new InventoryManagementContext())
            {
                return db.ConsignmentDetails
                    .Include(i => i.Consignment)
                    .Include(i => i.Product).ThenInclude(i => i.Category)
                    .Include(i => i.InvoiceInputDetails).ThenInclude(i => i.InputBill).ThenInclude(i => i.Suplier)                                                                
                    .ToList();
            }
        }

        public List<ConsignmentDetail> GetConsignmentDetailsOutput()
        {
            using (var db = new InventoryManagementContext())
            {
                return db.ConsignmentDetails
                    .Include(i => i.Consignment)
                    .Include(i => i.Product).ThenInclude(i => i.Category)
                    .Include(i => i.InvoiceOutputDetails).ThenInclude(i => i.OutputBill).ThenInclude(i => i.Customer)
                    .ToList();
            }
        }

        public ConsignmentDetail GetCOnsignmentDetails(int productID, int consignmentID)
        {
            using(var db = new InventoryManagementContext())
            {
                return db.ConsignmentDetails.Where(l => l.ProductId == productID && l.ConsignmentId == consignmentID).FirstOrDefault();
            }
        }
    }
}
