using System.Web.Http.Controllers;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using DataStorage;
using DataStorage.Queries;
using Services;
using Services.Utilities;

namespace Info.Infrastructure
{
    public class ServiceInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Classes.FromThisAssembly()
                .BasedOn<IHttpController>()
                .LifestylePerWebRequest());
            
                container.Register(
                    Component.For<IPricesService>().LifestylePerWebRequest().ImplementedBy<PricesServiceDecorator>(),
                    Component.For<IPricesService>().LifestylePerWebRequest().ImplementedBy<PricesService>(),
                    Component.For<IPriceStreamService>().LifestylePerWebRequest().ImplementedBy<PriceStreamServiceDecorator>(),
                    Component.For<IPriceStreamService>().LifestylePerWebRequest().ImplementedBy<PriceStreamService>(),
                    Component.For<IPeriodPriceService>().LifestylePerWebRequest().ImplementedBy<PeriodPriceService>(),
                    Component.For<IPriceRepository>().LifestylePerWebRequest().ImplementedBy<PriceRepository>(),
                    Component.For<IPeriodPriceRepository>().LifestylePerWebRequest().ImplementedBy<PeriodPriceRepository>(),
                    Component.For<IPriceStreamRepository>().LifestylePerWebRequest().ImplementedBy<PriceStreamRepository>(),
                    Component.For<IDuplicatePriceExistsQuery>().LifestylePerWebRequest().ImplementedBy<DuplicatePriceExistsQuery>(),
                    Component.For<IDuplicatePeriodPriceExistsQuery>().LifestylePerWebRequest().ImplementedBy<DuplicatePeriodPriceExistsQuery>(),
                    Component.For<IDuplicatePriceStreamExistsQuery>().LifestylePerWebRequest().ImplementedBy<DuplicatePriceStreamExistsQuery>(),
                    Component.For<IGetPeriodPriceBySymbolQuery>().LifestylePerWebRequest().ImplementedBy<GetPeriodPriceBySymbolQuery>(),
                    Component.For<IGetPriceStreamBySymbolQuery>().LifestylePerWebRequest().ImplementedBy<GetPriceStreamBySymbolQuery>(),
                    Component.For<IGetDateTime>().LifestylePerWebRequest().ImplementedBy<GetDateTime>(),
                    Component.For<IIsMarketHoursFactory>().LifestylePerWebRequest().ImplementedBy<IsMarketHoursFactory>()
                );
        }
    }
}