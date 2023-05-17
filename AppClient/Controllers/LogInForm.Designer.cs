namespace MPPCSharp.Forms
{
    partial class LogInForm
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
            System.Windows.Forms.Label label2;
            System.Windows.Forms.Label label3;
            this.usernameTextBox = new System.Windows.Forms.TextBox();
            this.passwordTextBox = new System.Windows.Forms.TextBox();
            this.logInButton = new System.Windows.Forms.Button();
            this.registerButton = new System.Windows.Forms.Button();
            this.userExists = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Cursor = System.Windows.Forms.Cursors.Arrow;
            label2.Enabled = false;
            label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            label2.Location = new System.Drawing.Point(273, 146);
            label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(102, 25);
            label2.TabIndex = 0;
            label2.Text = "Username";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Enabled = false;
            label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            label3.Location = new System.Drawing.Point(273, 288);
            label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(98, 25);
            label3.TabIndex = 0;
            label3.Text = "Password";
            label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // usernameTextBox
            // 
            this.usernameTextBox.AccessibleDescription = "";
            this.usernameTextBox.AccessibleName = "";
            this.usernameTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.usernameTextBox.Location = new System.Drawing.Point(280, 189);
            this.usernameTextBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.usernameTextBox.Multiline = true;
            this.usernameTextBox.Name = "usernameTextBox";
            this.usernameTextBox.Size = new System.Drawing.Size(382, 52);
            this.usernameTextBox.TabIndex = 0;
            // 
            // passwordTextBox
            // 
            this.passwordTextBox.Location = new System.Drawing.Point(280, 331);
            this.passwordTextBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.passwordTextBox.Multiline = true;
            this.passwordTextBox.Name = "passwordTextBox";
            this.passwordTextBox.PasswordChar = '*';
            this.passwordTextBox.Size = new System.Drawing.Size(382, 52);
            this.passwordTextBox.TabIndex = 3;
            // 
            // logInButton
            // 
            this.logInButton.Location = new System.Drawing.Point(280, 495);
            this.logInButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.logInButton.Name = "logInButton";
            this.logInButton.Size = new System.Drawing.Size(170, 40);
            this.logInButton.TabIndex = 5;
            this.logInButton.Text = "Log In";
            this.logInButton.UseVisualStyleBackColor = true;
            this.logInButton.Click += new System.EventHandler(this.logInClick);
            // 
            // registerButton
            // 
            this.registerButton.Location = new System.Drawing.Point(495, 495);
            this.registerButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.registerButton.Name = "registerButton";
            this.registerButton.Size = new System.Drawing.Size(170, 40);
            this.registerButton.TabIndex = 6;
            this.registerButton.Text = "Register";
            this.registerButton.UseVisualStyleBackColor = true;
            this.registerButton.Click += new System.EventHandler(this.registerClick);
            // 
            // userExists
            // 
            this.userExists.AutoSize = true;
            this.userExists.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.userExists.ForeColor = System.Drawing.Color.Firebrick;
            this.userExists.Location = new System.Drawing.Point(280, 414);
            this.userExists.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.userExists.Name = "userExists";
            this.userExists.Size = new System.Drawing.Size(144, 22);
            this.userExists.TabIndex = 7;
            this.userExists.Text = "User doesn\'t exit";
            this.userExists.Visible = false;
            // 
            // LogInForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(970, 677);
            this.Controls.Add(this.userExists);
            this.Controls.Add(this.registerButton);
            this.Controls.Add(this.logInButton);
            this.Controls.Add(label3);
            this.Controls.Add(this.passwordTextBox);
            this.Controls.Add(label2);
            this.Controls.Add(this.usernameTextBox);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "LogInForm";
            this.Text = "LogInForm";
            this.Load += new System.EventHandler(this.LogInForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox usernameTextBox;
        private System.Windows.Forms.TextBox passwordTextBox;
        private System.Windows.Forms.Button logInButton;
        private System.Windows.Forms.Button registerButton;
        private System.Windows.Forms.Label userExists;
    }
}