using System;
using System.Collections.Generic;
using System.Configuration;
using System.Xml.Linq;
using MySql.Data.MySqlClient;
using System.Linq;

public class EducationCenter
{
    private const string ConnectionString = "Server=127.0.0.1;Port=3306;Database=test;User Id=root;Password=;";

    private static List<User> users;
    private static List<TeachingStaff> teachingStaff;
    private static List<Administration> administration;
    private static List<Students> students;

    public EducationCenter()
    {
        users = new List<User>();
        teachingStaff = new List<TeachingStaff>();
        administration = new List<Administration>();
        students = new List<Students>();
        if (DatabaseManager.TestConnection())
        {
            Console.WriteLine("Connection to MySQL database successful.");
            LoadUsersFromDatabase();
        }
        else
        {
            Console.WriteLine("Connection to MySQL database failed. Check your connection settings.");
        }
    }

    public void Message()
    {

        Console.WriteLine("\nOptions:");
        Console.WriteLine("1. Add new user");
        Console.WriteLine("2. View all users");
        Console.WriteLine("3. View users by group");
        Console.WriteLine("4. Edit user");
        Console.WriteLine("5. Delete user");
        Console.WriteLine("6. Exit");

        Console.Write("Enter your choice (1-6): ");
        string choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                AddNewUser();
                break;
            case "2":
                ViewAllUsers();
                break;
            case "3":
                ViewUsersByGroup();
                break;
            case "4":
                EditUser();
                break;
            case "5":
               DeleteUser();
               break;
            case "6":
                Console.WriteLine("Exiting the program.");
                return;
            default:
                Console.WriteLine("Invalid choice. Please enter a number between 1 and 6.");
                break;
        }
        
    }

    static void AddNewUser()
    {
        Console.WriteLine("\nAdding new user:");
        Console.Write("Enter name: ");
        string name = Console.ReadLine();
        Console.Write("Enter telephone: ");
        string telephone = Console.ReadLine();
        Console.Write("Enter email: ");
        string email = Console.ReadLine();
        Console.Write("Enter user type (teacher/admin/student): ");
        string userType = Console.ReadLine().ToLower();

        User newUser = new User(users.Last().ID + 1, name, telephone, email, userType);
        DatabaseManager.InsertUserIntoDatabase(newUser);
        switch (userType)
        {
            case "teacher":
                newUser = CreateTeachingStaff(newUser);
                teachingStaff.Add(newUser as TeachingStaff);
                break;
            case "admin":
                newUser = CreateAdministration(newUser);
                administration.Add(newUser as Administration);
                break;
            case "student":
                newUser = CreateStudent(newUser);
                students.Add(newUser as Students);
                break;
            default:
                Console.WriteLine("Invalid user type.");
                return;
        }
        users.Add(newUser);
        Console.WriteLine("User added successfully.");

    }

    private static TeachingStaff CreateTeachingStaff(User user)
    {
        Console.Write("Enter subject 1: ");
        string subject1 = Console.ReadLine();
        Console.Write("Enter subject 2: ");
        string subject2 = Console.ReadLine();
        Console.Write("Enter salary: ");
        double salary = 0;
        bool isValidSalary = false;
        while (!isValidSalary)
        {
            Console.Write("Enter salary: ");
            string salaryInput = Console.ReadLine();
            
            isValidSalary = double.TryParse(salaryInput, out salary);
            
            if (!isValidSalary)
            {
                Console.WriteLine("Invalid input. Please enter a valid double for salary.");
            }
        }
        
        return new TeachingStaff(user.ID, user.Name, user.Telephone, user.Email, salary, subject1, subject2, user.ProfilePicture);
    }

    static Administration CreateAdministration(User user)
    {
        double salary = 0;
        bool isValidSalary = false;
        while (!isValidSalary)
        {
            Console.Write("Enter salary: ");
            string salaryInput = Console.ReadLine();
            
            isValidSalary = double.TryParse(salaryInput, out salary);
            
            if (!isValidSalary)
            {
                Console.WriteLine("Invalid input. Please enter a valid double for salary.");
            }
        }

        Console.Write("Enter full-time/part-time: ");
        string fullTimePartTime = Console.ReadLine();

        int workingHours = 0;
        bool isValidWorkingHours = false;        
        while (!isValidWorkingHours)
        {
            Console.Write("Enter working hours: ");
            string workingHoursInput = Console.ReadLine();
            
            isValidWorkingHours = int.TryParse(workingHoursInput, out workingHours);
            
            if (!isValidWorkingHours)
            {
                Console.WriteLine("Invalid input. Please enter a valid integer for working hours.");
            }
        }
        DatabaseManager.InsertAdminIntoDatabase(user.ID, salary, fullTimePartTime, workingHours);
        return new Administration(user.ID, user.Name, user.Telephone, user.Email, salary, fullTimePartTime, workingHours);
    }

    static Students CreateStudent(User user)
    {
        Console.Write("Enter current subject 1: ");
        string currentSubject1 = Console.ReadLine();
        Console.Write("Enter current subject 2: ");
        string currentSubject2 = Console.ReadLine();
        Console.Write("Enter previous subject 1: ");
        string prevSubject1 = Console.ReadLine();
        Console.Write("Enter previous subject 2: ");
        string prevSubject2 = Console.ReadLine();
        DatabaseManager.InsertStudentIntoDatabase(user.ID, currentSubject1, currentSubject2, prevSubject1, prevSubject2);
        return new Students(user.ID, user.Name, user.Telephone, user.Email, currentSubject1, currentSubject2, prevSubject1, prevSubject2, user.ProfilePicture);
    }

    private void LoadUsersFromDatabase()
    {
        users.Clear();
        try
        {
            using (MySqlConnection connection = new MySqlConnection(ConnectionString))
            {
                connection.Open();

                string selectUsersQuery = "SELECT * FROM Users";

                using (MySqlCommand command = new MySqlCommand(selectUsersQuery, connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = Convert.ToInt32(reader["UserID"]);
                            string name = Convert.ToString(reader["Name"]);
                            string telephone = Convert.ToString(reader["Telephone"]);
                            string email = Convert.ToString(reader["Email"]);
                            string role = Convert.ToString(reader["Role"]);
                            User user = new User(id, name, telephone, email, role);

                            if (user != null && role == "teacher")
                            {
                                TeachingStaff teacher = DatabaseManager.TeachingStaffByID(user);
                                if (teacher != null)
                                {
                                    user = teacher;
                                    teachingStaff.Add(teacher);
                                }
                            }

                            if (role == "admin")
                            {
                                Administration admin = DatabaseManager.AdminByID(user);
                                if (admin != null)
                                {
                                    user = admin;
                                    administration.Add(admin);
                                }
                            }   

                            if (role == "student")
                            {
                                Students student = DatabaseManager.StudentByID(user);
                                if (student != null)
                                {
                                    user = student;
                                    students.Add(student);
                                }
                            }   
                            users.Add(user);
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading users from database: {ex.Message}");
        }
    }

    public void ViewAllUsers()
    {
        for (int i = 0; i<users.Count(); i++)
        {
            Console.WriteLine(users[i]);
        }
    }

    public void ViewUsersByGroup()
    {
        Console.Write("\nEnter user group (teacher/admin/student): ");
        string role = Console.ReadLine().ToLower();
        foreach (User user in users)
        {
            if (user.Role == role)
            {
                Console.WriteLine(user);
            }
        }
    }

    public void EditUser()
    {
        Console.Write("\nEnter the ID of the user to edit: ");
        int ID = Convert.ToInt16(Console.ReadLine()); // clear input buffer

        Console.Write("Enter name: ");
        string name = Console.ReadLine();
        Console.Write("Enter telephone: ");
        string telephone = Console.ReadLine();
        Console.Write("Enter email: ");
        string email = Console.ReadLine();

        DatabaseManager.UpdateUserInDatabase(ID, name, telephone, email);
        var userToEdit = users.Find(user => user.ID == ID);
        if (userToEdit != null)
        {
            userToEdit.Name = name;
            userToEdit.Telephone = telephone;
            userToEdit.Email = email;
            if (userToEdit.Role == "teacher")
            {
                EditTeacher(userToEdit.ID);
            }
            if (userToEdit.Role == "admin")
            {
                EditAdmin(userToEdit.ID);
            }
            if (userToEdit.Role == "student")
            {
                EditStudent(userToEdit.ID);
            }
            // Console.WriteLine("User edited successfully.");
        }
        else
        {
            Console.WriteLine("User not found.");
        }
    }

    private void EditTeacher(int id)
    {
        Console.Write("Enter subject 1: ");
        string subject1 = Console.ReadLine();
        Console.Write("Enter subject 2: ");
        string subject2 = Console.ReadLine();
        Console.Write("Enter salary:");
        double salary = Convert.ToDouble(Console.ReadLine());
        var userToEdit = teachingStaff.Find(user => user.ID == id); // Replace 'id' with 'userId'
        if (userToEdit != null)
        {
            userToEdit.Subject1 = subject1;
            userToEdit.Subject2 = subject2;
            userToEdit.Salary = salary;
            Console.WriteLine("User edited successfully.");
        }
        else
        {
            Console.WriteLine("User not found.");
        }
        DatabaseManager.UpdateTeacherInDatabase(id, salary, subject1, subject2); // Replace 'id' with 'userId'
    }

    private void EditAdmin(int id)
    {
        Console.Write("Enter salary: ");
        double salary = Convert.ToDouble(Console.ReadLine());
        Console.Write("Enter full-time/part-time: ");
        string fullTimePartTime = Console.ReadLine();
        Console.Write("Enter working hours: ");
        int workingHours = Convert.ToInt32(Console.ReadLine());
        var userToEdit = administration.Find(user => user.ID == id); // Replace 'id' with 'userId'
        if (userToEdit != null)
        {
            userToEdit.Salary = salary;
            userToEdit.FullTimePartTime = fullTimePartTime;
            userToEdit.WorkingHours = workingHours;
            Console.WriteLine("User edited successfully.");
        }
        else
        {
            Console.WriteLine("User not found.");
        }
        DatabaseManager.UpdateAdminInDatabase(id, salary, fullTimePartTime, workingHours); // Replace 'id' with 'userId'
    }   

    private void EditStudent(int id)
    {
        Console.Write("Enter current subject 1: ");
        string currentSubject1 = Console.ReadLine();
        Console.Write("Enter current subject 2: ");
        string currentSubject2 = Console.ReadLine();
        Console.Write("Enter previous subject 1: ");
        string prevSubject1 = Console.ReadLine();
        Console.Write("Enter previous subject 2: ");
        string prevSubject2 = Console.ReadLine();
        var userToEdit = students.Find(user => user.ID == id); // Replace 'id' with 'userId'
        if (userToEdit != null)
        {
            userToEdit.CurrentSubject1 = currentSubject1;
            userToEdit.CurrentSubject2 = currentSubject2;
            userToEdit.PrevSubject1 = prevSubject1;
            userToEdit.PrevSubject2 = prevSubject2;
            Console.WriteLine("User edited successfully.");
        }
        else
        {
            Console.WriteLine("User not found.");
        }
        DatabaseManager.UpdateStudentInDatabase(id, currentSubject1, currentSubject2, prevSubject1, prevSubject2); // Replace 'id' with 'userId'
    }
    
    public void DeleteUser()
    {
        Console.Write("Enter user ID to delete: ");
        int userId = Convert.ToInt32(Console.ReadLine());
        DatabaseManager.DeleteUserFromDatabase(userId);
        var userToDelete = users.Find(user => user.ID == userId);
        if (userToDelete != null)
        {
            users.Remove(userToDelete);
            if (userToDelete.Role == "teacher")
            {
                var teacherToDelete = teachingStaff.Find(user => user.ID == userId);
                teachingStaff.Remove(teacherToDelete);
            }
            if (userToDelete.Role == "admin")
            {
                var adminToDelete = administration.Find(user => user.ID == userId);
                administration.Remove(adminToDelete);
            }
            if (userToDelete.Role == "student")
            {
                var studentToDelete = students.Find(user => user.ID == userId);
                students.Remove(studentToDelete);
            }
            Console.WriteLine("User deleted successfully.");
        }
        else
        {
            Console.WriteLine("User not found.");
        }
    }

}
