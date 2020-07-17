using System.Threading.Tasks;
using DTO;

namespace Services
{
    public interface IProcessedInformationService
    {
        Task<Progress> Get();
    }
}