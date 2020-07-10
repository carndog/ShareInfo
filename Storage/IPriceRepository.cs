using DTO;

namespace Storage
{
    public interface IPriceRepository
    {
        void Add(AssetPrice extract);
    }
}