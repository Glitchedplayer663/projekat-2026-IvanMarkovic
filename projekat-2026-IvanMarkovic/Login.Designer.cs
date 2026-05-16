namespace projekat_2026_IvanMarkovic
{
    partial class Login
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.LblTitle = new System.Windows.Forms.Label();
            this.LblEmail = new System.Windows.Forms.Label();
            this.LblPass = new System.Windows.Forms.Label();
            this.TBoxEmail = new System.Windows.Forms.TextBox();
            this.TBoxPass = new System.Windows.Forms.TextBox();
            this.BtnLogin = new System.Windows.Forms.Button();
            this.LinkRegistracija = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();

            this.LblTitle.AutoSize = true;
            this.LblTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.LblTitle.Location = new System.Drawing.Point(60, 20);
            this.LblTitle.Name = "LblTitle";
            this.LblTitle.Text = "Mobilizacija - Login";

            this.LblEmail.AutoSize = true;
            this.LblEmail.Location = new System.Drawing.Point(40, 80);
            this.LblEmail.Name = "LblEmail";
            this.LblEmail.Text = "Email:";

            this.TBoxEmail.Location = new System.Drawing.Point(40, 100);
            this.TBoxEmail.Name = "TBoxEmail";
            this.TBoxEmail.Size = new System.Drawing.Size(300, 22);

            this.LblPass.AutoSize = true;
            this.LblPass.Location = new System.Drawing.Point(40, 140);
            this.LblPass.Name = "LblPass";
            this.LblPass.Text = "Lozinka:";

            this.TBoxPass.Location = new System.Drawing.Point(40, 160);
            this.TBoxPass.Name = "TBoxPass";
            this.TBoxPass.PasswordChar = '*';
            this.TBoxPass.Size = new System.Drawing.Size(300, 22);

            this.BtnLogin.Location = new System.Drawing.Point(40, 205);
            this.BtnLogin.Name = "BtnLogin";
            this.BtnLogin.Size = new System.Drawing.Size(300, 35);
            this.BtnLogin.Text = "Prijavi se";
            this.BtnLogin.Click += new System.EventHandler(this.BtnLogin_Click);

            this.LinkRegistracija.AutoSize = true;
            this.LinkRegistracija.Location = new System.Drawing.Point(100, 255);
            this.LinkRegistracija.Name = "LinkRegistracija";
            this.LinkRegistracija.Text = "Nemate nalog? Registrujte se";
            this.LinkRegistracija.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkRegistracija_LinkClicked);

            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 300);
            this.Controls.Add(this.LblTitle);
            this.Controls.Add(this.LblEmail);
            this.Controls.Add(this.TBoxEmail);
            this.Controls.Add(this.LblPass);
            this.Controls.Add(this.TBoxPass);
            this.Controls.Add(this.BtnLogin);
            this.Controls.Add(this.LinkRegistracija);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Login";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Login - Mobilizacija";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Label LblTitle;
        private System.Windows.Forms.Label LblEmail;
        private System.Windows.Forms.Label LblPass;
        private System.Windows.Forms.TextBox TBoxEmail;
        private System.Windows.Forms.TextBox TBoxPass;
        private System.Windows.Forms.Button BtnLogin;
        private System.Windows.Forms.LinkLabel LinkRegistracija;
    }
}
