using System;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using DatingHeaven.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BaseTests {
    [TestClass]
    public class LinqExpressions {
        [TestMethod]
        public void build_simple_const_expression(){
            Expression exp = Expression.Constant(123);

            BinaryExpression binExp = Expression.Add(
                  Expression.Constant(32),
                  Expression.Constant(90)
                );
            var lambda = Expression.Lambda(binExp, null);

            var delegateInstance = lambda.Compile();
            var result = delegateInstance.DynamicInvoke(null);
            Debug.WriteLine(result);
        }


        [TestMethod]
        public void two(){
            Expression<Func<Message, object>> propertySelectorExpression = message => message.Header;
            Debug.WriteLine(propertySelectorExpression);
           // ConstantExpression c = Expression.Constant(3);

        }


        [TestMethod]
        public void test_property_accessor_expressions(){
            var message = new Message();
            Expression<Func<Message, object>> propertySelector = m => m.IsRead;
            Debug.WriteLine(propertySelector.Body.GetType());
        }
    }
}
