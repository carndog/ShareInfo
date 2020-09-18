using System.Threading.Tasks;
using DTO;

namespace DataStorage.Queries
{
    public interface IDuplicateEtoroClosedPositionExistsQuery
    {
        Task<bool> GetAsync(EtoroClosedPosition position);
    }
}