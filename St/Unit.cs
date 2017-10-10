using System;

namespace St
{
    public class Unit
    {
        private readonly string _name;
        public static readonly Unit Energy = new Unit(nameof(Energy));
        public static readonly Unit Mineral = new Unit(nameof(Mineral));
        public static readonly Unit Food = new Unit(nameof(Food));
        internal static readonly Unit Unity = new Unit(nameof(Unity));
        internal static readonly Unit Influence = new Unit(nameof(Influence));
        internal static readonly Unit Physics = new Unit(nameof(Physics));
        internal static readonly Unit Engineering = new Unit(nameof(Engineering));
        internal static readonly Unit Biology = new Unit(nameof(Biology));
        public static readonly Unit Turn = new Unit(nameof(Turn));


        private Unit(string name)
        {
            _name = name;
        }

        public Quantity s(int amount)
        {
            return new Quantity(amount, this);
        }

        public Quantity Points(double amount)
        {
            return new Quantity(amount, this);
        }

        public override string ToString()
        {
            return _name;
        }
    }
}