using AutoMapper;
using Proteus.Application.ViewModels;
using Proteus.Core.Entities;
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
            CreateMap<Product, ProductViewModel>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.ProductName)).ReverseMap();

            CreateMap<Category, CategoryViewModel>().ReverseMap();
        }
    }
}
