using static St.ResourceVector;

namespace St
{
    public class ResourceVectorBuilder
    {
        private double? _energy;
        private double? _mineral;
        private double? _food;
        private double? _unity;
        private double? _influence;
        private double? _physics;
        private double? _engineering;
        private double? _biology;

        public static ResourceVectorBuilder RVB=> new ResourceVectorBuilder();
        

        public ResourceVectorBuilder Energy(double amount)
        {
            _energy = amount;
            return this;
        }

        public ResourceVectorBuilder Mineral(double amount)
        {
            _mineral = amount;
            return this;
        }

        public ResourceVectorBuilder Food(double amount)
        {
            _food = amount;
            return this;
        }

        public ResourceVectorBuilder Unity(double amount)
        {
            _unity = amount;
            return this;
        }

        public ResourceVectorBuilder Influence(double amount)
        {
            _influence = amount;
            return this;
        }

        public ResourceVectorBuilder Physics(double amount)
        {
            _physics = amount;
            return this;
        }

        public ResourceVectorBuilder Engineering(double amount)
        {
            _engineering = amount;
            return this;
        }

        public ResourceVectorBuilder Society(double amount)
        {
            _biology = amount;
            return this;
        }

        public ResourceVector Vector => new ResourceVector(
            _energy ?? 0,
            _mineral ?? 0,
            _food ?? 0, 
            _unity ?? 0, 
            _influence ?? 0,
            _physics ?? 0,
            _biology ?? 0,
            _engineering ?? 0);
        public ResourceMask Mask => new ResourceMask(
            _energy?? 1,
            _mineral?? 1,
            _food?? 1,
            _unity?? 1,
            _influence?? 1 ,
            _physics?? 1,
            _biology?? 1 ,
            _engineering ?? 1);
    }
}