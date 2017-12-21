using System;
using System.Collections.Generic;
using System.Linq;
using static St.Unit;

namespace St
{
    public class ResourceVector

    { 
        private static readonly Quantity NoEnergy = Energy.Points(0);
        private static readonly Quantity NoMineral = Mineral.s(0);
        private static readonly Quantity NoFood = Food.Points(0);
        private static readonly Quantity NoUnity = Unity.Points(0);
        private static readonly Quantity NoInfluence = Influence.Points(0);
        private static readonly Quantity NoPhysics = Physics.Points(0);
        private static readonly Quantity NoBiology = Biology.Points(0);
        private static readonly Quantity NoEngineering = Engineering.Points(0);
        private readonly Quantity _energy = NoEnergy;
        private readonly Quantity _mineral = NoMineral;
        private readonly Quantity _food = NoFood;
        private readonly Quantity _unity = NoUnity;
        private readonly Quantity _influence = NoInfluence;
        private readonly Quantity _physics = NoPhysics;
        private readonly Quantity _biology = NoBiology;
        private readonly Quantity _engineering = NoEngineering;

        public static readonly ResourceVector Empty = new ResourceVector();

        private ResourceVector() { }

        public ResourceVector(double energy, double minerals, double food, double unity, double influence, double physics, double biology, double engineering)
        {
            _energy = Energy.Points(energy);
            _mineral = Mineral.Points(minerals);
            _food = Food.Points(food);
            _unity = Unity.Points(unity);
            _influence = Influence.Points(influence);
            _physics = Physics.Points(physics);
            _biology = Biology.Points(biology);
            _engineering = Engineering.Points(engineering);
        }

        public decimal Score
        {
            get
            {
                if (_energy < Energy.Points(0))
                    return _energy.Score();

                return new[] { _mineral, _food, _unity, _influence, _physics, _biology, _engineering }.Sum(q => q.Score());
            }
        } 

        private IEnumerable<Quantity> Resources => new List<Quantity>
        {
            _mineral,
            _energy,
            _food,
            _influence,
            _unity,
            _physics,
            _biology,
            _engineering
        };

        public ResourceVector(Quantity energy, Quantity minerals, Quantity food, Quantity unity, Quantity influence, Quantity physics, Quantity biology, Quantity engineering)
        {
            _energy = energy;
            _mineral = minerals;
            _food = food;
            _unity = unity;
            _influence = influence;
            _physics = physics;
            _biology = biology;
            _engineering = engineering;
        }

        
        public static ResourceVector operator +(ResourceVector l, ResourceVector r)
        {
            return new ResourceVector(
                l._energy + r._energy,
                l._mineral + r._mineral,
                l._food + r._food,
                l._unity + r._unity,
                l._influence + r._influence,
                l._physics + r._physics,
                l._biology + r._biology,
                l._engineering + r._engineering
            );
        }

        public static ResourceVector operator -(ResourceVector l, ResourceVector r)
        {
            return new ResourceVector(
                l._energy + -r._energy,
                l._mineral + -r._mineral,
                l._food + -r._food,
                l._unity + -r._unity,
                l._influence + -r._influence,
                l._physics + -r._physics,
                l._biology + -r._biology,
                l._engineering + -r._engineering
            );
        }

        public static ResourceVector operator -(ResourceVector l)
        {
            return new ResourceVector(
                -l._energy,
                -l._mineral,
                -l._food,
                -l._unity,
                -l._influence,
                -l._physics,
                -l._biology,
                -l._engineering
            );
        }

        public override bool Equals(object other)
        {
            if (base.Equals(other))
                return true;

            if (other is ResourceVector otherVector)
                return Equals(otherVector);

            return false;
        }

        public bool Equals(ResourceVector other) => Resources.SequenceEqual(other.Resources);

        public ResourceMask Mask
        {
            get
            {
                var result = new ResourceMask(
                    _energy.Equals(NoEnergy) ? 0 : 1,
                    _mineral.Equals(NoMineral) ? 0 : 1,
                    _food.Equals(NoFood) ? 0 : 1,
                    _unity.Equals(NoUnity) ? 0 : 1,
                    _influence.Equals(NoInfluence) ? 0 : 1,
                    _physics.Equals(NoPhysics) ? 0 : 1,
                    _biology.Equals(NoBiology) ? 0 : 1,
                    _engineering.Equals(NoEngineering) ? 0 : 1
                );

                return result.Equals(ResourceMask.NullMask) ? ResourceMask.IdentityMask: result;
            }
        }


        public override string ToString() => string.Join(" - ", Resources);

        public string PrettyPrint() => ToString();

        public class ResourceMask
        {
            private readonly double _energy;
            private readonly double _mineral;
            private readonly double _food;
            private readonly double _unity;
            private readonly double _influence;
            private readonly double _physics;
            private readonly double _biology;
            private readonly double _engineering;
            public static readonly ResourceMask NullMask = new ResourceMask(0,0,0,0,0,0,0,0);
            public static readonly ResourceMask IdentityMask = new ResourceMask(1,1,1,1,1,1,1,1);

            public ResourceMask(double energy, double minerals, double food, double unity, double influence,
                double physics, double biology, double engineering)
            {
                _energy = energy;
                _mineral = minerals;
                _food = food;
                _unity = unity;
                _influence = influence;
                _physics = physics;
                _biology = biology;
                _engineering = engineering;
            }

            private IEnumerable<double> Masks => new List<double>
            {
                _mineral,
                _energy,
                _food,
                _influence,
                _unity,
                _physics,
                _biology,
                _engineering
            };


            public static ResourceVector operator *(ResourceVector v, ResourceMask m)
            {
                return new ResourceVector(
                    v._energy * m._energy,
                    v._mineral * m._mineral,
                    v._food * m._food,
                    v._unity * m._unity,
                    v._influence * m._influence,
                    v._physics * m._physics,
                    v._biology * m._biology,
                    v._engineering * m._engineering
                );
            }

            public static ResourceMask operator +(ResourceMask v, ResourceMask m)
            {
                return new ResourceMask(
                    v._energy + (m._energy-1),
                    v._mineral + (m._mineral-1),
                    v._food + (m._food-1),
                    v._unity + (m._unity-1),
                    v._influence + (m._influence-1),
                    v._physics + (m._physics-1),
                    v._biology + (m._biology-1),
                    v._engineering + (m._engineering - 1)
                );
            }


            public override bool Equals(object other)
            {
                if (base.Equals(other))
                    return true;

                if (other is ResourceMask otherMask)
                    return Equals(otherMask);

                return false;
            }

            public bool Equals(ResourceMask other) => Masks.SequenceEqual(other.Masks);
        }


        public string ConcisePrint()
        {
            return string.Join(",", Resources.Select(q => q.ConcisePrint()));
        }
    }
}
