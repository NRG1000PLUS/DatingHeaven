using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using DatingHeaven.DataAccessLayer;
using DatingHeaven.DataAccessLayer.Infrastructure.EntityOperations;
using DatingHeaven.DataAccessLayer.Infrastructure.EntityOperations.SqlGenerators;
using DatingHeaven.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BaseTests {
    [TestClass]
    public class RepositoryTests{

        private IRepository<Message> _messagesRepo;
        private IDbContext _dbContext;
        private Message _newMessage;
        private RandomDataGenerator _randomData;

        [TestInitialize]
        public void Init(){
            _randomData = new RandomDataGenerator();
            _dbContext = new DatingHeavenDbContext();
             _messagesRepo = new EfRepository<Message>(new EntityContextProvider(
               new EntityInfoResolver(), new DatingHeavenDbContext(), 
               new EntitySqlGeneratorsFactory(new EntityInfoResolver())
              ));

            //_dbContext.Database.Delete();
            //_dbContext.Database.CreateIfNotExists();


            _newMessage = new Message{
                ReceiverId = _randomData.RandomInt(),
                SenderId = _randomData.RandomInt(),
                Body = _randomData.GenerateRandomString(100),
                Header = _randomData.GenerateRandomString(200),
                CreatedOn = DateTime.UtcNow,
                ModifiedOn = DateTime.UtcNow,
                GuidValue = Guid.NewGuid(),
                Id = _randomData.RandomInt(),
                IsHidden = false
            };
        }
            
        [TestMethod]
        public void TestMethod1(){
            var messagesDbSet = _dbContext.GetSet<Message>();
            var message = messagesDbSet.Add(_newMessage);
            _dbContext.SaveChanges();



            var returnedMessage = _messagesRepo.GetById(message.Id);
            Assert.IsTrue(returnedMessage != null);
        }
    }
}
