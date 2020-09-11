using System.Threading.Tasks;
using DTO;

namespace Services
{
    public interface IEtoroClosedPositionService
    {
        Task<int> AddAsync(EtoroClosedPosition etoroClosedPosition);
        
        Task<EtoroClosedPosition> GetAsync(int id);
    }
}