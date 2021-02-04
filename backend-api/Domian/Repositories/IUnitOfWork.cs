using System.Threading.Tasks;

namespace backend_api.Domain.Repositories
{
    public interface IUnitOfWork
    {
        Task CompleteAsync();
    }
}