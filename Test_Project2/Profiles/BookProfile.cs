using AutoMapper;
using Test_Project2.Models;

namespace Test_Project2.Profiles
{
    public class BookProfile : Profile
    {
        public BookProfile() {
            CreateMap<Books_Model, Books_ModelDto>().ReverseMap();
        }
    }
}
