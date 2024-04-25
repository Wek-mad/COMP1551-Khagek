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
    public partial class CreateTeacher : Form
    {
        private byte[] selectedImageBytes;
        public CreateTeacher()
        {
            InitializeComponent();
        }

        private void saveButton_Click(object sender, EventArgs e)
        {

            if (selectedImageBytes == null)
            {
                selectedImageBytes = new byte[0];
                selectedImageBytes = File.ReadAllBytes("default.png");
            }
            // Code to execute if salaryBox is a valid integer and greater than 0
            if (int.TryParse(salaryBox.Text, out int salary) && salary > 0)
            {

                User user = new User(Homepage.users.Count + 1, nameBox.Text, phoneBox.Text, emailBox.Text, "teacher", selectedImageBytes);
                DatabaseManager.InsertUserIntoDatabase(user);
                Homepage.users.Add(user);
                TeachingStaff teacher = new TeachingStaff(user.ID, user.Name, user.Telephone, user.Email, salary, sub1Box.Text, sub2Box.Text, selectedImageBytes);
                DatabaseManager.InsertTeachingStaffIntoDatabase(user.ID, teacher.Salary, teacher.Subject1, teacher.Subject2);
                MessageBox.Show("Teacher created successfully!");
                Homepage.reload();
                this.Close();
            }
            else
            {
                MessageBox.Show("Invalid salary value. Please enter a valid positive integer.");
                return;
            }
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
