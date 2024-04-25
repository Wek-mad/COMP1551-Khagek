namespace SchoolControl
{
    partial class CreateSelectionForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CreateSelectionForm));
            this.createteacher = new System.Windows.Forms.Button();
            this.createstudent = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // createteacher
            // 
            this.createteacher.Font = new System.Drawing.Font("Trebuchet MS", 13.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.createteacher.Location = new System.Drawing.Point(34, 44);
            this.createteacher.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.createteacher.Name = "createteacher";
            this.createteacher.Size = new System.Drawing.Size(146, 45);
            this.createteacher.TabIndex = 0;
            this.createteacher.Text = "Teacher";
            this.createteacher.UseVisualStyleBackColor = true;
            this.createteacher.Click += new System.EventHandler(this.createTeacher_Click);
            // 
            // createstudent
            // 
            this.createstudent.Font = new System.Drawing.Font("Trebuchet MS", 13.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.createstudent.Location = new System.Drawing.Point(237, 44);
            this.createstudent.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.createstudent.Name = "createstudent";
            this.createstudent.Size = new System.Drawing.Size(146, 45);
            this.createstudent.TabIndex = 1;
            this.createstudent.Text = "Student";
            this.createstudent.UseVisualStyleBackColor = true;
            this.createstudent.Click += new System.EventHandler(this.createStudent_Click);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Trebuchet MS", 13.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(422, 44);
            this.button1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(176, 45);
            this.button1.TabIndex = 2;
            this.button1.Text = "Adminstator";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.createAdmin_Click);
            // 
            // CreateSelectionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(633, 133);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.createstudent);
            this.Controls.Add(this.createteacher);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "CreateSelectionForm";
            this.Text = "CreateSelectionForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button createteacher;
        private System.Windows.Forms.Button createstudent;
        private System.Windows.Forms.Button button1;
    }
}