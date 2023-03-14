using AutoMapper;
using Library.Model;

namespace WebApplication.Models.Profiles
{
    public class SupplierProfile : Profile
    {
        public SupplierProfile()
        {
            CreateMap<SupplierViewModel, Suplier>().ReverseMap();
        }
    }
}
