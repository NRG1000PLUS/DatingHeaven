using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DatingHeaven.DataAccessLayer;
using DatingHeaven.DataAccessLayer.Infrastructure.EntityOperations;

namespace DatingHeaven.BusinessLogic {
    public abstract class BaseService{
        private readonly IEntityContextProvider _entityContextProvider;

        protected BaseService(){
            // empty constructor
        }

        protected BaseService(IEntityContextProvider entityContextProvider){
            _entityContextProvider = entityContextProvider;
            
        }

        protected IEntityContextProvider EntityOperations{
            get{
                return _entityContextProvider;
            }
        }
    }
}
