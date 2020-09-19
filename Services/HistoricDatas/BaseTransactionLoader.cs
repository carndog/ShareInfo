using System.IO;
using System.Threading.Tasks;
using ExcelServices;

namespace Services.HistoricDatas
{
    public abstract class BaseTransactionLoader<T> where T : class, new()
    {
        private readonly IExcelLoader _excelLoader;

        private readonly IHistoricEntityService<T> _transactionService;

        public BaseTransactionLoader(
            IExcelLoader excelLoader,
            IHistoricEntityService<T> transactionService)
        {
            _excelLoader = excelLoader;
            _transactionService = transactionService;
        }

        protected abstract Task ProcessFile(string filePath);

        public async Task Load(string transactionsFolderPath)
        {
            if (!string.IsNullOrWhiteSpace(transactionsFolderPath))
            {
                if (Directory.Exists(transactionsFolderPath))
                {
                    string[] allFiles =
                        Directory.GetFiles(transactionsFolderPath, "*.xlsx", SearchOption.AllDirectories);

                    foreach (string filePath in allFiles)
                    {
                        await ProcessFile(filePath);
                    }
                }
            }
        }
    }
}