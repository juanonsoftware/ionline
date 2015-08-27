using Rabbit.IWasThere.Domain;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Rabbit.IWasThere.Data.EF
{
    public class EfMessageRepository : IMessageRepository
    {
        private readonly AppDbContext _context;

        public EfMessageRepository()
        {
            _context = new AppDbContext();
        }

        public IList<Message> GetMessages(int pageIndex, int pageSize)
        {
            var offset = pageIndex * pageSize;
            return _context.Messages.OrderByDescending(x => x.CreatedAt).Skip(() => offset).Take(() => pageSize).ToList();
        }

        public void Save(Message message)
        {
            if (_context.Messages.Any(x => x.Id == message.Id))
            {

            }
            else
            {
                _context.Messages.Add(message);
            }

            _context.SaveChanges();
        }


        public int Count()
        {
            return _context.Messages.Count();
        }
    }
}
