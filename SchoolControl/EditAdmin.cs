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
    public partial class EditAdmin : Form
    {
        // Variables for the selected image and the user ID
        private byte[] selectedImageBytes;
        private int id;
        // Constructor for the EditAdmin form that takes a User object as a parameter
        // The User object is used to fill the textboxes with the user's data
        public EditAdmin(User user)
        {
            InitializeComponent();
            id = user.ID;
            // Fill the textboxes with the user's data and display the profile picture
            nameBox.Text = user.Name;
            phoneBox.Text = user.Telephone;
            emailBox.Text = user.Email;
            Administration administration = DatabaseManager.AdminByID(user);
            salaryBox.Text = Convert.ToString(administration.Salary);
            workTimeBox.Text = Convert.ToString(administration.WorkingHours);
            typeComboBox.Text = administration.FullTimePartTime;
            pictureBox1.Image = Homepage.ByteArrayToImage(user.ProfilePicture);
            selectedImageBytes = user.ProfilePicture;
        }
        // Method to save the changes made to the user's data
        // The method updates the user's data in the database and in the lists of users and administration
        private void saveButton_Click(object sender, EventArgs e)
        {
            // Check if any of the fields are empty
            if (string.IsNullOrEmpty(nameBox.Text) || string.IsNullOrEmpty(phoneBox.Text) || string.IsNullOrEmpty(emailBox.Text))
            {
                MessageBox.Show("Please fill in all fields.");
                return;
            }
            DatabaseManager.UpdateUserInDatabase(id, nameBox.Text, phoneBox.Text, emailBox.Text, selectedImageBytes);
            // Check if salaryBox is a valid double and greater than 0
            if (!(double.TryParse(salaryBox.Text, out double salary) || salary > 0))
            {
                MessageBox.Show("Invalid salary value. Please enter a valid positive integer.");
                return;
            }
            // Check if workTimeBox is a valid integer and greater than 0
            if (!(int.TryParse(workTimeBox.Text, out int workingTime) || workingTime <= 0))
            {
                MessageBox.Show("Invalid working time value. Please enter a valid positive integer.");
                return;
            }
            DatabaseManager.UpdateAdminInDatabase(id, Convert.ToDouble(salaryBox.Text), typeComboBox.Text, Convert.ToInt16(workTimeBox.Text)); // Replace 'id' with 'userId'
            MessageBox.Show("Saved");
            Homepage.reload();
            this.Close();
        }

        private void uploadButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string imagePath = openFileDialog.FileName;

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
    }
}
