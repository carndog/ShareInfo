using System.Threading.Tasks;
using DTO;

namespace Storage
{
    public interface IProgressRepository
    {
        Task<Progress> Get();
    }
}