class CircularQueue<T>
{
    private T[] buffer;
    private int head = 0;
    private int tail = 0;
    private int count = 0;

    public CircularQueue(int capacity)
    {
        if (capacity <= 0)
        {
            throw new Exception("Capacity must be positive");
        }
        buffer = new T[capacity];
    }

    public int Count
    {
        get
        {
            return count;
        }
    }

    public int Capacity
    {
        get
        {
            return buffer.Length;
        }
    }

    public bool IsEmpty
    {
        get
        {
            return count == 0;
        }
    }

    public bool IsFull
    {
        get
        {
            return count == buffer.Length;
        }
    }

    public void Enqueue(T item)
    {
        if (IsFull)
        {
            throw new Exception("Queue is full");
        }
        buffer[tail] = item;
        tail = (tail + 1) % buffer.Length;
        count++;
    }

    public T Dequeue()
    {
        if (IsEmpty)
        {
            throw new Exception("Queue is empty");
        }
        T item = buffer[head];
        buffer[head] = default;
        head = (head + 1) % buffer.Length;
        count--;
        return item;
    }

    public T Peek()
    {
        if (IsEmpty)
        {
            throw new Exception("Queue is empty");
        }
        return buffer[head];
    }

    public void Clear()
    {
        buffer = new T[buffer.Length];
        head = 0;
        tail = 0;
        count = 0;
    }

    public void Show()
    {
        if (IsEmpty)
        {
            Console.WriteLine("Queue is empty");
            return;
        }
        for (int i = 0; i < count; i++)
        {
            Console.Write(buffer[(head + i) % buffer.Length] + " ");
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
            CircularQueue<int> queue = new CircularQueue<int>(3);
            queue.Enqueue(1);
            queue.Enqueue(2);
            queue.Enqueue(3);
            queue.Show();
            Console.WriteLine("Is full: " + queue.IsFull);

            Console.WriteLine("Dequeue: " + queue.Dequeue());
            Console.WriteLine("Dequeue: " + queue.Dequeue());
            queue.Enqueue(4);
            queue.Enqueue(5);
            queue.Show();
            Console.WriteLine("Peek: " + queue.Peek());
            Console.WriteLine("Count: " + queue.Count + " of " + queue.Capacity);

            queue.Enqueue(6);
        }
        catch (Exception error)
        {
            Console.WriteLine(error.Message);
        }
    }
}
