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
    public partial class EditTeacher : Form
    {
        private byte[] selectedImageBytes;
        private int id;
        public EditTeacher(User user)
        {
            InitializeComponent();
            id = user.ID;
            nameBox.Text = user.Name;
            phoneBox.Text = user.Telephone;
            emailBox.Text = user.Email;
            TeachingStaff teacher = DatabaseManager.TeachingStaffByID(user);
            salaryBox.Text = Convert.ToString(teacher.Salary);
            sub1Box.Text = teacher.Subject1;
            sub2Box.Text = teacher.Subject2;
            pictureBox1.Image = Homepage.ByteArrayToImage(user.ProfilePicture);
            selectedImageBytes = user.ProfilePicture;
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            DatabaseManager.UpdateUserInDatabase(id, nameBox.Text, phoneBox.Text, emailBox.Text, selectedImageBytes);
            var userToEdit = Homepage.users.Find(user => user.ID == id);
            if (userToEdit != null)
            {
                userToEdit.Name = nameBox.Text;
                userToEdit.Telephone = phoneBox.Text;
                userToEdit.Email = emailBox.Text;
            }
            else
            {
                Console.WriteLine("User not found.");
            }
            var teacherToEdit = Homepage.teachingStaff.Find(user => user.ID == id); // Replace 'id' with 'userId'
            if (teacherToEdit != null)
            {
                teacherToEdit.Subject1 = sub1Box.Text;
                teacherToEdit.Subject2 = sub2Box.Text;
                teacherToEdit.Salary = Convert.ToDouble(salaryBox.Text);
                teacherToEdit.Name = nameBox.Text;
                teacherToEdit.Telephone = phoneBox.Text;
                teacherToEdit.Email = emailBox.Text;
            }
            else
            {
                Console.WriteLine("User not found.");
            }
            DatabaseManager.UpdateTeacherInDatabase(id, Convert.ToDouble(salaryBox.Text), sub1Box.Text, sub2Box.Text); // Replace 'id' with 'userId'
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
