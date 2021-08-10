using System;
using System.Linq.Expressions;
using B.Framework.Application.Utility;

namespace B.Framework.Application.Extensiosn.Object
{
    public static class ObjectBaseExtensions
    {
        public static T GetPropertyValue<T>(this object obj,Expression<Func<T,string>> exp)
        {
            var memberExpression = exp.Body as MemberExpression;
            Check.Null(memberExpression);
            var value = (T) obj.GetType().GetProperty(memberExpression.Member.Name).GetValue(obj);
            Check.Null(value);
            return value;
        }

        public static T GetPropertyValue<T>(this object obj,string propertyName)
        {
            var propertyInfo = obj.GetType().GetProperty(propertyName);
            Check.Null(propertyInfo);
            return (T) propertyInfo.GetValue(obj);
            


        }
        
        
    }
}