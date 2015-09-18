using Rabbit.iOnline.Ioc.Autofac;
using Rabbit.Web;

namespace Rabbit.IOnline
{
    public class MvcApplication : CustomHttpApplication
    {
        public MvcApplication()
            : base(new ApplicationBehavior(new AutofacApplicationBootstrap()))
        {
        }
    }
}