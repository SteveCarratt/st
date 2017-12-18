using System;

namespace St
{
    public class Unit
    {
        private readonly string _name;
        public static readonly Unit Energy = new Unit(nameof(Energy), 1);
        public static readonly Unit Mineral = new Unit(nameof(Mineral), 1);
        public static readonly Unit Food = new Unit(nameof(Food), 0.8m);
        public static readonly Unit Unity = new Unit(nameof(Unity), 0.5m);
        internal static readonly Unit Influence = new Unit(nameof(Influence), 0.5m);
        internal static readonly Unit Physics = new Unit(nameof(Physics), 0.5m);
        internal static readonly Unit Engineering = new Unit(nameof(Engineering), 0.5m);
        internal static readonly Unit Biology = new Unit(nameof(Biology), 0.5m);
        public static readonly Unit Turn = new Unit(nameof(Turn), 0.5m);
        private readonly decimal _factor;


        private Unit(string name, decimal factor)
        {
            _name = name;
            _factor = factor;
        }

        public Quantity s(int amount)
        {
            return new Quantity(amount, this);
        }

        public Quantity Points(double amount)
        {
            return new Quantity(amount, this);
        }

        public decimal Score(double amount) => Convert.ToDecimal(amount) * _factor;

        public override string ToString()
        {
            return _name;
        }
    }
}