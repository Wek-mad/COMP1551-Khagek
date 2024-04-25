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
    public partial class CreateSelectionForm : Form
    {
        public CreateSelectionForm()
        {
            InitializeComponent();
        }

        private void createAdmin_Click(object sender, EventArgs e)
        {
            CreateAdmin createAdmin = new CreateAdmin();
            createAdmin.Show();
            this.Close();
        }

        private void createTeacher_Click(object sender, EventArgs e)
        {
            CreateTeacher createTeacher = new CreateTeacher();
            createTeacher.Show();
            this.Close();
        }

        private void createStudent_Click(object sender, EventArgs e)
        {
            CreateStudent createStudent = new CreateStudent();
            createStudent.Show();
            this.Close();
        }
    }
}
