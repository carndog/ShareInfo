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

            IEtoroClosedPositionLoader etoroClosedPositionLoader = windsorContainer.Resolve<IEtoroClosedPositionLoader>();
            IEtoroTransactionLoader etoroTransactionLoader = windsorContainer.Resolve<IEtoroTransactionLoader>();

            await etoroClosedPositionLoader.Load(closedPositionsFolderPath);
            await etoroTransactionLoader.Load(transactionsFolderPath);
        }
    }
}