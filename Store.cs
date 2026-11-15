enum StoreType
{
    Grocery,
    Household,
    Clothing,
    Shoes
}

class Store : IDisposable
{
    public string Name { get; private set; }
    public string Address { get; private set; }
    public StoreType Type { get; private set; }
    private bool disposed = false;

    public Store(string name, string address, StoreType type)
    {
        if (name == null || address == null)
        {
            throw new Exception("Store data cannot be empty");
        }
        Name = name;
        Address = address;
        Type = type;
        Console.WriteLine("Store " + Name + " opened");
    }

    public void ShowInfo()
    {
        if (disposed)
        {
            throw new Exception("Store is already disposed");
        }
        Console.WriteLine(Name + ", " + Address + ", " + Type);
    }

    public void Dispose()
    {
        if (disposed)
        {
            return;
        }
        disposed = true;
        Console.WriteLine("Dispose: store " + Name + " closed");
        GC.SuppressFinalize(this);
    }

    ~Store()
    {
        Console.WriteLine("Destructor: store " + Name + " destroyed");
    }
}

class Program
{
    static void CreateStoreWithoutDispose()
    {
        Store store = new Store("Shoe Point", "Park Avenue 3", StoreType.Shoes);
        store.ShowInfo();
    }

    static void Main()
    {
        try
        {
            Store grocery = new Store("Fresh Market", "Main Street 1", StoreType.Grocery);
            grocery.ShowInfo();
            grocery.Dispose();

            Console.WriteLine();
            using (Store clothing = new Store("Fashion House", "Central Square 7", StoreType.Clothing))
            {
                clothing.ShowInfo();
            }

            Console.WriteLine();
            CreateStoreWithoutDispose();

            Console.WriteLine();
            grocery.ShowInfo();
        }
        catch (Exception error)
        {
            Console.WriteLine(error.Message);
        }
    }
}
