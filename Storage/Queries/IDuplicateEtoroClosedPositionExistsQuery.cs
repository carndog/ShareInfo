using System.Threading.Tasks;
using DTO;

namespace Storage.Queries
{
    public interface IDuplicateEtoroClosedPositionExistsQuery
    {
        Task<bool> GetAsync(EtoroClosedPosition position);
    }
}