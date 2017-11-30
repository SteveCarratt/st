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

        public bool IsPositive => _amount >= 0;
        public decimal Score() => _unit.Score(_amount);

        
        public bool Equals(Quantity other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return _amount.Equals(other._amount) && Equals(_unit, other._unit);
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

        public static IEnumerable<Quantity> Add(IEnumerable<Quantity> left, IEnumerable<Quantity> right)
        {
            var groupedRight = right.GroupBy(q => q._unit);

            return left.Select(qleft =>
            {
                if (groupedRight.Any(rightGroup => rightGroup.Key == qleft._unit))
                    return qleft + groupedRight.First(rightGroup => rightGroup.Key == qleft._unit)
                               .Aggregate((l, r) => l + r);
                return qleft;
            }).Union(groupedRight.Where(rightGroup => left.All(qleft => qleft._unit != rightGroup.Key))
                .Select(group => group.Aggregate((l, r) => l + r))).Where(q => q._amount != 0).ToArray();
        }

        public static IEnumerable<Quantity> Subtract(IEnumerable<Quantity> left, IEnumerable<Quantity> right) =>
            Add(right.Select(q => -q), left);

        public static decimal Score(IEnumerable<Quantity> quantities) => quantities.Sum(x => x.Score());
    }

    public static class QuantityExtensions
    {
        public static decimal Score(this IEnumerable<Quantity> quantities) => Quantity.Score(quantities);
    }
}