using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace St
{
    public class Quantity : IEquatable<Quantity>
    {
        private readonly double _amount;
        private readonly Unit _unit;

        internal Quantity(double amount, Unit unit)
        {
            _amount = amount;
            _unit = unit;
        }

        public bool Equals(Quantity other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return _amount.Equals(other._amount) && Equals(_unit, other._unit);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Quantity) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (_amount.GetHashCode() * 397) ^ (_unit != null ? _unit.GetHashCode() : 0);
            }
        }

        public override string ToString()
        {
            return $"{_amount}-{_unit}";
        }
    }
}
