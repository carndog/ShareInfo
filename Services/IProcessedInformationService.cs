using System.Threading.Tasks;
using DTO;

namespace Info
{
    public interface IProcessedInformationService
    {
        Task<Progress> Get();
    }
}