using System.ComponentModel;
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
                Component.For<IEtoroClosedPositionLoader>().ImplementedBy<EtoroClosedPositionLoader>(),
                Component.For<IEtoroClosedPositionService>().ImplementedBy<EtoroClosedPositionService>(),
                Component.For<IExcelLoader>().ImplementedBy<ExcelLoader>(),
                Component.For<IEtoroClosedPositionRepository>().ImplementedBy<EtoroClosedPositionRepository>(),
                Component.For<IDuplicateEtoroClosedPositionExistsQuery>().ImplementedBy<DuplicateEtoroClosedPositionExistsQuery>()
            );
        }
    }
}