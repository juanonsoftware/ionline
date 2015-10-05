using log4net;
using Rabbit.IWasThere.Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Reflection;

namespace Rabbit.iOnline.Ioc.SimpleInjector
{
    public class SimpleInjectorApplicationBootstrap : IApplicationBootstrap
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(SimpleInjectorApplicationBootstrap));

        public void Initialize(IDictionary<string, object> parameters)
        {
            if (!parameters.ContainsKey(GlobalConstants.ControllersAssembly))
            {
                throw new ArgumentException("You must provide controllers assembly");
            }

            var dbSystem = ConfigurationManager.AppSettings[GlobalConstants.DatabaseSystem];
            Logger.InfoFormat("DbSystem is: {0}", dbSystem);

            SystemConfig.ConfigDatabase(dbSystem);
            SystemConfig.ConfigDependencyContainer(dbSystem, (Assembly)parameters[GlobalConstants.ControllersAssembly]);
        }
    }
}
