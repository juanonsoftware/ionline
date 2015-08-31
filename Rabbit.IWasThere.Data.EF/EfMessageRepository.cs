using Rabbit.IWasThere.Domain;
using System;
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

        public IEnumerable<Message> GetMessages(int pageIndex, int pageSize)
        {
            var offset = pageIndex * pageSize;
            return _context.Messages.OrderByDescending(x => x.CreatedAt).Skip(() => offset).Take(() => pageSize);
        }

        public IEnumerable<Message> GetMessages(Guid categoryId, int pageIndex, int pageSize)
        {
            var offset = pageIndex * pageSize;
            return _context.Messages.Where(x => x.CategoryId == categoryId).OrderByDescending(x => x.CreatedAt).Skip(() => offset).Take(() => pageSize);
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

        public Message GetById(Guid id)
        {
            return _context.Messages.First(x => x.Id == id);
        }
    }
}
