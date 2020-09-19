using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using DTO;
using ExcelServices;

namespace Services.HistoricDatas
{
    public class HalifaxDividendLoader : BaseTransactionLoader<HalifaxDividend>, IHalifaxDividendLoader
    {
        private readonly IExcelLoader _excelLoader;

        private readonly IHalifaxDividendService _halifaxDividendService;

        public HalifaxDividendLoader(
            IExcelLoader excelLoader,
            IHalifaxDividendService halifaxDividendService)
        {
            _excelLoader = excelLoader;
            _halifaxDividendService = halifaxDividendService;
        }

        protected override async Task ProcessFile(string filePath)
        {
            IEnumerable<object> objects = LoadDividends(filePath);

            IEnumerable<HalifaxDividend>
                dividends = objects.Cast<HalifaxDividend>().ToList();

            using (TransactionScope scope =
                new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                foreach (HalifaxDividend dividend in dividends)
                {
                    await _halifaxDividendService.AddAsync(dividend);
                }

                scope.Complete();
            }
        }

        private IEnumerable<object> LoadDividends(string dividendsFilePath)
        {
            ExcelMapping excelMapping = new ExcelMapping
            {
                SheetIndex = 0,
                TargetProperties = new Dictionary<int, string>
                {
                    {0, "IssueDate"},
                    {1, "Stock"},
                    {2, "ExDividendDate"},
                    {3, "SharesHeld"},
                    {4, "Amount"},
                    {5, "HandlingOption"}
                },
                ExpectedColumnHeaders = new Dictionary<int, string>
                {
                    {0, "Issue Date"},
                    {1, "Stock"},
                    {2, "XD Date"},
                    {3, "Shares held on XD Date"},
                    {4, "Amount Payable"},
                    {5, "Handling Option"}
                },
                TargetType = typeof(HalifaxDividend)
            };

            IEnumerable<object> objects = _excelLoader.Read(excelMapping, dividendsFilePath);
            return objects;
        }
    }
}