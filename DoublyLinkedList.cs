class DoublyListNode<T>
{
    public T Value { get; set; }
    public DoublyListNode<T>? Previous { get; set; }
    public DoublyListNode<T>? Next { get; set; }

    public DoublyListNode(T value)
    {
        Value = value;
    }
}

class DoublyLinkedList<T>
{
    private DoublyListNode<T>? head;
    private DoublyListNode<T>? tail;
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
        DoublyListNode<T> node = new DoublyListNode<T>(value);
        if (head == null)
        {
            head = node;
            tail = node;
        }
        else
        {
            node.Next = head;
            head.Previous = node;
            head = node;
        }
        count++;
    }

    public void AddLast(T value)
    {
        DoublyListNode<T> node = new DoublyListNode<T>(value);
        if (tail == null)
        {
            head = node;
            tail = node;
        }
        else
        {
            node.Previous = tail;
            tail.Next = node;
            tail = node;
        }
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
        if (index == count)
        {
            AddLast(value);
            return;
        }
        DoublyListNode<T> next = GetNode(index);
        DoublyListNode<T> previous = next.Previous!;
        DoublyListNode<T> node = new DoublyListNode<T>(value);
        node.Previous = previous;
        node.Next = next;
        previous.Next = node;
        next.Previous = node;
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
        if (head == null)
        {
            tail = null;
        }
        else
        {
            head.Previous = null;
        }
        count--;
        return value;
    }

    public T RemoveLast()
    {
        if (tail == null)
        {
            throw new Exception("List is empty");
        }
        T value = tail.Value;
        tail = tail.Previous;
        if (tail == null)
        {
            head = null;
        }
        else
        {
            tail.Next = null;
        }
        count--;
        return value;
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
        if (index == count - 1)
        {
            return RemoveLast();
        }
        DoublyListNode<T> removed = GetNode(index);
        removed.Previous!.Next = removed.Next;
        removed.Next!.Previous = removed.Previous;
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
        DoublyListNode<T>? current = head;
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
        tail = null;
        count = 0;
    }

    public void Show()
    {
        if (head == null)
        {
            Console.WriteLine("List is empty");
            return;
        }
        DoublyListNode<T>? current = head;
        while (current != null)
        {
            Console.Write(current.Value);
            if (current.Next != null)
            {
                Console.Write(" <-> ");
            }
            current = current.Next;
        }
        Console.WriteLine();
    }

    public void ShowBackward()
    {
        if (tail == null)
        {
            Console.WriteLine("List is empty");
            return;
        }
        DoublyListNode<T>? current = tail;
        while (current != null)
        {
            Console.Write(current.Value);
            if (current.Previous != null)
            {
                Console.Write(" <-> ");
            }
            current = current.Previous;
        }
        Console.WriteLine();
    }

    private DoublyListNode<T> GetNode(int index)
    {
        if (index < count / 2)
        {
            DoublyListNode<T> current = head!;
            for (int i = 0; i < index; i++)
            {
                current = current.Next!;
            }
            return current;
        }
        DoublyListNode<T> node = tail!;
        for (int i = count - 1; i > index; i--)
        {
            node = node.Previous!;
        }
        return node;
    }
}

class Program
{
    static void Main()
    {
        try
        {
            DoublyLinkedList<string> list = new DoublyLinkedList<string>();
            list.AddLast("B");
            list.AddLast("D");
            list.AddFirst("A");
            list.InsertAt(2, "C");
            list.InsertAt(4, "E");
            list.Show();
            list.ShowBackward();
            Console.WriteLine("Count: " + list.Count);
            Console.WriteLine("Element at index 3: " + list.Get(3));
            Console.WriteLine("Index of C: " + list.IndexOf("C"));

            Console.WriteLine("RemoveFirst: " + list.RemoveFirst());
            Console.WriteLine("RemoveLast: " + list.RemoveLast());
            Console.WriteLine("RemoveAt 1: " + list.RemoveAt(1));
            list.Show();
            list.ShowBackward();

            list.Clear();
            list.Show();
            list.RemoveLast();
        }
        catch (Exception error)
        {
            Console.WriteLine(error.Message);
        }
    }
}
