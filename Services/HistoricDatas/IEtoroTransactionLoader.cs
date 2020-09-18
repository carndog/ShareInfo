using System.Threading.Tasks;

namespace Services.HistoricDatas
{
    public interface IEtoroTransactionLoader
    {
        Task CreateEtoroTransactions(string transactionsFolderPath);
    }
}