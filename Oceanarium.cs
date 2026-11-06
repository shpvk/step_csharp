class SeaItem
{
    public string Name { get; private set; }
    public int Age { get; private set; }

    public SeaItem(string name, int age)
    {
        if (name == null)
        {
            throw new Exception("Name cannot be empty");
        }
        if (age < 0)
        {
            throw new Exception("Age cannot be negative");
        }
        Name = name;
        Age = age;
    }

    public virtual string GetDetails()
    {
        return "";
    }

    public void ShowInfo()
    {
        Console.WriteLine(GetType().Name + " " + Name + ", age " + Age + ", " + GetDetails());
    }
}

class Shark : SeaItem
{
    public int TeethCount { get; private set; }

    public Shark(string name, int age, int teethCount) : base(name, age)
    {
        if (teethCount < 0)
        {
            throw new Exception("Teeth count cannot be negative");
        }
        TeethCount = teethCount;
    }

    public override string GetDetails()
    {
        return "teeth: " + TeethCount;
    }
}

class Dolphin : SeaItem
{
    public int TricksCount { get; private set; }

    public Dolphin(string name, int age, int tricksCount) : base(name, age)
    {
        if (tricksCount < 0)
        {
            throw new Exception("Tricks count cannot be negative");
        }
        TricksCount = tricksCount;
    }

    public override string GetDetails()
    {
        return "knows tricks: " + TricksCount;
    }
}

class Octopus : SeaItem
{
    public string Color { get; private set; }

    public Octopus(string name, int age, string color) : base(name, age)
    {
        if (color == null)
        {
            throw new Exception("Color cannot be empty");
        }
        Color = color;
    }

    public override string GetDetails()
    {
        return "color: " + Color;
    }
}

class Oceanarium
{
    public string Name { get; private set; }
    public List<SeaItem> Inhabitants { get; private set; } = new List<SeaItem>();

    public Oceanarium(string name)
    {
        if (name == null)
        {
            throw new Exception("Name cannot be empty");
        }
        Name = name;
    }

    public void AddInhabitant(SeaItem inhabitant)
    {
        if (inhabitant == null)
        {
            throw new Exception("Inhabitant cannot be empty");
        }
        Inhabitants.Add(inhabitant);
    }

    public void ShowAll()
    {
        Console.WriteLine("Oceanarium " + Name + ":");
        if (Inhabitants.Count == 0)
        {
            Console.WriteLine("No inhabitants");
            return;
        }
        foreach (SeaItem inhabitant in Inhabitants)
        {
            inhabitant.ShowInfo();
        }
    }
}

class Program
{
    static void Main()
    {
        try
        {
            Oceanarium oceanarium = new Oceanarium("Blue World");
            oceanarium.AddInhabitant(new Shark("Bruce", 12, 300));
            oceanarium.AddInhabitant(new Dolphin("Flipper", 7, 15));
            oceanarium.AddInhabitant(new Octopus("Paul", 2, "red"));
            oceanarium.ShowAll();
        }
        catch (Exception error)
        {
            Console.WriteLine(error.Message);
        }
    }
}
