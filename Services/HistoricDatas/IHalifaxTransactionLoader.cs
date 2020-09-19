using System.Threading.Tasks;

namespace Services.HistoricDatas
{
    public interface IHalifaxTransactionLoader
    {
        Task Load(string transactionsFolderPath);
    }
}