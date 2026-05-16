namespace projekat_2026_IvanMarkovic
{
    partial class GlavnaKorisnik
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.LblTitle      = new System.Windows.Forms.Label();
            this.LblJedinice   = new System.Windows.Forms.Label();
            this.DGridJedinice = new System.Windows.Forms.DataGridView();
            this.LblMojVojnik  = new System.Windows.Forms.Label();
            this.DGridMojVojnik = new System.Windows.Forms.DataGridView();
            this.BtnOsvezi     = new System.Windows.Forms.Button();
            this.BtnLogout     = new System.Windows.Forms.Button();
            this.SuspendLayout();

            this.LblTitle.AutoSize = true;
            this.LblTitle.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.LblTitle.Location = new System.Drawing.Point(12, 10);
            this.LblTitle.Text = "Vojska Srbije — Pregled Mobilizacije";

            this.LblJedinice.AutoSize = true;
            this.LblJedinice.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.LblJedinice.Location = new System.Drawing.Point(12, 48);
            this.LblJedinice.Text = "Sve jedinice — divizija / puk / bataljon:";

            this.DGridJedinice.Location = new System.Drawing.Point(12, 70);
            this.DGridJedinice.Name = "DGridJedinice";
            this.DGridJedinice.Size = new System.Drawing.Size(960, 260);
            this.DGridJedinice.ReadOnly = true;
            this.DGridJedinice.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DGridJedinice.AllowUserToAddRows = false;
            this.DGridJedinice.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;

            this.LblMojVojnik.AutoSize = true;
            this.LblMojVojnik.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.LblMojVojnik.Location = new System.Drawing.Point(12, 345);
            this.LblMojVojnik.Name = "LblMojVojnik";
            this.LblMojVojnik.Text = "Moj vojnicki profil i status:";

            this.DGridMojVojnik.Location = new System.Drawing.Point(12, 367);
            this.DGridMojVojnik.Name = "DGridMojVojnik";
            this.DGridMojVojnik.Size = new System.Drawing.Size(960, 160);
            this.DGridMojVojnik.ReadOnly = true;
            this.DGridMojVojnik.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DGridMojVojnik.AllowUserToAddRows = false;
            this.DGridMojVojnik.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;

            this.BtnOsvezi.Text = "Osvezi";
            this.BtnOsvezi.Location = new System.Drawing.Point(12, 542);
            this.BtnOsvezi.Size = new System.Drawing.Size(100, 30);
            this.BtnOsvezi.Click += new System.EventHandler(this.BtnOsvezi_Click);

            this.BtnLogout.Text = "Odjavi se";
            this.BtnLogout.Location = new System.Drawing.Point(872, 542);
            this.BtnLogout.Size = new System.Drawing.Size(100, 30);
            this.BtnLogout.Click += new System.EventHandler(this.BtnLogout_Click);

            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(988, 588);
            this.Controls.Add(this.LblTitle);
            this.Controls.Add(this.LblJedinice);
            this.Controls.Add(this.DGridJedinice);
            this.Controls.Add(this.LblMojVojnik);
            this.Controls.Add(this.DGridMojVojnik);
            this.Controls.Add(this.BtnOsvezi);
            this.Controls.Add(this.BtnLogout);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "GlavnaKorisnik";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Korisnik - Mobilizacija";
            this.Load     += new System.EventHandler(this.GlavnaKorisnik_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.GlavnaKorisnik_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Label LblTitle;
        private System.Windows.Forms.Label LblJedinice;
        private System.Windows.Forms.DataGridView DGridJedinice;
        private System.Windows.Forms.Label LblMojVojnik;
        private System.Windows.Forms.DataGridView DGridMojVojnik;
        private System.Windows.Forms.Button BtnOsvezi;
        private System.Windows.Forms.Button BtnLogout;
    }
}
