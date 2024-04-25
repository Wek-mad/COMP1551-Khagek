using System.Windows.Forms;

namespace SchoolControl
{
    partial class Homepage
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Homepage));
            this.label1 = new System.Windows.Forms.Label();
            dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Createnew = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.editButton = new System.Windows.Forms.Button();
            this.deleteButton = new System.Windows.Forms.Button();
            this.viewGroupButton = new System.Windows.Forms.Button();
            this.roleInput = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F);
            this.label1.Location = new System.Drawing.Point(46, 30);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 26);
            this.label1.TabIndex = 0;
            // 
            // dataGridView1
            // 
            dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            dataGridView1.ColumnHeadersHeight = 29;
            dataGridView1.Location = new System.Drawing.Point(25, 66);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.RowTemplate.Height = 24;
            dataGridView1.Size = new System.Drawing.Size(1445, 377);
            dataGridView1.TabIndex = 1;
            // 
            // Createnew
            // 
            this.Createnew.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.Createnew.Font = new System.Drawing.Font("Trebuchet MS", 13.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Createnew.Location = new System.Drawing.Point(1241, 21);
            this.Createnew.Margin = new System.Windows.Forms.Padding(2);
            this.Createnew.Name = "Createnew";
            this.Createnew.Size = new System.Drawing.Size(107, 35);
            this.Createnew.TabIndex = 2;
            this.Createnew.Text = "Add New";
            this.Createnew.UseVisualStyleBackColor = false;
            this.Createnew.Click += new System.EventHandler(this.Createnew_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.SystemColors.Control;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F);
            this.button1.Location = new System.Drawing.Point(266, 26);
            this.button1.Margin = new System.Windows.Forms.Padding(2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(107, 35);
            this.button1.TabIndex = 3;
            this.button1.Text = "Reload ";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.reload_Click);
            // 
            // editButton
            // 
            this.editButton.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.editButton.Font = new System.Drawing.Font("Trebuchet MS", 13.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.editButton.Location = new System.Drawing.Point(1363, 21);
            this.editButton.Margin = new System.Windows.Forms.Padding(2);
            this.editButton.Name = "editButton";
            this.editButton.Size = new System.Drawing.Size(107, 35);
            this.editButton.TabIndex = 4;
            this.editButton.Text = "Edit";
            this.editButton.UseVisualStyleBackColor = false;
            this.editButton.Click += new System.EventHandler(this.editButton_Click);
            // 
            // deleteButton
            // 
            this.deleteButton.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.deleteButton.Font = new System.Drawing.Font("Trebuchet MS", 13.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.deleteButton.Location = new System.Drawing.Point(1118, 21);
            this.deleteButton.Margin = new System.Windows.Forms.Padding(2);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(107, 35);
            this.deleteButton.TabIndex = 5;
            this.deleteButton.Text = "Delete";
            this.deleteButton.UseVisualStyleBackColor = false;
            this.deleteButton.Click += new System.EventHandler(this.deleteButton_Click);
            // 
            // viewGroupButton
            // 
            this.viewGroupButton.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.viewGroupButton.Font = new System.Drawing.Font("Trebuchet MS", 13.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.viewGroupButton.Location = new System.Drawing.Point(991, 21);
            this.viewGroupButton.Margin = new System.Windows.Forms.Padding(2);
            this.viewGroupButton.Name = "viewGroupButton";
            this.viewGroupButton.Size = new System.Drawing.Size(101, 35);
            this.viewGroupButton.TabIndex = 6;
            this.viewGroupButton.Text = "View";
            this.viewGroupButton.UseVisualStyleBackColor = false;
            this.viewGroupButton.Click += new System.EventHandler(this.viewGroupButton_Click);
            // 
            // roleInput
            // 
            this.roleInput.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.roleInput.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.roleInput.Font = new System.Drawing.Font("Trebuchet MS", 13.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.roleInput.FormattingEnabled = true;
            this.roleInput.Items.AddRange(new object[] {
            "All",
            "Admin",
            "Teacher",
            "Student"});
            this.roleInput.Location = new System.Drawing.Point(850, 21);
            this.roleInput.Name = "roleInput";
            this.roleInput.Size = new System.Drawing.Size(121, 35);
            this.roleInput.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Trebuchet MS", 13.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(771, 24);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 27);
            this.label3.TabIndex = 9;
            this.label3.Text = "Role:";
            // 
            // Homepage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(1532, 475);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.roleInput);
            this.Controls.Add(this.viewGroupButton);
            this.Controls.Add(this.deleteButton);
            this.Controls.Add(this.editButton);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.Createnew);
            this.Controls.Add(dataGridView1);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Homepage";
            this.Text = "Homepage";
            this.Load += new System.EventHandler(this.reload_Click);
            ((System.ComponentModel.ISupportInitialize)(dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button Createnew;
        private Button button1;
        private Button editButton;
        private Button deleteButton;
        private Button viewGroupButton;
        private ComboBox roleInput;
        private Label label3;
        private static DataGridView dataGridView1;
    }
}