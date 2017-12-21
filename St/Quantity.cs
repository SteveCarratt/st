using System;
using System.Collections.Generic;
using System.Linq;

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

        public decimal Score() => _unit.Score(_amount);
        
        public bool Equals(Quantity other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Math.Abs(_amount - other._amount)<1E-2 && Equals(_unit, other._unit);
        }

        public static bool operator ==(Quantity left, Quantity right)
        {
            if (ReferenceEquals(left, null)) return false;
            return left.Equals(right);
        }

       
        public static bool operator !=(Quantity left, Quantity right)
        {
            return !(left == right);
        }

        public static bool operator >(Quantity left, Quantity right)
        {
            AssertSameUnits(left, right);
            return left._amount > right._amount;
        }

        public static bool operator <(Quantity left, Quantity right)
        {
            return -left > -right;
        }

        public static bool operator >=(Quantity left, Quantity right)
        {
            AssertSameUnits(left, right);
            return left._amount > right._amount || left.Equals(right);
        }

        public static bool operator <=(Quantity left, Quantity right)
        {
            return -left >= -right;
        }

        private static void AssertSameUnits(Quantity left, Quantity right)
        {
            if (left._unit != right._unit)
                throw new InvalidOperationException($"Unit mismatch. L:{left._unit} R:{right._unit}");
        }

        public static Quantity operator ++(Quantity q)
        {
            return q + new Quantity(1, q._unit);
        }

        public static Quantity operator --(Quantity q)
        {
            return q - new Quantity(1, q._unit);
        }

        public static Quantity operator +(Quantity left, Quantity right)
        {
            AssertSameUnits(left, right);
            return new Quantity(left._amount + right._amount, left._unit);
        }

        public static Quantity operator *(Quantity quantity, double scalar)
        {
            return new Quantity(quantity._amount*scalar, quantity._unit);
        }

        public static Quantity operator -(Quantity left, Quantity right)
        {
            return new Quantity(left._amount + -right._amount, left._unit);
        }

        public static Quantity operator -(Quantity q)
        {
            return new Quantity(-q._amount, q._unit);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
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
            return $"{_amount} {_unit}";
        }

        public string ConcisePrint()
        {
            return _amount.ToString() + _unit.ShortName;
        }
    }
}