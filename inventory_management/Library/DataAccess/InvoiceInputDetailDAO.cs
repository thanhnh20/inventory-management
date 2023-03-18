using Library.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DataAccess
{
    public class InvoiceInputDetailDAO
    {
        private static InvoiceInputDetailDAO instance = null;
        private static readonly object instancelock = new object();
        private InvoiceInputDetailDAO() { }
        public static InvoiceInputDetailDAO Instance
        {
            get
            {
                lock (instancelock)
                {
                    if (instance == null)
                    {
                        instance = new InvoiceInputDetailDAO();
                    }
                    return instance;
                }
            }
        }

        public InvoiceInputDetail getConsignmentDetailIDByInputBill(int intputID)
        {
            using(var db = new InventoryManagementContext())
            {
                return db.InvoiceInputDetails.Where(c => c.InputBillId == intputID).FirstOrDefault();
            }
        }
    }
}
