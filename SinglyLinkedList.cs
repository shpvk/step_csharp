class ListNode<T>
{
    public T Value { get; set; }
    public ListNode<T>? Next { get; set; }

    public ListNode(T value)
    {
        Value = value;
    }
}

class SinglyLinkedList<T>
{
    private ListNode<T>? head;
    private int count = 0;

    public int Count
    {
        get
        {
            return count;
        }
    }

    public bool IsEmpty
    {
        get
        {
            return count == 0;
        }
    }

    public void AddFirst(T value)
    {
        ListNode<T> node = new ListNode<T>(value);
        node.Next = head;
        head = node;
        count++;
    }

    public void AddLast(T value)
    {
        if (head == null)
        {
            AddFirst(value);
            return;
        }
        ListNode<T> current = head;
        while (current.Next != null)
        {
            current = current.Next;
        }
        current.Next = new ListNode<T>(value);
        count++;
    }

    public void InsertAt(int index, T value)
    {
        if (index < 0 || index > count)
        {
            throw new Exception("Index out of range");
        }
        if (index == 0)
        {
            AddFirst(value);
            return;
        }
        ListNode<T> previous = GetNode(index - 1);
        ListNode<T> node = new ListNode<T>(value);
        node.Next = previous.Next;
        previous.Next = node;
        count++;
    }

    public T RemoveFirst()
    {
        if (head == null)
        {
            throw new Exception("List is empty");
        }
        T value = head.Value;
        head = head.Next;
        count--;
        return value;
    }

    public T RemoveLast()
    {
        return RemoveAt(count - 1);
    }

    public T RemoveAt(int index)
    {
        if (index < 0 || index >= count)
        {
            throw new Exception("Index out of range");
        }
        if (index == 0)
        {
            return RemoveFirst();
        }
        ListNode<T> previous = GetNode(index - 1);
        ListNode<T> removed = previous.Next!;
        previous.Next = removed.Next;
        count--;
        return removed.Value;
    }

    public T Get(int index)
    {
        if (index < 0 || index >= count)
        {
            throw new Exception("Index out of range");
        }
        return GetNode(index).Value;
    }

    public int IndexOf(T value)
    {
        ListNode<T>? current = head;
        int index = 0;
        while (current != null)
        {
            if (Equals(current.Value, value))
            {
                return index;
            }
            current = current.Next;
            index++;
        }
        return -1;
    }

    public void Clear()
    {
        head = null;
        count = 0;
    }

    public void Show()
    {
        if (head == null)
        {
            Console.WriteLine("List is empty");
            return;
        }
        ListNode<T>? current = head;
        while (current != null)
        {
            Console.Write(current.Value);
            if (current.Next != null)
            {
                Console.Write(" -> ");
            }
            current = current.Next;
        }
        Console.WriteLine();
    }

    private ListNode<T> GetNode(int index)
    {
        ListNode<T> current = head!;
        for (int i = 0; i < index; i++)
        {
            current = current.Next!;
        }
        return current;
    }
}

class Program
{
    static void Main()
    {
        try
        {
            SinglyLinkedList<int> list = new SinglyLinkedList<int>();
            list.AddLast(2);
            list.AddLast(3);
            list.AddFirst(1);
            list.InsertAt(3, 5);
            list.InsertAt(3, 4);
            list.Show();
            Console.WriteLine("Count: " + list.Count);
            Console.WriteLine("Element at index 2: " + list.Get(2));
            Console.WriteLine("Index of 4: " + list.IndexOf(4));
            Console.WriteLine("Index of 10: " + list.IndexOf(10));

            Console.WriteLine("RemoveFirst: " + list.RemoveFirst());
            Console.WriteLine("RemoveLast: " + list.RemoveLast());
            Console.WriteLine("RemoveAt 1: " + list.RemoveAt(1));
            list.Show();

            list.Clear();
            list.Show();
            list.RemoveFirst();
        }
        catch (Exception error)
        {
            Console.WriteLine(error.Message);
        }
    }
}
