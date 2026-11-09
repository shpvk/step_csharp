using System.Collections;

class Apartment : IEnumerable
{
    public int Number { get; private set; }
    private string[] residents = new string[0];

    public Apartment(int number)
    {
        if (number <= 0)
        {
            throw new Exception("Apartment number must be positive");
        }
        Number = number;
    }

    public void AddResident(string name)
    {
        if (name == null)
        {
            throw new Exception("Resident name cannot be empty");
        }
        string[] newResidents = new string[residents.Length + 1];
        for (int i = 0; i < residents.Length; i++)
        {
            newResidents[i] = residents[i];
        }
        newResidents[residents.Length] = name;
        residents = newResidents;
    }

    public IEnumerator GetEnumerator()
    {
        return new ApartmentEnumerator(residents);
    }
}

class ApartmentEnumerator : IEnumerator
{
    private string[] residents;
    private int position = -1;

    public ApartmentEnumerator(string[] residents)
    {
        this.residents = residents;
    }

    public bool MoveNext()
    {
        position++;
        return position < residents.Length;
    }

    public void Reset()
    {
        position = -1;
    }

    public object Current
    {
        get
        {
            return residents[position];
        }
    }
}

class House : IEnumerable
{
    public string Address { get; private set; }
    private Apartment[] apartments = new Apartment[0];

    public House(string address)
    {
        if (address == null)
        {
            throw new Exception("Address cannot be empty");
        }
        Address = address;
    }

    public void AddApartment(Apartment apartment)
    {
        if (apartment == null)
        {
            throw new Exception("Apartment cannot be empty");
        }
        Apartment[] newApartments = new Apartment[apartments.Length + 1];
        for (int i = 0; i < apartments.Length; i++)
        {
            newApartments[i] = apartments[i];
        }
        newApartments[apartments.Length] = apartment;
        apartments = newApartments;
    }

    public IEnumerator GetEnumerator()
    {
        return new HouseEnumerator(apartments);
    }
}

class HouseEnumerator : IEnumerator
{
    private Apartment[] apartments;
    private int position = -1;

    public HouseEnumerator(Apartment[] apartments)
    {
        this.apartments = apartments;
    }

    public bool MoveNext()
    {
        position++;
        return position < apartments.Length;
    }

    public void Reset()
    {
        position = -1;
    }

    public object Current
    {
        get
        {
            return apartments[position];
        }
    }
}

class Program
{
    static void Main()
    {
        try
        {
            Apartment first = new Apartment(1);
            first.AddResident("John Smith");
            first.AddResident("Anna Smith");

            Apartment second = new Apartment(2);
            second.AddResident("Mike Brown");

            Apartment third = new Apartment(3);

            House house = new House("Main Street 5");
            house.AddApartment(first);
            house.AddApartment(second);
            house.AddApartment(third);

            Console.WriteLine("House " + house.Address + ":");
            foreach (Apartment apartment in house)
            {
                Console.WriteLine("Apartment " + apartment.Number + ":");
                foreach (string resident in apartment)
                {
                    Console.WriteLine("  " + resident);
                }
            }
        }
        catch (Exception error)
        {
            Console.WriteLine(error.Message);
        }
    }
}
