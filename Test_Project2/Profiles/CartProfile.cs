using AutoMapper;
using Test_Project2.Models;

namespace Test_Project2.Profiles
{
    public class CartProfile : Profile
    {
        public CartProfile() {
            CreateMap<Cart_Model, Cart_ModelDto>().ReverseMap();
        }
    }
}
