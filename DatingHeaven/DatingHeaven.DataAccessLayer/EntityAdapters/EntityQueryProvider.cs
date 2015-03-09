using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using DatingHeaven.Entities;

namespace DatingHeaven.DataAccessLayer.EntityAdapters {
    class EntityQueryProvider<TEnt, TAdapter> : IQueryProvider 
        where TEnt : BaseEntity where TAdapter: class, IEntityAdapter, new(){
        private readonly DbSet<TEnt> _entitySet;

        public EntityQueryProvider(DbSet<TEnt> entitySet){
            _entitySet = entitySet;
        }



        public IQueryable CreateQuery(Expression expression){
            return new AdapterDbSet<TEnt, TAdapter>(_entitySet);
        }

        public IQueryable<TElement> CreateQuery<TElement>(Expression expression){
            if (typeof (TElement) != typeof (TAdapter)){
                //
                throw new Exception("Must create adapters query");
            }

            IQueryable<TAdapter> query = new AdapterDbSet<TEnt, TAdapter>(_entitySet);

            // cast to the IQueryable<TElement> 
            return (IQueryable<TElement>)query;
        }

        public TResult Execute<TResult>(Expression expression) {
            throw new NotImplementedException();
        }

        public object Execute(Expression expression) {
            throw new NotImplementedException();
        }
    }
}
