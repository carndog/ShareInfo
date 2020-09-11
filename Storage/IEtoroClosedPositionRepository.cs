using System.Threading.Tasks;
using DTO;

namespace Storage
{
    public interface IEtoroClosedPositionRepository
    {
        Task<int> AddAsync(EtoroClosedPosition position);

        Task<EtoroClosedPosition> GetAsync(int id);

        Task<bool> ExistsAsync(int id);
    }
}