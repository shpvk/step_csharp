using System.Collections;

class Car
{
    public string Brand { get; private set; }
    public string Model { get; private set; }
    public int Year { get; private set; }
    public string PlateNumber { get; private set; }

    public Car(string brand, string model, int year, string plateNumber)
    {
        if (brand == null || model == null || plateNumber == null)
        {
            throw new Exception("Car data cannot be empty");
        }
        if (year < 1886 || year > DateTime.Now.Year)
        {
            throw new Exception("Wrong year");
        }
        Brand = brand;
        Model = model;
        Year = year;
        PlateNumber = plateNumber;
    }

    public override string ToString()
    {
        return Brand + " " + Model + ", " + Year + ", " + PlateNumber;
    }
}

class Garage : IEnumerable
{
    private Car[] cars = new Car[0];

    public void AddCar(Car car)
    {
        if (car == null)
        {
            throw new Exception("Car cannot be empty");
        }
        Car[] newCars = new Car[cars.Length + 1];
        for (int i = 0; i < cars.Length; i++)
        {
            newCars[i] = cars[i];
        }
        newCars[cars.Length] = car;
        cars = newCars;
    }

    public IEnumerator GetEnumerator()
    {
        return new GarageEnumerator(cars);
    }
}

class GarageEnumerator : IEnumerator
{
    private Car[] cars;
    private int position = -1;

    public GarageEnumerator(Car[] cars)
    {
        this.cars = cars;
    }

    public bool MoveNext()
    {
        position++;
        return position < cars.Length;
    }

    public void Reset()
    {
        position = -1;
    }

    public object Current
    {
        get
        {
            return cars[position];
        }
    }
}

class Program
{
    static void Main()
    {
        try
        {
            Garage garage = new Garage();
            garage.AddCar(new Car("Toyota", "Camry", 2018, "AA1234BB"));
            garage.AddCar(new Car("BMW", "X5", 2021, "KA5678CE"));
            garage.AddCar(new Car("Ford", "Focus", 2012, "BC9012AI"));

            Console.WriteLine("Cars in garage:");
            foreach (Car car in garage)
            {
                Console.WriteLine(car);
            }
        }
        catch (Exception error)
        {
            Console.WriteLine(error.Message);
        }
    }
}
