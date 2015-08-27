using System.Collections.Generic;
using Rabbit.IWasThere.Domain;

namespace Rabbit.IWasThere.Data
{
    public interface IMessageRepository
    {
        IList<Message> GetMessages(int pageIndex, int pageSize);

        void Save(Message message);

        int Count();
    }
}
