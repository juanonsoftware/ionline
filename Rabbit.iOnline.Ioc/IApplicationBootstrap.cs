using System.Collections.Generic;

namespace Rabbit.iOnline.Ioc
{
    public interface IApplicationBootstrap
    {
        void Initialize(IDictionary<string, object> parameters = null);
    }
}