using Autofac;
using Autofac.Integration.Mvc;
using Rabbit.IWasThere.Data.DocumentDB;

namespace Rabbit.IOnline.App_Start.Autofac
{
    public class AzureDocumentDbDataModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(
                c =>
                    new DocumentDbMessageRepository("https://rabbitvn.documents.azure.com:443/",
                        "czZII6NLz/JgWZ35GTR72+qoSlvyIEDxl7z+86/gcd9LVcHAX25dDJTYqlxz4v35WU8oud1pOvBb+KtFBZGMdg=="))
                .AsImplementedInterfaces()
                .InstancePerHttpRequest();

            builder.Register(
                c =>
                    new DocumentDbMessageCounter("https://rabbitvn.documents.azure.com:443/",
                        "czZII6NLz/JgWZ35GTR72+qoSlvyIEDxl7z+86/gcd9LVcHAX25dDJTYqlxz4v35WU8oud1pOvBb+KtFBZGMdg=="))
                .AsImplementedInterfaces()
                .InstancePerHttpRequest();

            base.Load(builder);
        }
    }
}