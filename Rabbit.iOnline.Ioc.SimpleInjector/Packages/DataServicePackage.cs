using Rabbit.Configuration;
using SimpleInjector;
using SimpleInjector.Packaging;

namespace Rabbit.iOnline.Ioc.SimpleInjector.Packages
{
    public class DataServicePackage : IPackage
    {
        public void RegisterServices(Container container)
        {
            container.RegisterSingleton<IConfiguration, EnvironmentAwareAppSettingsConfiguration>();
            container.RegisterPerWebRequest(DataServiceFactory.Create);
        }
    }
}