class CreditCard
{
    public string Number { get; private set; }
    public string OwnerName { get; private set; }
    public string ExpiryDate { get; private set; }
    public decimal CreditLimit { get; private set; }
    public decimal Balance { get; private set; }
    private string pin;

    public event Action<decimal> Deposited;
    public event Action<decimal> Spent;
    public event Action CreditStarted;
    public event Action LimitReached;
    public event Action PinChanged;

    public CreditCard(string number, string ownerName, string expiryDate, string pin, decimal creditLimit, decimal balance)
    {
        if (number == null || ownerName == null || expiryDate == null)
        {
            throw new Exception("Card data cannot be empty");
        }
        if (pin == null || pin.Length != 4)
        {
            throw new Exception("PIN must have 4 digits");
        }
        if (creditLimit < 0 || balance < 0)
        {
            throw new Exception("Credit limit and balance cannot be negative");
        }
        Number = number;
        OwnerName = ownerName;
        ExpiryDate = expiryDate;
        this.pin = pin;
        CreditLimit = creditLimit;
        Balance = balance;
    }

    public void Deposit(decimal amount)
    {
        if (amount <= 0)
        {
            throw new Exception("Amount must be positive");
        }
        Balance += amount;
        Deposited?.Invoke(amount);
    }

    public void Spend(decimal amount)
    {
        if (amount <= 0)
        {
            throw new Exception("Amount must be positive");
        }
        if (amount > Balance + CreditLimit)
        {
            throw new Exception("Not enough money");
        }
        bool wasOwnMoney = Balance >= 0;
        Balance -= amount;
        Spent?.Invoke(amount);
        if (wasOwnMoney && Balance < 0)
        {
            CreditStarted?.Invoke();
        }
        if (Balance == -CreditLimit)
        {
            LimitReached?.Invoke();
        }
    }

    public void ChangePin(string oldPin, string newPin)
    {
        if (oldPin != pin)
        {
            throw new Exception("Wrong PIN");
        }
        if (newPin == null || newPin.Length != 4)
        {
            throw new Exception("PIN must have 4 digits");
        }
        pin = newPin;
        PinChanged?.Invoke();
    }

    public void ShowInfo()
    {
        Console.WriteLine("Number: " + Number);
        Console.WriteLine("Owner: " + OwnerName);
        Console.WriteLine("Expiry date: " + ExpiryDate);
        Console.WriteLine("Credit limit: " + CreditLimit);
        Console.WriteLine("Balance: " + Balance);
    }
}

class Program
{
    static void Main()
    {
        CreditCard card = new CreditCard("4441 1144 0000 1234", "John Smith", "12/28", "1234", 1000, 500);
        card.Deposited += amount => Console.WriteLine("Event: deposited " + amount);
        card.Spent += amount => Console.WriteLine("Event: spent " + amount);
        card.CreditStarted += () => Console.WriteLine("Event: started using credit money");
        card.LimitReached += () => Console.WriteLine("Event: credit limit reached");
        card.PinChanged += () => Console.WriteLine("Event: PIN changed");

        int choice = 0;
        while (choice != 5)
        {
            Console.Write("\n1 Deposit\n2 Spend\n3 Change PIN\n4 Show info\n5 Exit\nYour choice: ");
            try
            {
                choice = int.Parse(Console.ReadLine());
                if (choice == 1)
                {
                    Console.Write("Enter amount: ");
                    card.Deposit(decimal.Parse(Console.ReadLine()));
                }
                else if (choice == 2)
                {
                    Console.Write("Enter amount: ");
                    card.Spend(decimal.Parse(Console.ReadLine()));
                }
                else if (choice == 3)
                {
                    Console.Write("Enter old PIN: ");
                    string oldPin = Console.ReadLine();
                    Console.Write("Enter new PIN: ");
                    string newPin = Console.ReadLine();
                    card.ChangePin(oldPin, newPin);
                }
                else if (choice == 4)
                {
                    card.ShowInfo();
                }
            }
            catch (Exception error)
            {
                Console.WriteLine(error.Message);
            }
        }
    }
}
