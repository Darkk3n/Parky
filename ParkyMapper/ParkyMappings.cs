using AutoMapper;
using Parky.Models;
using Parky.Models.Dtos;

namespace Parky.ParkyMapper
{
    public class ParkyMappings : Profile
    {
        public ParkyMappings()
        {
            CreateMap<NationalPark, NationalParkDto>().ReverseMap();
        }
    }
}