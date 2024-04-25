/// Represents an administration user in the school control system.
public class Administration : User
{
    private double salary; // The salary of the administration user.
    private string fullTimePartTime; // The full-time or part-time status of the administration user.
    private int workingHours; // The working hours per week of the administration user.

    /// Gets or sets the salary of the administration user.
    public double Salary 
    { 
        get {return salary;}
        set {salary = value;}
    }
    /// Gets or sets the full-time or part-time status of the administration user.
    public string FullTimePartTime 
    { 
        get {return fullTimePartTime;}
        set {fullTimePartTime = value;}
    }
    /// Gets or sets the working hours per week of the administration user.
    public int WorkingHours 
    {
        get {return workingHours;}
        set {workingHours = value;}
    }
    /// Initializes a new instance of the Administration class with the specified parameters.
    public Administration(int id, string name, string telephone, string email, double salary, string fullTimePartTime, int workingHours, byte[] profilePicture)
        : base(id, name, telephone, email, "admin", profilePicture)
    {
        Salary = salary;
        FullTimePartTime = fullTimePartTime;
        WorkingHours = workingHours;
    }
    /// Returns a string representation of the administration user.
    public override string ToString()
    {
        return $"{base.ToString()}, Salary: {Salary:C}, Full-Time/Part-Time: {FullTimePartTime}, Working Hours: {WorkingHours} hours/week";
    }
}
