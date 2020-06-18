using AutoMapper;
using Proteus.Application.Models;
using Proteus.UI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Proteus.UI.Mapper
{
    public class ProteusProfile: Profile
    {
        public ProteusProfile()
        {
            CreateMap<ProductModel, ProductViewModel>();
            CreateMap<CategoryModel, CategoryViewModel>();
        }
    }
}
