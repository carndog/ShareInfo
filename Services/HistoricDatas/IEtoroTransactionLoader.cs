﻿using System.Threading.Tasks;

namespace Services.HistoricDatas
{
    public interface IEtoroTransactionLoader
    {
        Task Load(string transactionsFolderPath);
    }
}