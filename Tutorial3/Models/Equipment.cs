namespace Tutorial3.Models;

public abstract class Equipment
{
    public Guid Id { get; private set; } = Guid.NewGuid(); // Уникальный ID от системы
    public string Name { get; set; }
    public bool IsAvailable { get; set; } = true;

    protected Equipment(string name)
    {
        Name = name;
    }
}

public class Laptop : Equipment
{
    public int RamSizeGb { get; set; }
    public string ProcessorType { get; set; }
    public Laptop(string name, int ram, string cpu) : base(name) 
    { RamSizeGb = ram; ProcessorType = cpu; }
}

public class Projector : Equipment
{
    public string Resolution { get; set; }
    public int Lumens { get; set; }
    public Projector(string name, string res, int lumens) : base(name) 
    { Resolution = res; Lumens = lumens; }
}

public class Camera : Equipment
{
    public int Megapixels { get; set; }
    public bool HasLensIncluded { get; set; }
    public Camera(string name, int mp, bool lens) : base(name) 
    { Megapixels = mp; HasLensIncluded = lens; }
}