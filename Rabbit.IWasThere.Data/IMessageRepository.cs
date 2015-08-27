using System;
using System.Collections.Generic;
using Rabbit.IWasThere.Domain;

namespace Rabbit.IWasThere.Data
{
    public interface IMessageRepository
    {
        IList<Message> GetMessages(int pageIndex, int pageSize);

        Message GetById(Guid id);

        void Save(Message message);

        int Count();
    }
}
