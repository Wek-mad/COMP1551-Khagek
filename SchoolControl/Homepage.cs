using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Google.Protobuf.Compiler;
using MySql.Data.MySqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace SchoolControl
{
    public partial class Homepage : Form
    {
        private string username;  // The username of the user that is currently logged in
        public static List<User> users; // List of all users
        public static List<TeachingStaff> teachingStaff; // List of all teaching staff
        public static List<Administration> administration; // List of all administration
        public static List<Students> students; // List of all students
        public Homepage(string username) // Constructor for the Homepage form
        {
            InitializeComponent();
            this.username = username;
            DatabaseManager.loadData();
            label1.Text = "Hello, " + username;
            reload();
        }
        private void Createnew_Click(object sender, EventArgs e) // Method to load the data from the database
        {
            CreateSelectionForm CreateSelectionForm = new CreateSelectionForm();
            CreateSelectionForm.Show();
            reload_Click(sender, e);
        }
        public static byte[] ImageToByteArray(Image img) // Method to convert an image to a byte array
        {
            MemoryStream ms = new MemoryStream();
            img.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
            return ms.ToArray();
        }
        public static Image ByteArrayToImage(byte[] data) // Method to convert a byte array to an image
        {
            MemoryStream ms = new MemoryStream(data);
            Image returnImage = Image.FromStream(ms);
            return returnImage;
        }
        private void reload_Click(object sender, EventArgs e)
        {
            reload();
        }

        public static void reload() // Method to reload the data from the database
        {
            DatabaseManager.loadData();
            dataGridView1.DataSource = users;
            dataGridView1.Columns[0].Width = 80;
            ((DataGridViewImageColumn)dataGridView1.Columns["ProfilePicture"]).ImageLayout = DataGridViewImageCellLayout.Zoom;
            dataGridView1.Refresh();
        }
        // Method to delete a user from the database
        private void deleteButton_Click(object sender, EventArgs e) 
        {            
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // check if the user is sure they want to delete the user
                if (MessageBox.Show("Are you sure you want to delete this user?", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    // Delete the user
                    // Access the data in the selected row through the DataBoundItem property
                    foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                    {
                        var user = (User)row.DataBoundItem;
                        users.Remove(user);
                        DatabaseManager.DeleteUserFromDatabase(user.ID);
                        if (user.Role == "teacher") // Remove the user from the teaching staff list
                        {
                            var teacher = teachingStaff.FirstOrDefault(t => t.ID == user.ID);
                            if (teacher != null)
                            {
                                teachingStaff.Remove(teacher);
                            }
                        }
                        else if (user.Role == "admin") // Remove the user from the administration list
                        {
                            var admin = administration.FirstOrDefault(a => a.ID == user.ID);
                            if (admin != null)
                            {
                                administration.Remove(admin);
                            }
                        }
                        else if (user.Role == "student") // Remove the user from the students list
                        {
                            var student = students.FirstOrDefault(s => s.ID == user.ID);
                            if (student != null)
                            {
                                students.Remove(student);
                            }
                        }
                    }
                    reload_Click(sender, e);
                }
            }
            else
            {
                // Handle the case accordingly
                // No row or multiple rows are selected
                MessageBox.Show("Please select a single row to edit.");
            }
        }
        // Method to edit a user
        private void editButton_Click(object sender, EventArgs e)
        {
            // One row is selected
            if (dataGridView1.SelectedRows.Count == 1)
            {
                // Access the data in the selected row through the DataBoundItem property
                var user = (User)dataGridView1.SelectedRows[0].DataBoundItem;
                if (user.Role == "student") // Edit the student
                {
                    EditStudent editStudent = new EditStudent(user);
                    editStudent.Show();
                }
                else if (user.Role == "teacher") // Edit the teacher
                {
                    EditTeacher editTeacher = new EditTeacher(user);
                    editTeacher.Show();
                }
                else if (user.Role == "admin") // Edit the admin
                {
                    EditAdmin editAdmin = new EditAdmin(user);
                    editAdmin.Show();
                }
                reload_Click(sender, e); // Reload the data after editing
            }
            else
            {
                // No row or multiple rows are selected
                // Handle the case accordingly
                MessageBox.Show("Please select a single row to edit.");
            }
        }

        private void viewGroupButton_Click(object sender, EventArgs e) // Method to view the groups
        {
            string role = roleInput.Text;
            switch (role)
            {
                case "All":
                    dataGridView1.DataSource = users;
                    dataGridView1.Refresh();
                    break;
                case "Admin":
                    dataGridView1.DataSource = administration;
                    dataGridView1.Refresh();
                    break;
                case "Teacher":
                    dataGridView1.DataSource = teachingStaff;
                    dataGridView1.Refresh();
                    break;
                case "Student":
                    dataGridView1.DataSource = students;
                    dataGridView1.Refresh();
                    break;
            }
        }
    }
}
