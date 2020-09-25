using System;
using System.Threading.Tasks;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Dapper.NodaTime;
using Services.HistoricDatas;

namespace HistoricDataLoader
{
    internal static class Program
    {
        static async Task Main(string[] args)
        {
            DapperNodaTimeSetup.Register();
            HistoricDataInstaller installer = new HistoricDataInstaller();
            IWindsorContainer windsorContainer = new WindsorContainer();
            installer.Install(windsorContainer, new DefaultConfigurationStore());

            if (args.Length == 0)
            {
                Console.WriteLine("No args");
                return;
            }
            
            if (args.Length != 2)
            {
                Console.WriteLine("Needs 2 args");
                return;
            }

            string command = args[0];
            string path = args[1];

            if (command.Equals("etoroPositions", StringComparison.InvariantCultureIgnoreCase))
            {
                IEtoroClosedPositionLoader etoroClosedPositionLoader = windsorContainer.Resolve<IEtoroClosedPositionLoader>();
                await etoroClosedPositionLoader.Load(path);
            }
            else if (command.Equals("etoroTransactions", StringComparison.InvariantCultureIgnoreCase))
            {
                IEtoroTransactionLoader etoroTransactionLoader = windsorContainer.Resolve<IEtoroTransactionLoader>();
                await etoroTransactionLoader.Load(path);
            }
            else if (command.Equals("halifaxTransactions", StringComparison.InvariantCultureIgnoreCase))
            {
                IHalifaxTransactionLoader halifaxTransactionLoader = windsorContainer.Resolve<IHalifaxTransactionLoader>();
                await halifaxTransactionLoader.Load(path);
            }
            else if (command.Equals("halifaxDividends", StringComparison.InvariantCultureIgnoreCase))
            {
                IHalifaxDividendLoader halifaxDividendLoader = windsorContainer.Resolve<IHalifaxDividendLoader>();
                await halifaxDividendLoader.Load(path);
            }
            
            Console.WriteLine("Data loaded");
            Console.WriteLine("Press any key to continue....");
        }
    }
}