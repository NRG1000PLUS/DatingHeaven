using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DatingHeaven.DataAccessLayer.Infrastructure.EntityOperations;

namespace DatingHeaven.BusinessLogic {
    public abstract class BaseService{
        private readonly IEntityOperationsProvider _entityContextProvider;

        protected BaseService(){
            // empty constructor
        }

        protected BaseService(IEntityOperationsProvider entityContextProvider){
            _entityContextProvider = entityContextProvider;
            
        }

        protected IEntityOperationsProvider EntityOperations{
            get{
                return _entityContextProvider;
            }
        }
    }
}
