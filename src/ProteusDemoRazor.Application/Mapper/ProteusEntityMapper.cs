using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proteus.Application.Mapper
{
    public class ProteusEntityMapper : Profile
    {
        public ProteusEntityMapper()
        {
            //Create the mapps between the Entities and Models
            //CreateMap<Product, ProductModel>()
            //    .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.ProductName)).ReverseMap();

            //CreateMap<Category, CategoryModel>().ReverseMap();
        }
    }
}
