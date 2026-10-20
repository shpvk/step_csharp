internal class Program
{
    static void Main()
    {
        try
        {
            Student ivan = new Student(1, "Ivan");
            Student olena = new Student(2, "Olena");
            Student petro = new Student(3, "Petro");
            Teacher mykola = new Teacher("Mykola", "Math");
            Teacher iryna = new Teacher("Iryna", "Programming");

            University university = new University("KPI", "Kyiv", "Beresteiskyi Ave 37");
            Course math = new Course("Math", "Algebra", "Geometry");
            Course csharp = new Course("C#", "Classes", "Interfaces");

            university.AddTeacher(mykola);
            university.AddTeacher(iryna);
            math.AddStudent(ivan);
            math.AddStudent(olena);
            csharp.AddStudent(olena);
            csharp.AddStudent(petro);

            university.DisplayInfo();
            Console.WriteLine("City from university: " + university.Address.City);
            university.DisplayTeachers();
            math.DisplayInfo();
            math.DisplayStudents();
            csharp.DisplayInfo();
            csharp.DisplayStudents();
            Console.WriteLine("Courses created: " + Course.Count);

            Console.WriteLine();
            Console.Write("Find by name 'Olena': ");
            math.FindStudent("Olena")?.DisplayInfo();
            Console.Write("Find by id 3: ");
            csharp.FindStudent(3)?.DisplayInfo();

            Console.WriteLine();
            try
            {
                math.AddStudent(olena);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
            }

            try
            {
                Student wrong = new Student(0, "");
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
            }

            Console.WriteLine();
            math.RemoveStudent(ivan);
            Console.WriteLine("Ivan removed from Math, but still exists:");
            ivan.DisplayInfo();

            university = null!;
            Console.WriteLine("University deleted, its address is gone with it, but teachers still exist:");
            mykola.DisplayInfo();
            iryna.DisplayInfo();
        }
        catch (Exception e)
        {
            Console.WriteLine("Unexpected error: " + e.Message);
        }
    }
}

internal class Address
{
    public string City { get; }
    public string Street { get; }

    public Address(string city, string street)
    {
        if (city == null || street == null)
        {
            throw new Exception("City and street can't be empty");
        }

        City = city;
        Street = street;
    }

    public void DisplayInfo()
    {
        Console.WriteLine("Address: " + City + ", " + Street);
    }
}

internal class Student
{
    public int Id { get; }
    public string Name { get; }

    public Student(int id, string name)
    {
        if (id <= 0)
        {
            throw new Exception("Id must be positive");
        }
        if (name == null)
        {
            throw new Exception("Name can't be empty");
        }

        Id = id;
        Name = name;
    }

    public void DisplayInfo()
    {
        Console.WriteLine("Student #" + Id + ": " + Name);
    }
}

internal class Teacher
{
    public string Name { get; }
    public string Subject { get; }

    public Teacher(string name, string subject)
    {
        if (name == null || subject == null)
        {
            throw new Exception("Name and subject can't be empty");
        }

        Name = name;
        Subject = subject;
    }

    public void DisplayInfo()
    {
        Console.WriteLine("Teacher: " + Name + " (" + Subject + ")");
    }
}

internal class University
{
    public string Name { get; }
    public Address Address { get; }
    private Teacher[] teachers = new Teacher[0];

    public University(string name, string city, string street)
    {
        if (name == null)
        {
            throw new Exception("University name can't be empty");
        }

        Name = name;
        Address = new Address(city, street);
    }

    public void AddTeacher(Teacher teacher)
    {
        if (teacher == null)
        {
            throw new Exception("Teacher can't be null");
        }

        Teacher[] newTeachers = new Teacher[teachers.Length + 1];
        for (int i = 0; i < teachers.Length; i++)
        {
            newTeachers[i] = teachers[i];
        }
        newTeachers[teachers.Length] = teacher;
        teachers = newTeachers;
    }

    public void DisplayTeachers()
    {
        Console.WriteLine("Teachers of " + Name + ":");
        foreach (Teacher teacher in teachers)
        {
            teacher.DisplayInfo();
        }
    }

    public void DisplayInfo()
    {
        Console.WriteLine("University: " + Name);
        Address.DisplayInfo();
    }
}

internal class Course
{
    public static int Count { get; private set; }

    public string Title { get; }
    private string[] topics;
    private Student[] students = new Student[0];

    public Course(string title, params string[] topics)
    {
        if (title == null)
        {
            throw new Exception("Course title can't be empty");
        }

        Title = title;
        this.topics = topics;
        Count++;
    }

    public void AddStudent(Student student)
    {
        if (student == null)
        {
            throw new Exception("Student can't be null");
        }
        if (FindStudent(student.Id) != null)
        {
            throw new Exception(student.Name + " is already on " + Title);
        }

        Student[] newStudents = new Student[students.Length + 1];
        for (int i = 0; i < students.Length; i++)
        {
            newStudents[i] = students[i];
        }
        newStudents[students.Length] = student;
        students = newStudents;
    }

    public void RemoveStudent(Student student)
    {
        if (FindStudent(student.Id) == null)
        {
            throw new Exception(student.Name + " is not on " + Title);
        }

        Student[] newStudents = new Student[students.Length - 1];
        int index = 0;
        foreach (Student current in students)
        {
            if (current.Id != student.Id)
            {
                newStudents[index] = current;
                index++;
            }
        }
        students = newStudents;
    }

    public Student? FindStudent(int id)
    {
        foreach (Student student in students)
        {
            if (student.Id == id)
            {
                return student;
            }
        }
        return null;
    }

    public Student? FindStudent(string name)
    {
        foreach (Student student in students)
        {
            if (student.Name == name)
            {
                return student;
            }
        }
        return null;
    }

    public void DisplayStudents()
    {
        Console.WriteLine("Students of " + Title + ":");
        foreach (Student student in students)
        {
            student.DisplayInfo();
        }
    }

    public void DisplayInfo()
    {
        Console.WriteLine("Course: " + Title);
        foreach (string topic in topics)
        {
            Console.WriteLine("  Topic: " + topic);
        }
    }
}
