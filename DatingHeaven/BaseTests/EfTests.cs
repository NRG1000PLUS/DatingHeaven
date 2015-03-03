using System;
using System.Data.Entity;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using DatingHeaven.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DatingHeaven.DataAccessLayer;

namespace BaseTests {
    [TestClass]
    public class EfTests{

        private const string DB_NAME = "DatingHeavenDB";
        private readonly DatingHeavenDbContext _dbContext = new DatingHeavenDbContext();

        private Message _insertedMessage;

        

        [TestInitialize]
        public void Init(){
            // create the DB if needed
            if (!_dbContext.Database.Exists()){
                   _dbContext.Database.Create();
            }

            Message newMessage = new Message {
                Body = "Hi! How are you doing?",
                CreatedOn = DateTime.Now,
                ModifiedOn = DateTime.Now,
                Header = "From Jane",
                ReceiverId = 234,
                SenderId = 600
            };

            _dbContext.Messages.Add(newMessage);
            _dbContext.SaveChanges();
        }

       

     

        [TestCleanup]
        public void CleanUp(){
           // _dbContext.Messages.Remove(_insertedMessage);
           // _dbContext.SaveChanges();
        }

        [TestMethod]
        public void get_the_only_one_message(){
            
            Assert.IsTrue( _dbContext.Database.Exists());
            var message = _dbContext.Messages.FirstOrDefault();
            Assert.IsTrue(message != null);
        }

    }
}
