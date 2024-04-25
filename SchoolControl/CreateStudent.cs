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
    public partial class CreateStudent : Form
    {
        private byte[] selectedImageBytes;
        public CreateStudent()
        {
            InitializeComponent();
            this.pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
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

        private void saveButton_Click(object sender, EventArgs e)
        {
            if (selectedImageBytes == null)
            {
                selectedImageBytes = new byte[0];
                selectedImageBytes = File.ReadAllBytes("default.png");
            }
            User user = new User(Homepage.users.Count + 1, nameBox.Text, phoneBox.Text, emailBox.Text, "student", selectedImageBytes);
            DatabaseManager.InsertUserIntoDatabase(user);
            Homepage.users.Add(user);
            Students student = new Students(user.ID, user.Name, user.Telephone, user.Email, sub1Box.Text, sub2Box.Text, sub3Box.Text, sub4Box.Text, selectedImageBytes);
            DatabaseManager.InsertStudentIntoDatabase(user.ID, student.CurrentSubject1, student.CurrentSubject2, student.PrevSubject1, student.PrevSubject2);
            MessageBox.Show("Student created successfully!");
            Homepage.reload();
            this.Close();
        }
    }
}