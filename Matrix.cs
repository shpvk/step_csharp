Matrix first = new Matrix(new double[][] { new double[] { 1, 2 }, new double[] { 3, 4 } });
Matrix second = new Matrix(new double[][] { new double[] { 5, 6 }, new double[] { 7, 8 } });

Console.WriteLine("First matrix:");
first.Print();
Console.WriteLine("Second matrix:");
second.Print();
Console.WriteLine("Sum:");
(first + second).Print();
Console.WriteLine("Difference:");
(first - second).Print();
Console.WriteLine("Product:");
(first * second).Print();
Console.WriteLine("Multiply by 3:");
(first * 3).Print();
Console.WriteLine("3 * matrix:");
(3 * first).Print();
Console.WriteLine("== : " + (first == second));
Console.WriteLine("!= : " + (first != second));
Console.WriteLine("Equals: " + first.Equals(second));
first[0, 1] = 10;
Console.WriteLine("Element [0, 1]: " + first[0, 1]);

class Matrix
{
    private double[][] values;

    public int Rows
    {
        get
        {
            return values.Length;
        }
    }

    public int Columns
    {
        get
        {
            return values[0].Length;
        }
    }

    public Matrix(int rows, int columns)
    {
        if (rows <= 0 || columns <= 0)
        {
            throw new Exception("Matrix dimensions must be positive");
        }
        values = new double[rows][];
        for (int row = 0; row < rows; row++)
        {
            values[row] = new double[columns];
        }
    }

    public Matrix(double[][] source)
    {
        if (source == null || source.Length == 0 || source[0] == null || source[0].Length == 0)
        {
            throw new Exception("Matrix cannot be empty");
        }
        values = new double[source.Length][];
        for (int row = 0; row < Rows; row++)
        {
            if (source[row] == null || source[row].Length != source[0].Length)
            {
                throw new Exception("Matrix rows must have the same length");
            }
            values[row] = new double[source[0].Length];
            for (int column = 0; column < Columns; column++)
            {
                values[row][column] = source[row][column];
            }
        }
    }

    public double this[int row, int column]
    {
        get
        {
            CheckIndex(row, column);
            return values[row][column];
        }
        set
        {
            CheckIndex(row, column);
            values[row][column] = value;
        }
    }

    private void CheckIndex(int row, int column)
    {
        if (row < 0 || row >= Rows || column < 0 || column >= Columns)
        {
            throw new Exception("Matrix index is out of range");
        }
    }

    public void Input()
    {
        for (int row = 0; row < Rows; row++)
        {
            for (int column = 0; column < Columns; column++)
            {
                Console.Write("Element [" + row + ", " + column + "]: ");
                this[row, column] = double.Parse(Console.ReadLine()!);
            }
        }
    }

    public void Print()
    {
        for (int row = 0; row < Rows; row++)
        {
            for (int column = 0; column < Columns; column++)
            {
                Console.Write(this[row, column] + " ");
            }
            Console.WriteLine();
        }
    }

    public static Matrix operator +(Matrix left, Matrix right)
    {
        if (left.Rows != right.Rows || left.Columns != right.Columns)
        {
            throw new Exception("Matrix dimensions must match");
        }
        Matrix result = new Matrix(left.Rows, left.Columns);
        for (int row = 0; row < left.Rows; row++)
        {
            for (int column = 0; column < left.Columns; column++)
            {
                result[row, column] = left[row, column] + right[row, column];
            }
        }
        return result;
    }

    public static Matrix operator -(Matrix left, Matrix right)
    {
        if (left.Rows != right.Rows || left.Columns != right.Columns)
        {
            throw new Exception("Matrix dimensions must match");
        }
        Matrix result = new Matrix(left.Rows, left.Columns);
        for (int row = 0; row < left.Rows; row++)
        {
            for (int column = 0; column < left.Columns; column++)
            {
                result[row, column] = left[row, column] - right[row, column];
            }
        }
        return result;
    }

    public static Matrix operator *(Matrix left, Matrix right)
    {
        if (left.Columns != right.Rows)
        {
            throw new Exception("Left columns must match right rows");
        }
        Matrix result = new Matrix(left.Rows, right.Columns);
        for (int row = 0; row < left.Rows; row++)
        {
            for (int column = 0; column < right.Columns; column++)
            {
                for (int index = 0; index < left.Columns; index++)
                {
                    result[row, column] += left[row, index] * right[index, column];
                }
            }
        }
        return result;
    }

    public static Matrix operator *(Matrix matrix, double number)
    {
        Matrix result = new Matrix(matrix.Rows, matrix.Columns);
        for (int row = 0; row < matrix.Rows; row++)
        {
            for (int column = 0; column < matrix.Columns; column++)
            {
                result[row, column] = matrix[row, column] * number;
            }
        }
        return result;
    }

    public static Matrix operator *(double number, Matrix matrix)
    {
        return matrix * number;
    }

    public static bool operator ==(Matrix? left, Matrix? right)
    {
        if (left is null || right is null)
        {
            return left is null && right is null;
        }
        if (left.Rows != right.Rows || left.Columns != right.Columns)
        {
            return false;
        }
        for (int row = 0; row < left.Rows; row++)
        {
            for (int column = 0; column < left.Columns; column++)
            {
                if (!left[row, column].Equals(right[row, column]))
                {
                    return false;
                }
            }
        }
        return true;
    }

    public static bool operator !=(Matrix? left, Matrix? right)
    {
        return !(left == right);
    }

    public override bool Equals(object? obj)
    {
        if (obj == null || obj.GetType() != typeof(Matrix))
        {
            return false;
        }
        Matrix other = (Matrix)obj;
        return this == other;
    }

    public override int GetHashCode()
    {
        HashCode hash = new HashCode();
        hash.Add(Rows);
        hash.Add(Columns);
        for (int row = 0; row < Rows; row++)
        {
            for (int column = 0; column < Columns; column++)
            {
                hash.Add(this[row, column]);
            }
        }
        return hash.ToHashCode();
    }
}
