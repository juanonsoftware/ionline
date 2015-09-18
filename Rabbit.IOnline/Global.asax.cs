using Rabbit.Web;

namespace Rabbit.IOnline
{
    public class MvcApplication : CustomHttpApplication
    {
        public MvcApplication()
            : base(new ApplicationBehavior())
        {
        }
    }
}