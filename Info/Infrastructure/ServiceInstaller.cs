using System.Web.Http.Controllers;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Storage;

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
                    Component.For<IProcessedInformationService>().ImplementedBy<ProcessedInformationService>(),
                    Component.For<IProgressRepository>().ImplementedBy<ProgressRepository>()
                );
        }
    }
}