/// Represents a teaching staff member in a school.
public class TeachingStaff : User
{
    private double salary; // The salary of the teaching staff.
    private string subject1; // The first subject taught by the teaching staff.
    private string subject2; // The second subject taught by the teaching staff.
    /// Gets or sets the salary of the teaching staff.
    public double Salary { 
        get {return salary;}
        set {salary = value;}
    }
    /// Gets or sets the first subject taught by the teaching staff.
    public string Subject1 { 
        get {return subject1;}
        set {subject1 = value;}
    }
    /// Gets or sets the second subject taught by the teaching staff.
    public string Subject2 { 
        get {return subject2;}
        set {subject2 = value;}
    }
    /// Initializes a new instance of the TeachingStaff class with the specified parameters.
    public TeachingStaff(int id, string name, string telephone, string email, double salary, string subject1, string subject2, byte[] profilePicture)
        : base(id, name, telephone, email, "teacher", profilePicture)
    {
        Salary = salary;
        Subject1 = subject1;
        Subject2 = subject2;
    }
    /// Returns a string representation of the teaching staff.
    public override string ToString()
    {
        return $"{base.ToString()}, Salary: {Salary:C}, Subjects: {Subject1}, {Subject2}";
    }
}
