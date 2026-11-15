class PriorityQueue<T>
{
    private T[] items = new T[0];
    private int[] priorities = new int[0];

    public int Count
    {
        get
        {
            return items.Length;
        }
    }

    public bool IsEmpty
    {
        get
        {
            return items.Length == 0;
        }
    }

    public void Enqueue(T item, int priority)
    {
        T[] newItems = new T[items.Length + 1];
        int[] newPriorities = new int[priorities.Length + 1];
        for (int i = 0; i < items.Length; i++)
        {
            newItems[i] = items[i];
            newPriorities[i] = priorities[i];
        }
        newItems[items.Length] = item;
        newPriorities[priorities.Length] = priority;
        items = newItems;
        priorities = newPriorities;
    }

    public T Dequeue()
    {
        int index = FindHighestPriorityIndex();
        T item = items[index];
        T[] newItems = new T[items.Length - 1];
        int[] newPriorities = new int[priorities.Length - 1];
        int newIndex = 0;
        for (int i = 0; i < items.Length; i++)
        {
            if (i != index)
            {
                newItems[newIndex] = items[i];
                newPriorities[newIndex] = priorities[i];
                newIndex++;
            }
        }
        items = newItems;
        priorities = newPriorities;
        return item;
    }

    public T Peek()
    {
        return items[FindHighestPriorityIndex()];
    }

    public void Clear()
    {
        items = new T[0];
        priorities = new int[0];
    }

    public void Show()
    {
        if (IsEmpty)
        {
            Console.WriteLine("Queue is empty");
            return;
        }
        for (int i = 0; i < items.Length; i++)
        {
            Console.Write(items[i] + "(" + priorities[i] + ") ");
        }
        Console.WriteLine();
    }

    private int FindHighestPriorityIndex()
    {
        if (IsEmpty)
        {
            throw new Exception("Queue is empty");
        }
        int highestIndex = 0;
        for (int i = 1; i < priorities.Length; i++)
        {
            if (priorities[i] > priorities[highestIndex])
            {
                highestIndex = i;
            }
        }
        return highestIndex;
    }
}

class Program
{
    static void Main()
    {
        try
        {
            PriorityQueue<string> queue = new PriorityQueue<string>();
            queue.Enqueue("Low task", 1);
            queue.Enqueue("Urgent task", 5);
            queue.Enqueue("Normal task", 3);
            queue.Enqueue("Second urgent task", 5);
            queue.Show();
            Console.WriteLine("Count: " + queue.Count);
            Console.WriteLine("Peek: " + queue.Peek());

            while (!queue.IsEmpty)
            {
                Console.WriteLine("Dequeue: " + queue.Dequeue());
            }

            queue.Dequeue();
        }
        catch (Exception error)
        {
            Console.WriteLine(error.Message);
        }
    }
}
