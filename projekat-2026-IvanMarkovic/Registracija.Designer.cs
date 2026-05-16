namespace projekat_2026_IvanMarkovic
{
    partial class Registracija
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
            this.LblPassOpet = new System.Windows.Forms.Label();
            this.TBoxEmail = new System.Windows.Forms.TextBox();
            this.TBoxPass = new System.Windows.Forms.TextBox();
            this.TBoxPassOpet = new System.Windows.Forms.TextBox();
            this.BtnRegistracija = new System.Windows.Forms.Button();
            this.SuspendLayout();

            this.LblTitle.AutoSize = true;
            this.LblTitle.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.LblTitle.Location = new System.Drawing.Point(60, 20);
            this.LblTitle.Text = "Registracija novog naloga";

            this.LblEmail.AutoSize = true;
            this.LblEmail.Location = new System.Drawing.Point(40, 75);
            this.LblEmail.Text = "Email:";

            this.TBoxEmail.Location = new System.Drawing.Point(40, 95);
            this.TBoxEmail.Name = "TBoxEmail";
            this.TBoxEmail.Size = new System.Drawing.Size(300, 22);

            this.LblPass.AutoSize = true;
            this.LblPass.Location = new System.Drawing.Point(40, 135);
            this.LblPass.Text = "Lozinka:";

            this.TBoxPass.Location = new System.Drawing.Point(40, 155);
            this.TBoxPass.Name = "TBoxPass";
            this.TBoxPass.PasswordChar = '*';
            this.TBoxPass.Size = new System.Drawing.Size(300, 22);

            this.LblPassOpet.AutoSize = true;
            this.LblPassOpet.Location = new System.Drawing.Point(40, 195);
            this.LblPassOpet.Text = "Ponovite lozinku:";

            this.TBoxPassOpet.Location = new System.Drawing.Point(40, 215);
            this.TBoxPassOpet.Name = "TBoxPassOpet";
            this.TBoxPassOpet.PasswordChar = '*';
            this.TBoxPassOpet.Size = new System.Drawing.Size(300, 22);

            this.BtnRegistracija.Location = new System.Drawing.Point(40, 260);
            this.BtnRegistracija.Name = "BtnRegistracija";
            this.BtnRegistracija.Size = new System.Drawing.Size(300, 35);
            this.BtnRegistracija.Text = "Registruj se";
            this.BtnRegistracija.Click += new System.EventHandler(this.BtnRegistracija_Click);

            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 320);
            this.Controls.Add(this.LblTitle);
            this.Controls.Add(this.LblEmail);
            this.Controls.Add(this.TBoxEmail);
            this.Controls.Add(this.LblPass);
            this.Controls.Add(this.TBoxPass);
            this.Controls.Add(this.LblPassOpet);
            this.Controls.Add(this.TBoxPassOpet);
            this.Controls.Add(this.BtnRegistracija);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Registracija";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Registracija";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Label LblTitle;
        private System.Windows.Forms.Label LblEmail;
        private System.Windows.Forms.Label LblPass;
        private System.Windows.Forms.Label LblPassOpet;
        private System.Windows.Forms.TextBox TBoxEmail;
        private System.Windows.Forms.TextBox TBoxPass;
        private System.Windows.Forms.TextBox TBoxPassOpet;
        private System.Windows.Forms.Button BtnRegistracija;
    }
}
