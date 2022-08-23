using AutoMapper;
using Homework4Base;
using Homework4Data;
using Homework4Dto;
using Homework4Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework4Api.Controller
{
    [Route("api/v1/homework/[controller]")]
    [ApiController]
    public class PersonController : BaseController<PersonDto, Person>
    {
        private readonly IPersonService personService;
        private readonly IMemoryCache memoryCache;
        public PersonController(IPersonService personService, IMapper mapper, IMemoryCache memoryCache) : base(personService, mapper)
        {
            this.personService = personService;
            this.memoryCache = memoryCache;

        }
        [HttpGet]
        public async Task<IActionResult> GetPaginationAsync([FromQuery] string cacheKey, [FromQuery] int page, [FromQuery] int pageSize)
        {

            if (!memoryCache.TryGetValue(cacheKey, out var casheList))
            {
                PersonDto query = new PersonDto()
                {
                   
                    Id = 3,
                    FirstName = "Ali",
                    LastName = "Kaya",
                    Email="abcd@gmail.com",
                    Description="erkek",
                    Phone="5632147896",
                    StaffId = "2"
                    
                };

                QueryResource pagintation = new QueryResource(page, pageSize);
                var result = await personService.GetPaginationAsync(pagintation, query);

                if (!result.Success)
                    return BadRequest(result);

                if (result.Response is null)
                    return NoContent();

                var cacheExpOptions = new MemoryCacheEntryOptions
                {
                    AbsoluteExpiration = DateTime.Now.AddMinutes(30),
                    Priority = CacheItemPriority.Normal
                };

                //set cashe
                memoryCache.Set(cacheKey, result, cacheExpOptions);
                return Ok(result);
            }
            return Ok(casheList);
        }
    }
}
