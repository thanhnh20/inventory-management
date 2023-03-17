using Library.DataAccess;
using Library.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Library.Repository.RepositoryImpl
{
    public class InvoiceOutputRepositoryIml : IInvoiceOutputRepository
    {
        public Task<bool> Add(InvoiceOutput entity)
        {
            throw new NotImplementedException();
        }

        public bool CreateInvoiceOutput(InvoiceOutput invoiceOutput, List<Product> listProduct) => InvoiceOutputDAO.Instance.CreateInvoiceOutput(invoiceOutput, listProduct);

        public Task<bool> Delete(InvoiceOutput entity)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<InvoiceOutput>> GetMany(Expression<Func<InvoiceOutput, bool>> expression = null)
        {
            throw new NotImplementedException();
        }

        public Task<InvoiceOutput> GetOne(Expression<Func<InvoiceOutput, bool>> expression = null)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(InvoiceOutput entity)
        {
            throw new NotImplementedException();
        }
    }
}
