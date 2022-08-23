using AutoMapper;
using Homework4Service;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework4Api.Controller
{
    [ApiController]
    public class BaseController<Dto, Entity> : ControllerBase
    {
        private readonly IBaseService<Dto, Entity> _baseService;
        protected readonly IMapper Mapper;


        public BaseController(IBaseService<Dto, Entity> baseService, IMapper mapper)
        {
            this._baseService = baseService;
            this.Mapper = mapper;
        }
    }
}
