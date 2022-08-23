using System;
using System.Threading.Tasks;

namespace Homework4Data
{
    public interface IUnitOfWork:IDisposable
    {
        Task CompleteAsync();
    }
}
