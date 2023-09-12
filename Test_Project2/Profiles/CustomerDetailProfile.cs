using AutoMapper;
using Test_Project2.Models;

namespace Test_Project2.Profiles
{
    public class CustomerDetailProfile : Profile
    {
        public CustomerDetailProfile() {
            CreateMap<Customer_Details, Customer_DetailsDto>().ReverseMap();
        }
    }
}
