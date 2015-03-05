using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using DatingHeaven.DataAccessLayer;
using DatingHeaven.DataAccessLayer.Infrastructure;
using DatingHeaven.DataAccessLayer.Infrastructure.EntityOperations;
using DatingHeaven.DataAccessLayer.Infrastructure.EntityOperations.SqlGenerators;
using DatingHeaven.Entities;
using DatingHeaven.Entities.Member;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BaseTests {
    [TestClass]
    public class EntityContextTests{
        private IEntityOperationsProvider _entityContextProvider;
        private IEntityTableInfoResolver _entityInfoResolver;
        private IDbContext _dbContext;
        private Message _newMessage;
       

        [TestInitialize]
        public void Init(){
            _entityInfoResolver = new EntityTableInfoResolver();
            _dbContext = new DatingHeavenDbContext();
            _entityContextProvider = new EntityOperationsProvider(
                _entityInfoResolver,
                new EntityOperationsProviderConfig{
                    SqlParameterizationEnabled = true
                }, 
                _dbContext,
                new SqlGeneratorsFactory(_entityInfoResolver));
        }

        [TestMethod]
        public void get_property_value(){
            _newMessage = new Message{
                 SenderId = 332,
                 ReceiverId = 222,
                 Body = "How are you doing, mazafaka?",
                 Header = "Hello",
                 CreatedOn = DateTime.Now,
                 ModifiedOn = DateTime.Now
            };

            var returnedMessage = _dbContext.GetSet<Message>().Add(_newMessage);
            _dbContext.SaveChanges();

            var body = (string)_entityContextProvider.GetProperty<Message>(returnedMessage.Id, "Body");
            Assert.IsTrue(returnedMessage.Body.Equals(body));
        }                         

        [TestCleanup]
        public void End(){
            if (_newMessage != null){
                _dbContext.GetSet<Message>().Remove(_newMessage);
                _dbContext.SaveChanges();
            }
        }
    }
}
