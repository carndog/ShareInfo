using System.Threading.Tasks;
using DTO;

namespace Storage
{
    public interface IPriceRepository
    {
        Task<int> Add(AssetPrice price);

        Task<AssetPrice> Get(int id);
    }
}