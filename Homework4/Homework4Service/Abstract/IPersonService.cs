using Homework4Base;
using Homework4Data;
using Homework4Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Homework4Service
{
    public interface IPersonService : IBaseService<PersonDto, Person>
    {
        Task<PaginationResponse<IEnumerable<PersonDto>>> GetPaginationAsync(QueryResource pagination, PersonDto filterResource);
    }
}
