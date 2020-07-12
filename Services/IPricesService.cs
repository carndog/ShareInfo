using System.Threading.Tasks;
using DTO;

namespace Info
{
    public interface IPricesService
    {
        Task Add(AssetPrice assetPrice);
    }
}