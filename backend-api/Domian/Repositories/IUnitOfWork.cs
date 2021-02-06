using System.Threading.Tasks;

namespace backend_api.Domain.Repositories
{
    /// <summary>
    /// Unit of work service
    /// </summary>
    public interface IUnitOfWork
    {
        /// <summary>
        /// Saves the changes made to the db
        /// </summary>
        /// <returns></returns>
        Task CompleteAsync();
    }
}