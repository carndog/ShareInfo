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
                SheetIndex = 1,
                TargetProperties = new Dictionary<int, string>
                {
                    {0, "Date"},
                    {1, "Type"},
                    {2, "CompanyCode"},
                    {3, "Exchange"},
                    {4, "Quantity"},
                    {5, "ExecutedPrice"},
                    {6, "NetConsideration"},
                    {7, "Reference"}
                },
                ExpectedColumnHeaders = new Dictionary<int, string>
                {
                    {0, "Date"},
                    {1, "Type"},
                    {2, "Company Code"},
                    {3, "Listed On Market"},
                    {4, "Quantity"},
                    {5, "Executed Price (p)"},
                    {6, "Net Consideration (£)"},
                    {7, "Reference"}
                },
                TargetType = typeof(HalifaxTransaction)
            };

            IEnumerable<object> objects = _excelLoader.Read(excelMapping, transactionsFilePath);
            return objects;
        }
    }
}