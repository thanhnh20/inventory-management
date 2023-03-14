using AutoMapper;
using Library.Model;

namespace WebApplication.Models.Profiles
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            CreateMap<CustomerViewModel, Customer>().ReverseMap();
        }
    }
}
