namespace B.Framework.Domain.Shared.Attribute
{
 
            public interface IBAttributeBase
            {
                string Value { get; }
                bool Equals(object? obj);
                int GetHashCode();
                bool IsDefaultAttribute();
            }
   
}