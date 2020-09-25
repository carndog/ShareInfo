using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using DataStorage;
using DataStorage.Queries;
using ExcelServices;
using Services;
using Services.HistoricDatas;
using Component = Castle.MicroKernel.Registration.Component;

namespace HistoricDataLoader
{
    public class HistoricDataInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<IExcelLoader>().ImplementedBy<ExcelLoader>(),
                Component.For<IEtoroClosedPositionLoader>().ImplementedBy<EtoroClosedPositionLoader>(),
                Component.For<IEtoroClosedPositionService>().ImplementedBy<EtoroClosedPositionService>(),
                Component.For<IEtoroClosedPositionRepository>().ImplementedBy<EtoroClosedPositionRepository>(),
                Component.For<IDuplicateEtoroClosedPositionExistsQuery>().ImplementedBy<DuplicateEtoroClosedPositionExistsQuery>(),
                Component.For<IEtoroTransactionLoader>().ImplementedBy<EtoroTransactionLoader>(),
                Component.For<IEtoroTransactionService>().ImplementedBy<EtoroTransactionService>(),
                Component.For<IEtoroTransactionRepository>().ImplementedBy<EtoroTransactionRepository>(),
                Component.For<IDuplicateEtoroTransactionExistsQuery>().ImplementedBy<DuplicateDuplicateEtoroTransactionExistsQuery>(),
                Component.For<IHalifaxTransactionLoader>().ImplementedBy<HalifaxTransactionLoader>(),
                Component.For<IHalifaxTransactionService>().ImplementedBy<HalifaxTransactionService>(),
                Component.For<IHalifaxTransactionRepository>().ImplementedBy<HalifaxTransactionRepository>(),
                Component.For<IDuplicateHalifaxTransactionExistsQuery>().ImplementedBy<DuplicateHalifaxTransactionExistsQuery>(),
                Component.For<IHalifaxDividendLoader>().ImplementedBy<HalifaxDividendLoader>(),
                Component.For<IHalifaxDividendService>().ImplementedBy<HalifaxDividendService>(),
                Component.For<IHalifaxDividendRepository>().ImplementedBy<HalifaxDividendRepository>(),
                Component.For<IDuplicateHalifaxDividendExistsQuery>().ImplementedBy<DuplicateHalifaxDividendExistsQuery>()
            );
        }
    }
}