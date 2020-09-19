using System.Threading.Tasks;

namespace Services.HistoricDatas
{
    public interface IEtoroClosedPositionLoader
    {
        Task Load(string closedPositionsFolderPath);
    }
}