using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using B.Framework.Application.Utility;
using B.Framework.Domain.Shared.Attribute;

namespace B.Framework.Application.Attribute.Extensions
{
    public static class BAttributeExtensiosn
    {
        public static string GetBAttributevalue<T>(this T obj, Expression<Func<T, string>> value)
        {
            var memberExpression = value.Body as MemberExpression;
            Check.Null(memberExpression,AttributeDefinations.Errors.GetBAttributeValueCannotBeNull);
            var attr = memberExpression?.Member.GetCustomAttributes(typeof(BAttributeBase), true).FirstOrDefault() as BAttributeBase;
            Check.Null(attr);
            return  attr == null ? memberExpression?.Member.Name : attr.Value;
        }

        public static string GetBAttributeValue(this PropertyInfo property)
        {
            var attribute = property.GetCustomAttribute<BAttributeBase>();
            Check.Null(attribute,AttributeDefinations.Errors.GetBAttributeValueCannotBeNull);
            return attribute?.Value;
        }

        public static string GetBEnumValue<T>(this Enum obj) where T : BAttributeBase
        {
            var fi = obj.GetType().GetField(obj.ToString() ?? string.Empty);
            var attribute = fi.GetCustomAttribute<T>(true);
            Check.Null(attribute,AttributeDefinations.Errors.GetBAttributeValueCannotBeNull);
            if (string.IsNullOrWhiteSpace(attribute.Value))
                return string.Empty;
            return attribute.Value;

        }

    }
}