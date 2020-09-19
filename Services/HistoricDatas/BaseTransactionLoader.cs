using System.IO;
using System.Threading.Tasks;

namespace Services.HistoricDatas
{
    public abstract class BaseTransactionLoader<T> where T : class, new()
    {
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