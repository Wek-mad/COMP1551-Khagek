using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace SchoolControl
{
    public partial class CreateAdmin : Form
    {
        private byte[] selectedImageBytes;
        public CreateAdmin()
        {
            InitializeComponent();
        }
        // Method to upload an image
        private void uploadButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp";
            // Code to execute if the image is submitted successfully
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string imagePath = openFileDialog.FileName;
                // Code to execute if the image is too large 
                try
                {
                    selectedImageBytes = File.ReadAllBytes(imagePath);
                    pictureBox1.Image = System.Drawing.Image.FromFile(imagePath);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error reading image: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        // Method to save the data of the new administrator
        private void saveButton_Click(object sender, EventArgs e)
        {
            if (selectedImageBytes == null)
            {
                selectedImageBytes = new byte[0];
                selectedImageBytes = File.ReadAllBytes("default.png");
            }
            // Code to execute if any of the fields are empty
            if (nameBox.Text == "" || phoneBox.Text == "" || emailBox.Text == "" || typeComboBox.Text == "")
            {
                MessageBox.Show("Please fill in all fields.");
                return;
            }
            // Code to execute if salaryBox is a valid double and greater than 0
            if (!(double.TryParse(salaryBox.Text, out double salary) || salary > 0))
            {
                MessageBox.Show("Invalid salary value. Please enter a valid double integer.");
                return;
            }
            // Code to execute if workTimeBox is a valid integer and greater than 0
            if (!(int.TryParse(workTimeBox.Text, out int workingTime) || workingTime > 0))
            {

                MessageBox.Show("Invalid working time value. Please enter a valid positive integer.");
                return;
            }
            else
            {
                User user = new User(Homepage.users.Count + 1, nameBox.Text, phoneBox.Text, emailBox.Text, "admin", selectedImageBytes);
                DatabaseManager.InsertUserIntoDatabase(user);
                Homepage.users.Add(user);
                Administration admin = new Administration(user.ID, user.Name, user.Telephone, user.Email, salary, typeComboBox.Text, workingTime, selectedImageBytes);
                DatabaseManager.InsertAdminIntoDatabase(user.ID, admin.Salary, admin.FullTimePartTime, admin.WorkingHours);
                MessageBox.Show("Administer created successfully!");
                Homepage.reload();
                this.Close();
            }
        }
    }
}
