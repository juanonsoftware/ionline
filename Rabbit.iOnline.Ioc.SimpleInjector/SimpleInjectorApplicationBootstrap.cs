using Rabbit.IWasThere.Common;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Rabbit.iOnline.Ioc.SimpleInjector
{
    public class SimpleInjectorApplicationBootstrap : IApplicationBootstrap
    {
        public void Initialize(IDictionary<string, object> parameters)
        {
            if (!parameters.ContainsKey(GlobalConstants.ControllersAssembly))
            {
                throw new ArgumentException("You must provide controllers assembly");
            }

            SystemConfig.ConfigDatabase();
            SystemConfig.ConfigDependencyContainer((Assembly)parameters[GlobalConstants.ControllersAssembly]);
        }
    }
}
