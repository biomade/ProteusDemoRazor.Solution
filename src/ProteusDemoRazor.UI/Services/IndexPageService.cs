using AutoMapper;
using Proteus.UI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Proteus.UI.Services
{
    public class IndexPageService : IIndexPageService
    {
               private readonly IMapper _mapper;

        public IndexPageService(IMapper mapper)
        {
           
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

    }
}
