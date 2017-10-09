using System;

namespace St
{
    public class Resources : IEquatable<Resources>
    {
        private readonly double _energy;
        private readonly double _minerals;

        public Resources(double energy, double minerals)
        {
            _energy = energy;
            _minerals = minerals;
        }

        public bool Equals(Resources other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return _energy.Equals(other._energy) && _minerals.Equals(other._minerals);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Resources) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (_energy.GetHashCode() * 397) ^ _minerals.GetHashCode();
            }
        }
    }
}