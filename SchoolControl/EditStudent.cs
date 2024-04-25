using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using Mysqlx.Datatypes;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using System.Xml.Linq;

namespace SchoolControl
{
    public partial class EditStudent : Form
    {
        private byte[] selectedImageBytes;
        private int id;
        public EditStudent(User user)
        {
            Students staff = DatabaseManager.StudentByID(user);
            InitializeComponent();
            nameBox.Text = user.Name;
            phoneBox.Text = user.Telephone;
            emailBox.Text = user.Email;
            id = user.ID;
            sub1Box.Text = staff.CurrentSubject1;
            sub2Box.Text = staff.CurrentSubject2;
            sub3Box.Text = staff.PrevSubject1;
            sub4Box.Text = staff.PrevSubject2;
            pictureBox1.Image = Homepage.ByteArrayToImage(user.ProfilePicture);
            selectedImageBytes = user.ProfilePicture;
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
            // Check if all fields are filled
            if (nameBox.Text == "" || phoneBox.Text == "" || emailBox.Text == "" || sub1Box.Text == "" || sub2Box.Text == "" || sub3Box.Text == "" || sub4Box.Text == "")
            {
                MessageBox.Show("Please fill in all fields.");
                return;
            }
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
            var studentToEdit = Homepage.students.Find(user => user.ID == id); // Replace 'id' with 'userId'
            if (studentToEdit != null)
            {
                studentToEdit.CurrentSubject1 = sub1Box.Text;
                studentToEdit.CurrentSubject2 = sub2Box.Text;
                studentToEdit.PrevSubject1 = sub3Box.Text;
                studentToEdit.PrevSubject2 = sub4Box.Text;
            }
            else
            {
                Console.WriteLine("User not found.");
            }
            DatabaseManager.UpdateStudentInDatabase(id, sub1Box.Text, sub2Box.Text, sub3Box.Text, sub4Box.Text); // Replace 'id' with 'userId'
            MessageBox.Show("Saved");
            Homepage.reload();
            this.Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
