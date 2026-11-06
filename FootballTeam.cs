class Player
{
    public string Name { get; private set; }
    public int Number { get; private set; }
    public string Position { get; private set; }

    public Player(string name, int number, string position)
    {
        if (name == null || position == null)
        {
            throw new Exception("Player data cannot be empty");
        }
        if (number < 1 || number > 99)
        {
            throw new Exception("Number must be from 1 to 99");
        }
        Name = name;
        Number = number;
        Position = position;
    }

    public void ShowInfo()
    {
        Console.WriteLine("#" + Number + " " + Name + ", " + Position);
    }
}

class FootballTeam
{
    public string Name { get; private set; }
    public List<Player> Players { get; private set; } = new List<Player>();

    public FootballTeam(string name)
    {
        if (name == null)
        {
            throw new Exception("Team name cannot be empty");
        }
        Name = name;
    }

    public void AddPlayer(Player player)
    {
        if (player == null)
        {
            throw new Exception("Player cannot be empty");
        }
        foreach (Player teamPlayer in Players)
        {
            if (teamPlayer.Number == player.Number)
            {
                throw new Exception("Number " + player.Number + " is already taken");
            }
        }
        Players.Add(player);
    }

    public void ShowAll()
    {
        Console.WriteLine("Team " + Name + ":");
        if (Players.Count == 0)
        {
            Console.WriteLine("No players");
            return;
        }
        foreach (Player player in Players)
        {
            player.ShowInfo();
        }
    }
}

class Program
{
    static void Main()
    {
        FootballTeam team = new FootballTeam("Dynamo");
        try
        {
            team.AddPlayer(new Player("John Smith", 1, "goalkeeper"));
            team.AddPlayer(new Player("Mike Brown", 4, "defender"));
            team.AddPlayer(new Player("Alex White", 10, "midfielder"));
            team.AddPlayer(new Player("Tom Green", 9, "forward"));
            team.AddPlayer(new Player("Sam Black", 10, "forward"));
        }
        catch (Exception error)
        {
            Console.WriteLine(error.Message);
        }
        team.ShowAll();
    }
}
