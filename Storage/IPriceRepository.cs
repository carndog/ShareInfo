using DTO;

namespace Storage
{
    public interface IPriceRepository
    {
        bool Add(AssetPrice price);
    }
}