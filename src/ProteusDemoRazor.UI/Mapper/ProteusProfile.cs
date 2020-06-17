using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Proteus.Application.Models;
using Proteus.UI.ViewModels;

namespace Proteus.UI.Mapper
{
    public class ProteusProfile: Profile
    {
        //automapper profile to convert ViewModels to Models
        public ProteusProfile()
        {
            //CreateMap<ProductModel, ProductViewModel>();
            //CreateMap<CategoryModel, CategoryViewModel>();
        }
    }
}
