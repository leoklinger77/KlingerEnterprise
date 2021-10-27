using System.Threading.Tasks;

namespace KlingerSystem.Core.Interfaces
{
    public interface IUnitOfWork
    {
        Task<bool> Commit();
    }
}
