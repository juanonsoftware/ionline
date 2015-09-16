using System;
using Autofac;
using Autofac.Integration.Mvc;
using Rabbit.IWasThere.Data.DocumentDB;
using System.Configuration;

namespace Rabbit.IOnline.App_Start.Autofac
{
    public class AzureDocumentDbDataModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var documentDbAppKey = ConfigurationManager.AppSettings["DocumentDbAppKey"];
            var documentDbUri = ConfigurationManager.AppSettings["DocumentDbUri"];

            documentDbAppKey = Environment.GetEnvironmentVariable("DocumentDbAppKey");
            documentDbUri = Environment.GetEnvironmentVariable("DocumentDbUri");
            
            builder.Register(
                c => new DocumentDbMessageRepository(documentDbAppKey, documentDbUri))
                .AsImplementedInterfaces()
                .InstancePerHttpRequest();

            builder.Register(
                c => new DocumentDbMessageCounter(documentDbAppKey, documentDbUri))
                .AsImplementedInterfaces()
                .InstancePerHttpRequest();

            base.Load(builder);
        }
    }
}