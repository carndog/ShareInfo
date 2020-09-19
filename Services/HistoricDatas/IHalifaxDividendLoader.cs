using System.Threading.Tasks;

namespace Services.HistoricDatas
{
    public interface IHalifaxDividendLoader
    {
        Task Load(string transactionsFolderPath);
    }
}