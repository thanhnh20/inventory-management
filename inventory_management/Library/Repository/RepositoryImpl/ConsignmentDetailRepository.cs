using Library.DataAccess;
using Library.Model;
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

        public ConsignmentDetail getConsignmentByID(int consID) => ConsignmentDetailDAO.Instance.getConsignmentByID(consID);

        public List<ConsignmentDetail> getConsignmentIDByProductID(int productID) => ConsignmentDetailDAO.Instance.getConsignmentIDByProductID(productID);

        public IEnumerable<IGrouping<int, ConsignmentDetail>> GetConsignmentDetails(int consignmentID) => ConsignmentDetailDAO.Instance.GetConsignmentDetails(consignmentID);

        public int GetConsignmentIDDetailsByID(int id) => ConsignmentDetailDAO.Instance.GetConsignmentIDDetailsByID((int)id);

        public List<ConsignmentDetail> GetConsignmentDetails() => ConsignmentDetailDAO.Instance.GetConsignmentDetails();

        public List<ConsignmentDetail> GetConsignmentDetailsOutput() => ConsignmentDetailDAO.Instance.GetConsignmentDetailsOutput();
    }
}
