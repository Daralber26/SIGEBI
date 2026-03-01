namespace SIGEBI.Desktop.Forms
{
    partial class LoginForm
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
            label1 = new Label();
            txtEmail = new TextBox();
            txtPassword = new TextBox();
            label2 = new Label();
            btnLogin = new Button();
            lblError = new Label();
            SuspendLayout();
            // 
            // label1
            // 
            label1.BackColor = SystemColors.ButtonShadow;
            label1.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(149, 62);
            label1.Name = "label1";
            label1.Size = new Size(185, 24);
            label1.TabIndex = 0;
            label1.Text = "Email";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // txtEmail
            // 
            txtEmail.Location = new Point(149, 100);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(185, 23);
            txtEmail.TabIndex = 1;
            // 
            // txtPassword
            // 
            txtPassword.Location = new Point(149, 184);
            txtPassword.Name = "txtPassword";
            txtPassword.Size = new Size(185, 23);
            txtPassword.TabIndex = 3;
            // 
            // label2
            // 
            label2.BackColor = SystemColors.ButtonShadow;
            label2.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(149, 144);
            label2.Name = "label2";
            label2.Size = new Size(185, 24);
            label2.TabIndex = 8;
            label2.Text = "Contraseña";
            label2.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // btnLogin
            // 
            btnLogin.BackColor = Color.DarkOliveGreen;
            btnLogin.Location = new Point(411, 245);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(132, 47);
            btnLogin.TabIndex = 9;
            btnLogin.Text = "Iniciar sesión";
            btnLogin.UseVisualStyleBackColor = false;
            // 
            // lblError
            // 
            lblError.BackColor = Color.IndianRed;
            lblError.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblError.Location = new Point(40, 245);
            lblError.Name = "lblError";
            lblError.Size = new Size(341, 87);
            lblError.TabIndex = 10;
            lblError.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // LoginForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(812, 485);
            Controls.Add(lblError);
            Controls.Add(btnLogin);
            Controls.Add(label2);
            Controls.Add(txtPassword);
            Controls.Add(txtEmail);
            Controls.Add(label1);
            Name = "LoginForm";
            Text = "LoginForm";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox txtEmail;
        private TextBox txtPassword;
        private Label label2;
        private Button btnLogin;
        private Label lblError;
    }
}