/// Represents a student in the school.
public class Students : User
{
    private string currentSubject1; // The first current subject of the student.
    private string currentSubject2; // The second current subject of the student.
    private string prevSubject1; // The first previous subject of the student.
    private string prevSubject2; // The second previous subject of the student.
    /// Gets or sets the first current subject of the student.
    public string CurrentSubject1 { 
        get {return currentSubject1;}
        set {currentSubject1 = value;}
     }
    /// Gets or sets the second current subject of the student.
    public string CurrentSubject2 { 
        get {return currentSubject2;}
        set {currentSubject2 = value;}
     }
    /// Gets or sets the first previous subject of the student.
    public string PrevSubject1 { 
        get {return prevSubject1;}
        set {prevSubject1 = value;}
     }
    /// Gets or sets the second previous subject of the student.
    public string PrevSubject2 { 
        get {return prevSubject2;}
        set {prevSubject2 = value;}
     }
    /// Initializes a new instance of the Students class with the specified parameters.
    public Students(int id, string name, string telephone, string email, string currentSubject1, string currentSubject2, string prevSubject1, string prevSubject2, byte[] profilePicture)
        : base(id, name, telephone, email, "student", profilePicture)
    {
        CurrentSubject1 = currentSubject1;
        CurrentSubject2 = currentSubject2;
        PrevSubject1 = prevSubject1;
        PrevSubject2 = prevSubject2;
    }
    /// Returns a string representation of the Students object.
    public override string ToString()
    {
        return $"{base.ToString()}, Current Subjects: {CurrentSubject1}, {CurrentSubject2}, Previous Subjects: {PrevSubject1}, {PrevSubject2}";
    }
}
