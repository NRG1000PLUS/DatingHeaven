using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DatingHeaven.DataAccessLayer {
    public class PropertySelectionAnalyzer : IPropertySelectionAnalyzer {
        public string GetPropertyName<T>(Expression<Func<T, object>> propertySelector) where T : class{
            MemberExpression expression = null;

            if (propertySelector.Body.NodeType == ExpressionType.MemberAccess){
                expression = (MemberExpression) propertySelector.Body;
            }  else if (propertySelector.Body.NodeType == ExpressionType.Convert){
                expression = (MemberExpression) (((UnaryExpression) propertySelector.Body).Operand);
            }

            return (expression != null)
                ? GetPropertyNameInternal<T>(expression)
                : null;
        }

        private string GetPropertyNameInternal<T>(MemberExpression memberExp){
            int dotIndex = memberExp.Member.Name.IndexOf('.');
            string propertyName = memberExp.Member.Name;
            if (dotIndex != (-1)){
                // delete any symbols before the '.' (dot) symbol
                propertyName = propertyName.Substring(dotIndex + 1);
            }
            return propertyName;
        }

        public void ValidateSelector<T>(Expression<Func<T, object>> propertySelector) where T : class {
            if (propertySelector == null){
                // NULL 
                throw new NullReferenceException("Parameter <propertySelector> is NULL (not defined)");
            }

            var isValid = propertySelector.Body.NodeType == ExpressionType.MemberAccess ||
                          propertySelector.Body.NodeType == ExpressionType.Convert;

            if (!isValid){
                const string exMsg = @"Expression must be of type <MemberAccess> or <Convert>";
                var ex = new ArgumentException(exMsg);
                throw ex;
            }

            EnsureIsMemberAccessExpression(propertySelector);
        }

        private void EnsureIsMemberAccessExpression<T>(Expression<Func<T, object>> propertySelector){
            MemberExpression memberExp = propertySelector.Body as MemberExpression;
            if (memberExp == null){
                UnaryExpression unaryExp = propertySelector.Body as UnaryExpression;
                if ((unaryExp == null) || (unaryExp.NodeType != ExpressionType.Convert)){
                    throw new ArgumentException("Must be a <Convert> expression");
                }

                var operandExp = unaryExp.Operand as MemberExpression;
                if (operandExp == null){
                    throw new ArgumentException("propertySelector");
                }
            }
        }
    }
}
