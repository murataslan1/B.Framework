namespace B.Framework.Domain.Shared.Attribute
{
    public class BAttributeBase : System.Attribute, IBAttributeBase
    {
        public static BAttributeBase Default = new BAttributeBase();

        public BAttributeBase(string value)
        {
            this.GetValue = value;
        }

        public BAttributeBase() : this(string.Empty)
        {
        }

        public virtual string Value => this.GetValue;

        private string GetValue { get; set; }

        public override bool Equals(object? obj) =>
            obj is BAttributeBase bAttributeBase && bAttributeBase.Value == Value;

        public override int GetHashCode() => Value == null ? 0 : Value.GetHashCode();
        public override bool IsDefaultAttribute() => this.Equals((object) BAttributeBase.Default);

    }
}