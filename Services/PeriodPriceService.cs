﻿using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using System.Transactions;
using DataStorage;
using DataStorage.Queries;
using DTO;
using DTO.Exceptions;
using log4net;
using NodaTime;

namespace Services
{
    public class PeriodPriceService : IPeriodPriceService
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        
        private readonly IPeriodPriceRepository _periodPriceRepository;

        private readonly IDuplicatePeriodPriceExistsQuery _duplicatePeriodPriceExistsQuery;

        private readonly IGetPeriodPriceBySymbolQuery _getPeriodPriceBySymbolQuery;

        public PeriodPriceService(
            IPeriodPriceRepository periodPriceRepository, 
            IDuplicatePeriodPriceExistsQuery duplicatePeriodPriceExistsQuery, 
            IGetPeriodPriceBySymbolQuery getPeriodPriceBySymbolQuery)
        {
            _periodPriceRepository = periodPriceRepository;
            _duplicatePeriodPriceExistsQuery = duplicatePeriodPriceExistsQuery;
            _getPeriodPriceBySymbolQuery = getPeriodPriceBySymbolQuery;
        }

        public async Task<int> AddAsync(PeriodPrice periodPrice)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                periodPrice.PeriodPriceId = Guid.NewGuid();
                
                bool exists = await _duplicatePeriodPriceExistsQuery.GetAsync(periodPrice);

                if (!exists)
                {
                    int id = await _periodPriceRepository.AddAsync(periodPrice).ConfigureAwait(false);

                    scope.Complete();
                    
                    return id;
                }

                throw new DuplicateExistsException();
            }
        }

        public async Task<IEnumerable<PeriodPrice>> AddListAsync(IEnumerable<PeriodPrice> periodPrices)
        {
            try
            {
                foreach (PeriodPrice periodPrice in periodPrices)
                {
                    periodPrice.PeriodPriceId = Guid.NewGuid();

                    bool exists = await _duplicatePeriodPriceExistsQuery.GetAsync(periodPrice);

                    if (!exists)
                    {
                        int id = await _periodPriceRepository.AddAsync(periodPrice);
                        periodPrice.Id = id;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString());
                throw;
            }

            return periodPrices;
        }

        public async Task<PeriodPrice> GetAsync(int id)
        {
            if (await _periodPriceRepository.ExistsAsync(id))
            {
                PeriodPrice periodPrice = await _periodPriceRepository.GetAsync(id);
                return periodPrice;
            }

            throw new PeriodPriceNotFoundException();
        }

        public async Task<LocalDate?> GetLatestAsync(string symbol)
        {
            LocalDate? latest = await _periodPriceRepository.GetLatestAsync(symbol);
            return latest;
        }

        public async Task<PeriodPriceCollection> GetAsync(string symbol)
        {
            return await _getPeriodPriceBySymbolQuery.GetAsync(symbol);
        }
    }
}