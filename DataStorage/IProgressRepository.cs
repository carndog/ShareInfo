using System.Threading.Tasks;
using DTO;

namespace DataStorage
{
    public interface IProgressRepository
    {
        Task<Progress> Get();
    }
}