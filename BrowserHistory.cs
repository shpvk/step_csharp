class BrowserHistory
{
    private LinkedList<string> pages = new LinkedList<string>();
    private LinkedListNode<string>? currentPage;

    public BrowserHistory(string homePage)
    {
        Visits(homePage);
    }

    public void Visits(string url)
    {
        if (url == null)
        {
            throw new Exception("Url cannot be empty");
        }
        if (currentPage == null)
        {
            currentPage = pages.AddLast(url);
            return;
        }
        while (currentPage.Next != null)
        {
            pages.Remove(currentPage.Next);
        }
        currentPage = pages.AddAfter(currentPage, url);
    }

    public string Back(int steps)
    {
        if (steps < 0)
        {
            throw new Exception("Steps cannot be negative");
        }
        for (int i = 0; i < steps && currentPage != null && currentPage.Previous != null; i++)
        {
            currentPage = currentPage.Previous;
        }
        return GetCurrentPage();
    }

    public string Forward(int steps)
    {
        if (steps < 0)
        {
            throw new Exception("Steps cannot be negative");
        }
        for (int i = 0; i < steps && currentPage != null && currentPage.Next != null; i++)
        {
            currentPage = currentPage.Next;
        }
        return GetCurrentPage();
    }

    public string GetCurrentPage()
    {
        if (currentPage == null)
        {
            return "History is empty";
        }
        return currentPage.Value;
    }

    public void ShowHistory()
    {
        if (currentPage == null)
        {
            Console.WriteLine("History is empty");
            return;
        }
        LinkedListNode<string>? page = pages.First;
        while (page != null)
        {
            if (page == currentPage)
            {
                Console.Write("[ " + page.Value + " ]");
            }
            else
            {
                Console.Write(page.Value);
            }
            if (page.Next != null)
            {
                Console.Write(" -> ");
            }
            page = page.Next;
        }
        Console.WriteLine();
    }
}

class Program
{
    static void Main()
    {
        try
        {
            BrowserHistory browser = new BrowserHistory("home.com");

            browser.Visits("google.com");
            browser.Visits("github.com");
            browser.ShowHistory();

            Console.WriteLine(browser.Back(1));
            browser.ShowHistory();

            browser.Visits("stackoverflow.com");
            browser.ShowHistory();

            Console.WriteLine(browser.Back(2));
            browser.ShowHistory();

            Console.WriteLine(browser.Forward(1));
            browser.ShowHistory();

            Console.WriteLine(browser.Back(10));
            Console.WriteLine(browser.Forward(10));
            browser.ShowHistory();
        }
        catch (Exception error)
        {
            Console.WriteLine(error.Message);
        }
    }
}
