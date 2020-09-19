using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using DTO;
using ExcelServices;

namespace Services.HistoricDatas
{
    public class EtoroClosedPositionLoader : BaseTransactionLoader<EtoroClosedPosition>, IEtoroClosedPositionLoader
    {
        private readonly IEtoroClosedPositionService _etoroClosedPositionService;

        private readonly IExcelLoader _excelLoader;

        public EtoroClosedPositionLoader(
            IExcelLoader excelLoader, 
            IEtoroClosedPositionService etoroClosedPositionService)
        {
            _etoroClosedPositionService = etoroClosedPositionService;
            _excelLoader = excelLoader;
        }

        protected override async Task ProcessFile(string filePath)
        {
            IEnumerable<object> objects = LoadEtoroClosedPositions(filePath);

            IEnumerable<EtoroClosedPosition>
                etoroClosedPositions = objects.Cast<EtoroClosedPosition>().ToList();

            using (TransactionScope scope =
                new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                foreach (EtoroClosedPosition position in etoroClosedPositions)
                {
                    await _etoroClosedPositionService.AddAsync(position);
                }

                scope.Complete();
            }
        }

        private IEnumerable<object> LoadEtoroClosedPositions(string closedPositionsFilePath)
        {
            ExcelMapping excelMapping = new ExcelMapping
            {
                SheetIndex = 1,
                TargetProperties = new Dictionary<int, string>
                {
                    {0, "PositionId"},
                    {1, "Action"},
                    {3, "Amount"},
                    {4, "Units"},
                    {5, "OpenRate"},
                    {6, "CloseRate"},
                    {7, "Spread"},
                    {8, "Profit"},
                    {9, "OpenDate"},
                    {10, "ClosedDate"},
                    {11, "TakeProfitRate"},
                    {12, "StopLossRate"},
                    {13, "RollOverFees"},
                },
                ExpectedColumnHeaders = new Dictionary<int, string>
                {
                    {0, "Position Id"},
                    {1, "Action"},
                    {3, "Amount"},
                    {4, "Units"},
                    {5, "Open Rate"},
                    {6, "Close Rate"},
                    {7, "Spread"},
                    {8, "Profit"},
                    {9, "Open Date"},
                    {10, "Close Date"},
                    {11, "Take Profit Rate"},
                    {12, "Stop Loss Rate"},
                    {13, "RollOver Fees And Dividends"},
                },
                TargetType = typeof(EtoroClosedPosition)
            };

            IEnumerable<object> objects = _excelLoader.Read(excelMapping, closedPositionsFilePath);
            return objects;
        }
    }
}