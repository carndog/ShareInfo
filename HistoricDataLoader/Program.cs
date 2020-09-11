using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using DTO;
using ExcelServices;
using Services;
using Storage;
using Storage.Queries;

namespace HistoricDataLoader
{
    internal static class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Starting loading of etoro closed positions");
            Console.WriteLine("Press any key to continue....");
            Console.ReadKey();

            if (args.Length == 0)
            {
                Console.WriteLine("No args");
                return;  
            }
            
            string closedPositionsFolderPath = args[0];

            if (string.IsNullOrWhiteSpace(closedPositionsFolderPath))
            {
                Console.WriteLine("No folder path for closed positions");
                return;
            }

            if (!Directory.Exists(closedPositionsFolderPath))
            {
                Console.WriteLine("Folder does not exist");
                return;
            }
            
            EtoroClosedPositionService service = new EtoroClosedPositionService(
                new EtoroClosedPositionRepository(),
                new DuplicateEtoroClosedPositionExistsQuery());

            string[] allFiles = Directory.GetFiles(closedPositionsFolderPath, "*.xlsx", SearchOption.AllDirectories);

            foreach (string filePath in allFiles)
            {
                try
                {
                    Console.WriteLine($"Processing file {filePath}");

                    IEnumerable<object> objects = LoadEtoroClosedPositions(filePath);

                    IEnumerable<EtoroClosedPosition>
                        etoroClosedPositions = objects.Cast<EtoroClosedPosition>().ToList();

                    using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                    {
                        foreach (EtoroClosedPosition position in etoroClosedPositions)
                        {
                            Console.WriteLine(
                                $"Adding {position.PositionId}: {position.Action} closed on {position.ClosedDate}");
                            await service.AddAsync(position);
                        }
                        
                        scope.Complete();
                    }

                    Console.WriteLine($"Completed adding {etoroClosedPositions.Count()} positions");
                }
                catch (Exception exception)
                {
                    Console.WriteLine($"Failed to process {filePath}");
                    Console.WriteLine(exception.ToString());
                }
            }
            
            Console.WriteLine($"Completed adding {allFiles.Length} files.  All done.");
        }

        private static IEnumerable<object> LoadEtoroClosedPositions(string closedPositionsFilePath)
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

            ExcelLoader excelLoader = new ExcelLoader();
            IEnumerable<object> objects = excelLoader.Read(excelMapping, closedPositionsFilePath);
            return objects;
        }
    }
}