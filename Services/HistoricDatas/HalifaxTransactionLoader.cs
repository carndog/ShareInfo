using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using DTO;
using ExcelServices;

namespace Services.HistoricDatas
{
    public class HalifaxTransactionLoader : BaseTransactionLoader<HalifaxTransaction>, IHalifaxTransactionLoader
    {
        private readonly IExcelLoader _excelLoader;

        private readonly IHalifaxTransactionService _halifaxTransactionService;

        public HalifaxTransactionLoader(
            IExcelLoader excelLoader,
            IHalifaxTransactionService halifaxTransactionService)
        {
            _excelLoader = excelLoader;
            _halifaxTransactionService = halifaxTransactionService;
        }

        protected override async Task ProcessFile(string filePath)
        {
            IEnumerable<object> objects = LoadHalifaxTransactions(filePath);

            IEnumerable<HalifaxTransaction>
                etoroTransactions = objects.Cast<HalifaxTransaction>().ToList();

            using (TransactionScope scope =
                new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                foreach (HalifaxTransaction transaction in etoroTransactions)
                {
                    await _halifaxTransactionService.AddAsync(transaction);
                }

                scope.Complete();
            }
        }

        private IEnumerable<object> LoadHalifaxTransactions(string transactionsFilePath)
        {
            ExcelMapping excelMapping = new ExcelMapping
            {
                SheetIndex = 2,
                TargetProperties = new Dictionary<int, string>
                {
                    {0, "Date"},
                    {1, "AccountBalance"},
                    {2, "Type"},
                    {3, "Details"},
                    {4, "PositionId"},
                    {5, "Amount"},
                    {6, "RealizedEquityChange"},
                    {7, "RealizedEquity"}
                },
                ExpectedColumnHeaders = new Dictionary<int, string>
                {
                    {0, "Date"},
                    {1, "Account Balance"},
                    {2, "Type"},
                    {3, "Details"},
                    {4, "Position Id"},
                    {5, "Amount"},
                    {6, "Realized Equity Change"},
                    {7, "Realized Equity"}
                },
                TargetType = typeof(HalifaxTransaction)
            };

            IEnumerable<object> objects = _excelLoader.Read(excelMapping, transactionsFilePath);
            return objects;
        }
    }
}