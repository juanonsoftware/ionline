using System;
using System.Collections.Generic;

namespace Rabbit.IWasThere.Data
{
    public interface IMessageCounter
    {
        IDictionary<Guid, int> CountMessages();
    }
}