using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SchoolControl
{
    // Login form for the application
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }
        // When the login button is clicked, the user is logged in
        private void logInButton_Click(object sender, EventArgs e)
        {
            // Get the address, username and password from the user input
            string address = this.comboBox1.GetItemText(this.comboBox1.SelectedItem);
            string username = this.usernameInput.Text;
            string password = this.passwordInput.Text;
            if (address == "" || username == "")
            {
                MessageBox.Show("Fill in the blank!");
            }
            else
            {
                try
                {
                    DatabaseManager.ConnectionString = $"Server={address}; Database = test; User Id = {username}; Password ={password}";
                    // Attempt to log in with the given credentials
                    if (DatabaseManager.TestConnection())
                    {
                        MessageBox.Show("Welcome to SchoolControl!");
                        this.Hide();
                        // Set the connection string and initialize the database with the given credentials
                        DatabaseManager.InitializeDatabase();
                        // Open the homepage with the given username
                        Homepage homepage = new Homepage(username);
                        homepage.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("Wrong Password or Username!");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message);
                }
            }
        }
    }
}
