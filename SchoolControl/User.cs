/// Represents a user in the system.
public class User
{
    private int id;  // The ID of the user.
    private string name; // The name of the user.
    private string telephone;// The telephone number of the user.
    private string email;// The email address of the user.
    private string role;// The role of the user.
    private byte[] profilePicture;
    /// Gets or sets the ID of the user.
    public int ID {
        get {return id;}
        set {id = value;}
    }
    /// Gets or sets the name of the user.
    public string Name { 
        get {return name;}
        set {name = value;}
    }
    /// Gets or sets the telephone number of the user.
    public string Telephone { 
        get {return telephone;}
        set {telephone = value;}
    }
    /// Gets or sets the email address of the user.
    public string Email { 
        get {return email;}
        set {email = value;}
    }
    /// Gets or sets the role of the user.
    public string Role { 
        get {return role;}
        set {role = value;}
    }
    /// Gets or sets the profile picture of the user.
    public byte[] ProfilePicture { 
        get {return profilePicture;}
        set {profilePicture = value;}
    }
    /// Initializes a new instance of the User class.
    public User(int id, string name, string telephone, string email, string role, byte[] profilePicture)
    {
        ID = id;
        Name = name;
        Telephone = telephone;
        Email = email;
        Role = role;
        ProfilePicture = profilePicture;
    }
    /// Returns a string that represents the current user.
    public override string ToString()
    {
        return $"ID: {ID}, Name: {Name}, Telephone: {Telephone}, Email: {Email}, Role: {Role}";
    }
}
