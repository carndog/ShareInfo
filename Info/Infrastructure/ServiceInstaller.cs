using System.Web.Http.Controllers;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using DataStorage;
using DataStorage.Queries;
using Services;

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
                    Component.For<IPricesService>().LifestylePerWebRequest().ImplementedBy<PricesService>(),
                    Component.For<IPeriodPriceService>().LifestylePerWebRequest().ImplementedBy<PeriodPriceService>(),
                    Component.For<IPriceRepository>().LifestylePerWebRequest().ImplementedBy<PriceRepository>(),
                    Component.For<IPeriodPriceRepository>().LifestylePerWebRequest().ImplementedBy<PeriodPriceRepository>(),
                    Component.For<IDuplicatePriceExistsQuery>().LifestylePerWebRequest().ImplementedBy<DuplicatePriceExistsQuery>(),
                    Component.For<IDuplicatePeriodPriceExistsQuery>().LifestylePerWebRequest().ImplementedBy<DuplicatePeriodPriceExistsQuery>()
                );
        }
    }
}