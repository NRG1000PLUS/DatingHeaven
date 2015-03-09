using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DatingHeaven.Entities;

namespace DatingHeaven.DataAccessLayer.EntityAdapters {
    class BaseEntityAdapter : IEntityAdapter{
        private BaseEntity _entity;


        public bool IsHidden{
            get; 
            set; 
        }

        public BaseEntity Entity {
            get{
                return _entity;
            }
            set{
                _entity = value;
            }
        }
    }
}
