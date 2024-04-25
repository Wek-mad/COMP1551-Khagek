using System;
using System.Collections.Generic;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using SchoolControl;
using System.Drawing;
using System.IO;
using ZstdSharp.Unsafe;
using System.Runtime.CompilerServices;

public class DatabaseManager
{
    public static string ConnectionString = "Server=localhost; Database = test; User Id = root; Password =";
    private static byte[] _data = null;
    public static void InitializeDatabase()
    {
        string defaultImagePath = "../../default.png";
        _data = File.ReadAllBytes(defaultImagePath);
        using (MySqlConnection connection = new MySqlConnection(ConnectionString))
        {
            connection.Open();
            // Create tables if not exist in the database 
            // if the table already exists, the query will not create a new table
            string createTableQuery = @"
                CREATE TABLE IF NOT EXISTS Users (
                    UserID INT PRIMARY KEY AUTO_INCREMENT,
                    Name VARCHAR(255),
                    Telephone VARCHAR(15),
                    Email VARCHAR(255),
                    Role ENUM('teacher', 'admin', 'student'), 
                    ProfilePicture LONGBLOB
                );
            ";
            using (MySqlCommand command = new MySqlCommand(createTableQuery, connection))
            {
                command.ExecuteNonQuery();
            }
            // Create the teachingStaff table
            createTableQuery = @"
                CREATE TABLE IF NOT EXISTS teachingStaff (
                    ID INT PRIMARY KEY AUTO_INCREMENT,
                    UserID INT NOT NULL,
                    Salary DECIMAL(10, 2),
                    subject1 VARCHAR(100) DEFAULT NULL,
                    subject2 VARCHAR(100) DEFAULT NULL,
                    FOREIGN KEY (UserID) REFERENCES Users(UserID) ON DELETE CASCADE
                );
            ";
            using (MySqlCommand command = new MySqlCommand(createTableQuery, connection))
            {
                command.ExecuteNonQuery();
            }
            // Create the administration table
            createTableQuery = @"
                CREATE TABLE IF NOT EXISTS Administration (
                    ID INT PRIMARY KEY AUTO_INCREMENT,
                    UserID INT NOT NULL,
                    Salary DECIMAL(10, 2),
                    EmploymentType ENUM('full-time', 'part-time'),
                    WorkingHours INT,
                    FOREIGN KEY (UserID) REFERENCES Users(UserID) ON DELETE CASCADE
                );
            ";
            using (MySqlCommand command = new MySqlCommand(createTableQuery, connection))
            {
                command.ExecuteNonQuery();
            }
            // Create the students table
            createTableQuery = @"
                CREATE TABLE IF NOT EXISTS Students (
                    ID INT PRIMARY KEY AUTO_INCREMENT, 
                    UserID INT NOT NULL,
                    CurrentSubject1 VARCHAR(255),
                    CurrentSubject2 VARCHAR(255),
                    PreviousSubject1 VARCHAR(255),
                    PreviousSubject2 VARCHAR(255),
                    FOREIGN KEY (UserID) REFERENCES Users(UserID) ON DELETE CASCADE
                );";
            using (MySqlCommand command = new MySqlCommand(createTableQuery, connection))
            {
                command.ExecuteNonQuery();
            }
        }
    }

    // Load data from the database into the application 
    public static void loadData()
    {
        Homepage.users = new List<User>();
        Homepage.teachingStaff = new List<TeachingStaff>();
        Homepage.administration = new List<Administration>();
        Homepage.students = new List<Students>();
        try
        {
            using (MySqlConnection connection = new MySqlConnection(DatabaseManager.ConnectionString))
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

                            byte[] profilePicture;
                            object profilePictureValue = reader["ProfilePicture"];

                            if (profilePictureValue != DBNull.Value && profilePictureValue != null)
                            {
                                // Convert byte[] to Image
                                profilePicture = (byte[])profilePictureValue;
                                Image profileImage = Homepage.ByteArrayToImage(profilePicture);
                                // convert the Image back to byte[]
                                byte[] convertedProfilePicture = Homepage.ImageToByteArray(profileImage);
                            }
                            else
                            {
                                profilePicture = new byte[0];
                                profilePicture = _data;
                            }

                            User user = new User(id, name, telephone, email, role, profilePicture);
                            // Check the role of the user and create the appropriate object
                            // Add the user to the appropriate list
                            // Add the teacher to the teachingStaff list
                            if (user != null && role == "teacher")
                            {
                                TeachingStaff teacher = DatabaseManager.TeachingStaffByID(user);
                                if (teacher != null)
                                {
                                    user = teacher;
                                    Homepage.teachingStaff.Add(teacher);
                                }
                            }
                            // Add the admin to the administration list
                            if (role == "admin")
                            {
                                Administration admin = DatabaseManager.AdminByID(user);
                                if (admin != null)
                                {
                                    user = admin;
                                    Homepage.administration.Add(admin);
                                }
                            }
                            // Add the student to the students list
                            if (role == "student")
                            {
                                Students student = DatabaseManager.StudentByID(user);
                                if (student != null)
                                {
                                    user = student;
                                    Homepage.students.Add(student);
                                }
                            }
                            Homepage.users.Add(user);
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
    // Insert data into the database 
    public static void InsertUserIntoDatabase(User user)
    {
        using (MySqlConnection connection = new MySqlConnection(ConnectionString))
        {
            try
            {
                connection.Open();
                string query = "INSERT INTO Users (userID, name, email, telephone, role, profilePicture) VALUES (@id, @name, @email, @telephone, @role, @profilePicture)";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", user.ID);
                    command.Parameters.AddWithValue("@name", user.Name);
                    command.Parameters.AddWithValue("@email", user.Email);
                    command.Parameters.AddWithValue("@telephone", user.Telephone);
                    command.Parameters.AddWithValue("@role", user.Role);
                    command.Parameters.AddWithValue("@profilePicture", user.ProfilePicture);
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error inserting teacher data: {ex.Message}");
            }
        }
    }
    // Teacher is inserted into the teachingStaff table
    public static void InsertTeachingStaffIntoDatabase(int id, double salary, string subject1, string subject2)
    {
        using (MySqlConnection connection = new MySqlConnection(ConnectionString))
        {
            try
            {
                connection.Open();
                string insertQuery = "INSERT INTO teachingStaff (UserID, Salary, Subject1, Subject2) VALUES (@id, @salary, @subject1, @subject2)";
                using (MySqlCommand command = new MySqlCommand(insertQuery, connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    command.Parameters.AddWithValue("@salary", salary);
                    command.Parameters.AddWithValue("@subject1", subject1);
                    command.Parameters.AddWithValue("@subject2", subject2);
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating teacher data: {ex.Message}");
            }
        }
    }
    public static void InsertAdminIntoDatabase(int id, double salary, string employmentType, int workingHours)
    {
        using (MySqlConnection connection = new MySqlConnection(ConnectionString))
        {
            try
            {
                connection.Open();
                string insertQuery = "INSERT INTO Administration (UserID, Salary, EmploymentType, WorkingHours) VALUES (@id, @salary, @employmentType, @workingHours)";
                using (MySqlCommand command = new MySqlCommand(insertQuery, connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    command.Parameters.AddWithValue("@salary", salary);
                    command.Parameters.AddWithValue("@employmentType", employmentType);
                    command.Parameters.AddWithValue("@workingHours", workingHours);
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating admin data: {ex.Message}");
            }
        }
    }

    public static void InsertStudentIntoDatabase(int id, string currentSubject1, string currentSubject2, string previousSubject1, string previousSubject2)
    {
        using (MySqlConnection connection = new MySqlConnection(ConnectionString))
        {
            try
            {
                connection.Open();
                string insertQuery = "INSERT INTO Students (UserID, CurrentSubject1, CurrentSubject2, PreviousSubject1, PreviousSubject2) VALUES (@id, @currentSubject1, @currentSubject2, @previousSubject1, @previousSubject2)";
                using (MySqlCommand command = new MySqlCommand(insertQuery, connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    command.Parameters.AddWithValue("@currentSubject1", currentSubject1);
                    command.Parameters.AddWithValue("@currentSubject2", currentSubject2);
                    command.Parameters.AddWithValue("@previousSubject1", previousSubject1);
                    command.Parameters.AddWithValue("@previousSubject2", previousSubject2);
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating student data: {ex.Message}");
            }
        }
    }
    // Delete user from the database 
    // The user is deleted from the Users table and the corresponding table based on the role
    public static void DeleteUserFromDatabase(int userId)
    {
        using (MySqlConnection connection = new MySqlConnection(ConnectionString))
        {
            connection.Open();

            string deleteQuery = "DELETE FROM Users WHERE UserID = @ID;";
            MySqlCommand cmd = new MySqlCommand(deleteQuery, connection);

            cmd.Parameters.AddWithValue("@ID", userId);

            cmd.ExecuteNonQuery();
        }
    }
    // Update user in the database
    // The user is updated in the Users table and the corresponding table based on the role
    public static void UpdateUserInDatabase(int userId, string name, string telephone, string email, byte[] image)
    {
        using (MySqlConnection connection = new MySqlConnection(ConnectionString))
        {
            connection.Open();

            string updateQuery = "UPDATE Users SET Name = @Name, Telephone = @Telephone, Email = @Email, profilePicture = @image WHERE userID = @userID;";
            MySqlCommand cmd = new MySqlCommand(updateQuery, connection);

            cmd.Parameters.AddWithValue("@userID", userId);
            cmd.Parameters.AddWithValue("@Name", name);
            cmd.Parameters.AddWithValue("@Telephone", telephone);
            cmd.Parameters.AddWithValue("@Email", email);
            cmd.Parameters.AddWithValue("@image", image);

            cmd.ExecuteNonQuery();
        }
    }
    public static bool TestConnection()
    {
        try
        {
            using (MySqlConnection connection = new MySqlConnection(ConnectionString))
            {
                connection.Open();
                return true;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Connection test failed: {ex.Message}");
            return false;
        }
    }


    public static void UpdateTeacherInDatabase(int userId, double salary, string subject1, string subject2)
    {
        using (MySqlConnection connection = new MySqlConnection(ConnectionString))
        {
            connection.Open();

            string updateQuery = "UPDATE teachingStaff SET Salary = @Salary, Subject1 = @Subject1, Subject2 = @Subject2 WHERE userID = @userID;";
            MySqlCommand cmd = new MySqlCommand(updateQuery, connection);

            cmd.Parameters.AddWithValue("@userID", userId);
            cmd.Parameters.AddWithValue("@Salary", salary);
            cmd.Parameters.AddWithValue("@Subject1", subject1);
            cmd.Parameters.AddWithValue("@Subject2", subject2);

            cmd.ExecuteNonQuery();
        }
    }
    // Load the teachingStaff data from the database by the user ID, and return a TeachingStaff object
    // Do it for the Administration and Students as well
    public static TeachingStaff TeachingStaffByID(User user)
    {
        try
        {
            using (MySqlConnection connection = new MySqlConnection(ConnectionString))
            {
                connection.Open();

                string selectUsersQuery = $"SELECT * FROM teachingStaff WHERE userID = @ID"; // Modify the query to include the parameter for userID

                using (MySqlCommand command = new MySqlCommand(selectUsersQuery, connection))
                {
                    command.Parameters.AddWithValue("@ID", user.ID); // Add parameter for userID

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string subject1 = Convert.ToString(reader["subject1"]);
                            string subject2 = Convert.ToString(reader["subject2"]);
                            string salary = Convert.ToString(reader["salary"]);
                            return new TeachingStaff(user.ID, user.Name, user.Telephone, user.Email, Convert.ToDouble(salary), subject1, subject2, user.ProfilePicture);
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading users from database: {ex.Message}");
        }

        return null; // Add a return statement outside of the try-catch block to ensure that a value is always returned
    }
    public static Administration AdminByID(User user)
    {
        try
        {
            using (MySqlConnection connection = new MySqlConnection(ConnectionString))
            {
                connection.Open();

                string selectUsersQuery = $"SELECT * FROM Administration WHERE userID = @ID"; // Modify the query to include the parameter for userID

                using (MySqlCommand command = new MySqlCommand(selectUsersQuery, connection))
                {
                    command.Parameters.AddWithValue("@ID", user.ID); // Add parameter for userID

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string employmentType = Convert.ToString(reader["employmentType"]);
                            string workingHours = Convert.ToString(reader["workingHours"]);
                            string salary = Convert.ToString(reader["salary"]);
                            Administration admin = new Administration(user.ID, user.Name, user.Telephone, user.Email, Convert.ToDouble(salary), employmentType, Convert.ToInt16(workingHours), user.ProfilePicture);
                            return admin;
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading users from database: {ex.Message}");
        }

        return null; // Add a return statement outside of the try-catch block to ensure that a value is always returned
    }

    public static Students StudentByID(User user)
    {
        try
        {
            using (MySqlConnection connection = new MySqlConnection(ConnectionString))
            {
                connection.Open();

                string selectUsersQuery = $"SELECT * FROM Students WHERE userID = @ID"; // Modify the query to include the parameter for userID

                using (MySqlCommand command = new MySqlCommand(selectUsersQuery, connection))
                {
                    command.Parameters.AddWithValue("@ID", user.ID); // Add parameter for userID

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string currentSubject1 = Convert.ToString(reader["CurrentSubject1"]);
                            string currentSubject2 = Convert.ToString(reader["CurrentSubject2"]);
                            string previousSubject1 = Convert.ToString(reader["PreviousSubject1"]);
                            string previousSubject2 = Convert.ToString(reader["PreviousSubject2"]);
                            return new Students(user.ID, user.Name, user.Telephone, user.Email, currentSubject1, currentSubject2, previousSubject1, previousSubject2, user.ProfilePicture);
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading users from database: {ex.Message}");
        }

        return null; // Add a return statement outside of the try-catch block to ensure that a value is always returned
    }

    public static void UpdateAdminInDatabase(int userId, double salary, string employmentType, int workingHours)
    {
        using (MySqlConnection connection = new MySqlConnection(ConnectionString))
        {
            connection.Open();

            string updateQuery = "UPDATE Administration SET Salary = @Salary, EmploymentType = @EmploymentType, WorkingHours = @WorkingHours WHERE userID = @userID;";
            MySqlCommand cmd = new MySqlCommand(updateQuery, connection);

            cmd.Parameters.AddWithValue("@userID", userId);
            cmd.Parameters.AddWithValue("@Salary", salary);
            cmd.Parameters.AddWithValue("@EmploymentType", employmentType);
            cmd.Parameters.AddWithValue("@WorkingHours", workingHours);

            cmd.ExecuteNonQuery();
        }
    }

    public static void UpdateStudentInDatabase(int userId, string currentSubject1, string currentSubject2, string previousSubject1, string previousSubject2)
    {
        using (MySqlConnection connection = new MySqlConnection(ConnectionString))
        {
            connection.Open();

            string updateQuery = "UPDATE Students SET CurrentSubject1 = @CurrentSubject1, CurrentSubject2 = @CurrentSubject2, PreviousSubject1 = @PreviousSubject1, PreviousSubject2 = @PreviousSubject2 WHERE userID = @userID;";
            MySqlCommand cmd = new MySqlCommand(updateQuery, connection);

            cmd.Parameters.AddWithValue("@userID", userId);
            cmd.Parameters.AddWithValue("@CurrentSubject1", currentSubject1);
            cmd.Parameters.AddWithValue("@CurrentSubject2", currentSubject2);
            cmd.Parameters.AddWithValue("@PreviousSubject1", previousSubject1);
            cmd.Parameters.AddWithValue("@PreviousSubject2", previousSubject2);

            cmd.ExecuteNonQuery();
        }
    }
}
