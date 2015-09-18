using Rabbit.iOnline.Ioc.SimpleInjector;
using Rabbit.Web;

namespace Rabbit.IOnline
{
    public class MvcApplication : CustomHttpApplication
    {
        public MvcApplication()
            //: base(new ApplicationBehavior(new AutofacApplicationBootstrap()))
            : base(new ApplicationBehavior(new SimpleInjectorApplicationBootstrap()))
        {
        }
    }
}