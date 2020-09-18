using System.Threading.Tasks;

namespace Services.HistoricDatas
{
    public interface IEtoroClosedPositionLoader
    {
        Task CreateEtoroClosedPositions(string closedPositionsFolderPath);
    }
}