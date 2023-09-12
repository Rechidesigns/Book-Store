using AutoMapper;
using Test_Project2.Models;

namespace Test_Project2.Profiles
{
    public class OrderProfile : Profile
    {
        public OrderProfile() {
            CreateMap<Order_Model, Order_Model>().ReverseMap();
        
        }
    }
}
