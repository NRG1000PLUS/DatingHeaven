using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DatingHeaven.DataAccessLayer {
    public interface IPropertySelectionAnalyzer{
        /// <summary>
        /// 
        /// </summary>
        string GetPropertyName<T>(Expression<Func<T, object>> propertySelector) where T: class;


        /// <summary>
        /// 
        /// </summary>
        void ValidateSelector<T>(Expression<Func<T, object>> propertySelector) where T : class;
    }
}
