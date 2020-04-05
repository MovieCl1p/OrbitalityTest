using System;

namespace Core.Dispatcher.Signals
{
    public struct BindingId : IEquatable<BindingId>
    {
        private readonly Type _type;
        private readonly string _identifier;

        public Type Type => _type;

        public string Identifier => _identifier;

        public BindingId(Type type, string identifier)
        {
            _type = type;
            _identifier = identifier;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 29 + _type.GetHashCode();
                hash = hash * 29 + ( string.IsNullOrEmpty(_identifier) ? 0 : _identifier.GetHashCode());
                return hash;
            }
        }

        public override bool Equals(object other)
        {
            if (other is BindingId)
            {
                BindingId otherId = (BindingId) other;
                return otherId == this;
            }

            return false;
        }

        public bool Equals(BindingId that)
        {
            return this == that;
        }

        public static bool operator ==(BindingId left, BindingId right)
        {
            return left.Type == right.Type && Equals(left.Identifier, right.Identifier);
        }

        public static bool operator !=(BindingId left, BindingId right)
        {
            return !left.Equals(right);
        }
    }
}