using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using Dapper.NodaTime;
using DTO;
using DTO.Exceptions;
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
            DapperNodaTimeSetup.Register();

            Console.WriteLine("Starting loading of data");
            Console.WriteLine("Press any key to continue....");
            Console.ReadKey();

            if (args.Length == 0)
            {
                Console.WriteLine("No args");
                return;
            }

            string closedPositionsFolderPath = args[0];
            string transactionsFolderPath = args[1];

            await CreateEtoroClosedPositions(closedPositionsFolderPath);
            await CreateEtoroTransactions(transactionsFolderPath);
        }

        private static async Task CreateEtoroClosedPositions(string closedPositionsFolderPath)
        {
            if (!string.IsNullOrWhiteSpace(closedPositionsFolderPath))
            {
                if (Directory.Exists(closedPositionsFolderPath))
                {
                    EtoroClosedPositionService service = new EtoroClosedPositionService(
                        new EtoroClosedPositionRepository(),
                        new DuplicateEtoroClosedPositionExistsQuery());

                    string[] allFiles =
                        Directory.GetFiles(closedPositionsFolderPath, "*.xlsx", SearchOption.AllDirectories);

                    foreach (string filePath in allFiles)
                    {
                        try
                        {
                            Console.WriteLine($"Processing file {filePath}");

                            IEnumerable<object> objects = LoadEtoroClosedPositions(filePath);

                            IEnumerable<EtoroClosedPosition>
                                etoroClosedPositions = objects.Cast<EtoroClosedPosition>().ToList();

                            using (TransactionScope scope =
                                new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
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
            }
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

        private static async Task CreateEtoroTransactions(string transactionsFolderPath)
        {
            bool exists = await new EtoroTransactionExistsQuery().GetAsync();

            if (exists)
            {
                return;
            }
            
            if (!string.IsNullOrWhiteSpace(transactionsFolderPath))
            {
                if (Directory.Exists(transactionsFolderPath))
                {
                    EtoroTransactionService service = new EtoroTransactionService(
                        new EtoroTransactionRepository());

                    string[] allFiles =
                        Directory.GetFiles(transactionsFolderPath, "*.xlsx", SearchOption.AllDirectories);

                    foreach (string filePath in allFiles)
                    {
                        try
                        {
                            Console.WriteLine($"Processing file {filePath}");

                            IEnumerable<object> objects = LoadEtoroTransactions(filePath);

                            IEnumerable<EtoroTransaction>
                                etoroTransactions = objects.Cast<EtoroTransaction>().ToList();

                            using (TransactionScope scope =
                                new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                            {
                                long fakePositionId = long.MinValue;
                                foreach (EtoroTransaction transaction in etoroTransactions)
                                {
                                    Console.WriteLine(
                                        $"Adding {transaction.PositionId}: {transaction.Type} transacted on {transaction.Date}");

                                    if (transaction.PositionId == 0)
                                    {
                                        transaction.PositionId = fakePositionId;
                                        fakePositionId++;
                                    }

                                    if (transaction.Details == null)
                                    {
                                        transaction.Details = string.Empty;
                                    }

                                    await service.AddAsync(transaction);
                                }

                                scope.Complete();
                            }

                            Console.WriteLine($"Completed adding {etoroTransactions.Count()} transactions");
                        }
                        catch (Exception exception)
                        {
                            Console.WriteLine($"Failed to process {filePath}");
                            Console.WriteLine(exception.ToString());
                        }
                    }

                    Console.WriteLine($"Completed adding {allFiles.Length} files.  All done.");
                }
            }
        }

        private static IEnumerable<object> LoadEtoroTransactions(string transactionsFilePath)
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

            ExcelLoader excelLoader = new ExcelLoader();
            IEnumerable<object> objects = excelLoader.Read(excelMapping, transactionsFilePath);
            return objects;
        }
    }
}