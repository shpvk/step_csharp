using System.Collections;

class Alphabet : IEnumerable
{
    private char[] letters = new char[26];

    public Alphabet()
    {
        for (int i = 0; i < letters.Length; i++)
        {
            letters[i] = (char)('A' + i);
        }
    }

    public IEnumerator GetEnumerator()
    {
        return new AlphabetEnumerator(letters);
    }
}

class AlphabetEnumerator : IEnumerator
{
    private char[] letters;
    private int position = -1;

    public AlphabetEnumerator(char[] letters)
    {
        this.letters = letters;
    }

    public bool MoveNext()
    {
        position++;
        return position < letters.Length;
    }

    public void Reset()
    {
        position = -1;
    }

    public object Current
    {
        get
        {
            return letters[position];
        }
    }
}

class Program
{
    static void Main()
    {
        Alphabet alphabet = new Alphabet();
        foreach (char letter in alphabet)
        {
            Console.Write(letter + " ");
        }
        Console.WriteLine();
    }
}
