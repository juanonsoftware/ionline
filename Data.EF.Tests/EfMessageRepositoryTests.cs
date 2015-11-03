using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Rabbit.IWasThere.Data;
using Rabbit.IWasThere.Data.EF;
using Rabbit.IWasThere.Domain;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Data.EF.Tests
{
    [TestClass]
    public class EfMessageRepositoryTests
    {
        private IDbContext _dbContext;

        private IMessageRepository CreateSUT()
        {
            return new EfMessageRepository(_dbContext);
        }

        [TestMethod]
        public void CanGetMessages()
        {
            // Arrange
            SetupForGetMessages();
            var sut = CreateSUT();

            // Act
            var messages = sut.GetMessages(0, 2).ToList();

            // Assert
            Assert.AreEqual(2, messages.Count());
            Assert.IsTrue(messages.Any(x => x.Body == "B1"));
            Assert.IsTrue(messages.Any(x => x.Body == "B2"));
        }

        private void SetupForGetMessages()
        {
            var list1 = new List<Message>()
            {
                new Message()
                {
                    Body = "B1"
                },
                new Message()
                {
                    Body = "B2"
                }
            }.AsQueryable();

            var mockMessages = new Mock<DbSet<Message>>();
            mockMessages.As<IQueryable<Message>>().Setup(m => m.Provider).Returns(list1.Provider);
            mockMessages.As<IQueryable<Message>>().Setup(m => m.Expression).Returns(list1.Expression);
            mockMessages.As<IQueryable<Message>>().Setup(m => m.ElementType).Returns(list1.ElementType);
            mockMessages.As<IQueryable<Message>>().Setup(m => m.GetEnumerator()).Returns(list1.GetEnumerator());

            var mockContext = new Mock<AppDbContext>();
            mockContext.Setup(c => c.Messages).Returns(mockMessages.Object);

            _dbContext = mockContext.Object;
        }
    }
}
