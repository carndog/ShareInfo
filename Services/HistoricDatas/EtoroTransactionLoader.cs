using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using DTO;
using ExcelServices;

namespace Services.HistoricDatas
{
    public class EtoroTransactionLoader : BaseTransactionLoader<EtoroTransaction>, IEtoroTransactionLoader
    {
        private readonly IExcelLoader _excelLoader;

        private readonly IEtoroTransactionService _etoroTransactionService;

        public EtoroTransactionLoader(
            IExcelLoader excelLoader,
            IEtoroTransactionService etoroTransactionService)
        {
            _excelLoader = excelLoader;
            _etoroTransactionService = etoroTransactionService;
        }

        protected override async Task ProcessFile(string filePath)
        {
            IEnumerable<object> objects = LoadEtoroTransactions(filePath);

            IEnumerable<EtoroTransaction>
                etoroTransactions = objects.Cast<EtoroTransaction>().ToList();

            using (TransactionScope scope =
                new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                long fakePositionId = long.MinValue;
                foreach (EtoroTransaction transaction in etoroTransactions)
                {
                    if (transaction.PositionId == 0)
                    {
                        transaction.PositionId = fakePositionId;
                        fakePositionId++;
                    }

                    if (transaction.Details == null)
                    {
                        transaction.Details = string.Empty;
                    }

                    await _etoroTransactionService.AddAsync(transaction);
                }

                scope.Complete();
            }
        }

        private IEnumerable<object> LoadEtoroTransactions(string transactionsFilePath)
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
                TargetType = typeof(EtoroTransaction)
            };

            IEnumerable<object> objects = _excelLoader.Read(excelMapping, transactionsFilePath);
            return objects;
        }
    }
}