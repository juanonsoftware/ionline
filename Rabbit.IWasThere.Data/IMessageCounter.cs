using System;
using System.Collections.Generic;

namespace Rabbit.IWasThere.Data
{
    public interface IMessageCounter
    {
        /// <summary>
        /// Count messages by each category. Also return total messages in the system into a default category.
        /// </summary>
        IDictionary<Guid, int> CountMessages();

        /// <summary>
        /// Count messages by a given category
        /// </summary>
        int CountMessages(Guid categoryId);

        /// <summary>
        /// Count all messages in the system
        /// </summary>
        int CountAllMessages();
    }
}