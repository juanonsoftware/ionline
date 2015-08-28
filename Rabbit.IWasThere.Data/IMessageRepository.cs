﻿using System;
using System.Collections.Generic;
using Rabbit.IWasThere.Domain;

namespace Rabbit.IWasThere.Data
{
    public interface IMessageRepository
    {
        /// <summary>
        /// Get a page of messages
        /// </summary>
        /// <param name="pageIndex">Zero based number</param>
        /// <param name="pageSize"></param>
        IEnumerable<Message> GetMessages(int pageIndex, int pageSize);

        Message GetById(Guid id);

        void Save(Message message);

        int Count();
    }
}
