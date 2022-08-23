using Homework4Base;
using Homework4Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Homework4Data
{
    public interface IPersonRepository:IBaseRepository<Person>
    {
        Task<(IEnumerable<Person> records, int total)> GetPaginationAsync(QueryResource pagination, PersonDto filterResource);
        Task<int> TotalRecordAsync();
    }
}
