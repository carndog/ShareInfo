﻿using System.Threading.Tasks;
using DTO;

namespace Storage
{
    public interface IPriceRepository
    {
        Task<int> AddAsync(AssetPrice price);

        Task<AssetPrice> GetAsync(int id);

        Task<bool> ExistsAsync(int id);
    }
}