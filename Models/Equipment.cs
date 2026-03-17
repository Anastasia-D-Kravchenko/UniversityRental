namespace UniversityRental.Models
{
    public class Equipment
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public string Name { get; }
        public bool IsAvailable { get; set; } = true;

        protected Equipment(string name)
        {
            Name = name;
        }

        public override string ToString() => $"{Name} [{(IsAvailable ? "Available" : "Unavailable")}]";
    }
    public class Laptop : Equipment
    {
        public int RamSizeGB { get; }
        public string OsVersion { get; }

        public Laptop(string name, int ramSizeGB, string osVersion) : base(name)
        {
            RamSizeGB = ramSizeGB;
            OsVersion = osVersion;
        }
    }

    public class Projector : Equipment
    {
        public string Resolution { get; }
        public int Lumens { get; }

        public Projector(string name, string resolution, int lumens) : base(name)
        {
            Resolution = resolution;
            Lumens = lumens;
        }
    }

    public class Camera : Equipment
    {
        public int Megapixels { get; }
        public string LensType { get; }

        public Camera(string name, int megapixels, string lensType) : base(name)
        {
            Megapixels = megapixels;
            LensType = lensType;
        }
    }
}