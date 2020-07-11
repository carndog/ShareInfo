using System.Collections.Generic;
using DTO;

namespace Storage
{
    public interface IPriceRepository
    {
        void Add(AssetPrice price);

        IEnumerable<AssetPrice> GetAll();
    }
}