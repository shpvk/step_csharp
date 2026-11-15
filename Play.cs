class Play : IDisposable
{
    public string Title { get; private set; }
    public string AuthorName { get; private set; }
    public string Genre { get; private set; }
    public int Year { get; private set; }
    private bool disposed = false;

    public Play(string title, string authorName, string genre, int year)
    {
        if (title == null || authorName == null || genre == null)
        {
            throw new Exception("Play data cannot be empty");
        }
        if (year > DateTime.Now.Year)
        {
            throw new Exception("Wrong year");
        }
        Title = title;
        AuthorName = authorName;
        Genre = genre;
        Year = year;
        Console.WriteLine("Play " + Title + " created");
    }

    public void ShowInfo()
    {
        if (disposed)
        {
            throw new Exception("Play is already disposed");
        }
        Console.WriteLine(Title + ", " + AuthorName + ", " + Genre + ", " + Year);
    }

    public void Dispose()
    {
        if (disposed)
        {
            return;
        }
        disposed = true;
        Console.WriteLine("Dispose: play " + Title + " released");
        GC.SuppressFinalize(this);
    }

    ~Play()
    {
        Console.WriteLine("Destructor: play " + Title + " destroyed");
    }
}

class Program
{
    static void CreatePlayWithoutDispose()
    {
        Play play = new Play("Hamlet", "William Shakespeare", "Tragedy", 1603);
        play.ShowInfo();
    }

    static void Main()
    {
        try
        {
            CreatePlayWithoutDispose();

            Console.WriteLine();
            using (Play play = new Play("The Seagull", "Anton Chekhov", "Comedy", 1896))
            {
                play.ShowInfo();
            }

            Console.WriteLine();
            Play otherPlay = new Play("Pygmalion", "George Bernard Shaw", "Comedy", 1913);
            otherPlay.ShowInfo();
            otherPlay.Dispose();
            otherPlay.ShowInfo();
        }
        catch (Exception error)
        {
            Console.WriteLine(error.Message);
        }
    }
}
