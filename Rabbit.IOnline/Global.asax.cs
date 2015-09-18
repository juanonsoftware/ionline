using Rabbit.iOnline.Ioc;
using Rabbit.iOnline.Ioc.Autofac;
using Rabbit.Web;
using System;
using System.IO;
using System.Linq;
using System.Web.Hosting;

namespace Rabbit.IOnline
{
    public class MvcApplication : CustomHttpApplication
    {
        public MvcApplication()
            : base(new ApplicationBehavior(GetApplicationBootstrap()))
        {
        }

        private static IApplicationBootstrap GetApplicationBootstrap()
        {
            var bootstrapFile = HostingEnvironment.MapPath("~/App_Data/ApplicationBootstrap.txt");
            if (string.IsNullOrWhiteSpace(bootstrapFile) || !File.Exists(bootstrapFile))
            {
                return new AutofacApplicationBootstrap();
            }

            var typeName = File.ReadAllLines(bootstrapFile).First();
            var bootstrapType = Type.GetType(typeName);
            if (bootstrapType == null)
            {
                return new AutofacApplicationBootstrap();
            }

            return (IApplicationBootstrap)Activator.CreateInstance(bootstrapType);
        }
    }
}