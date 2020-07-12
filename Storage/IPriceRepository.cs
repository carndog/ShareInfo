using System.Threading.Tasks;
using DTO;

namespace Storage
{
    public interface IPriceRepository
    {
        Task<bool> Add(AssetPrice price);
    }
}