namespace projekat_2026_IvanMarkovic
{
    partial class AdminPage
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.TabControl          = new System.Windows.Forms.TabControl();
            this.TabVojnici          = new System.Windows.Forms.TabPage();
            this.TabHierarchija      = new System.Windows.Forms.TabPage();
            this.TabJedinice         = new System.Windows.Forms.TabPage();
            this.TabKomandanti       = new System.Windows.Forms.TabPage();
            this.TabMobilizacije     = new System.Windows.Forms.TabPage();
            this.TabKorisnici        = new System.Windows.Forms.TabPage();
            this.TabStatistika       = new System.Windows.Forms.TabPage();
            this.BtnLogout           = new System.Windows.Forms.Button();

            this.DGridVojnici        = new System.Windows.Forms.DataGridView();
            this.LblIme              = new System.Windows.Forms.Label();
            this.TBoxIme             = new System.Windows.Forms.TextBox();
            this.LblPrezime          = new System.Windows.Forms.Label();
            this.TBoxPrezime         = new System.Windows.Forms.TextBox();
            this.LblJmbg             = new System.Windows.Forms.Label();
            this.TBoxJmbg            = new System.Windows.Forms.TextBox();
            this.LblCin              = new System.Windows.Forms.Label();
            this.CBoxCin             = new System.Windows.Forms.ComboBox();
            this.LblKorisnikVojnik   = new System.Windows.Forms.Label();
            this.CBoxKorisnikVojnik  = new System.Windows.Forms.ComboBox();
            this.BtnDodajVojnika     = new System.Windows.Forms.Button();
            this.BtnObrisiVojnika    = new System.Windows.Forms.Button();
            this.BtnIzmeniVojnika    = new System.Windows.Forms.Button();

            this.DGridDivizije           = new System.Windows.Forms.DataGridView();
            this.LblDivizije             = new System.Windows.Forms.Label();
            this.LblNazivDivizije        = new System.Windows.Forms.Label();
            this.TBoxNazivDivizije       = new System.Windows.Forms.TextBox();
            this.LblLokacijaDivizije     = new System.Windows.Forms.Label();
            this.TBoxLokacijaDivizije    = new System.Windows.Forms.TextBox();
            this.LblOpisDivizije         = new System.Windows.Forms.Label();
            this.TBoxOpisDivizije        = new System.Windows.Forms.TextBox();
            this.BtnDodajDiviziju        = new System.Windows.Forms.Button();
            this.BtnObrisiDiviziju       = new System.Windows.Forms.Button();
            this.DGridPukovi             = new System.Windows.Forms.DataGridView();
            this.LblPukovi               = new System.Windows.Forms.Label();
            this.LblNazivPuka            = new System.Windows.Forms.Label();
            this.TBoxNazivPuka           = new System.Windows.Forms.TextBox();
            this.LblDivizijaPuk          = new System.Windows.Forms.Label();
            this.CBoxDivizijaPuk         = new System.Windows.Forms.ComboBox();
            this.LblTipPuka              = new System.Windows.Forms.Label();
            this.CBoxTipPuka             = new System.Windows.Forms.ComboBox();
            this.LblLokacijaPuka         = new System.Windows.Forms.Label();
            this.TBoxLokacijaPuka        = new System.Windows.Forms.TextBox();
            this.BtnDodajPuk             = new System.Windows.Forms.Button();
            this.BtnObrisiPuk            = new System.Windows.Forms.Button();

            this.DGridJedinice           = new System.Windows.Forms.DataGridView();
            this.LblNazivJedinice        = new System.Windows.Forms.Label();
            this.TBoxNazivJedinice       = new System.Windows.Forms.TextBox();
            this.LblPukJedinice          = new System.Windows.Forms.Label();
            this.CBoxPukJedinice         = new System.Windows.Forms.ComboBox();
            this.LblLokacijaJedinice     = new System.Windows.Forms.Label();
            this.TBoxLokacijaJedinice    = new System.Windows.Forms.TextBox();
            this.LblKapacitet            = new System.Windows.Forms.Label();
            this.TBoxKapacitet           = new System.Windows.Forms.TextBox();
            this.BtnDodajJedinicu        = new System.Windows.Forms.Button();
            this.BtnIzmeniJedinicu       = new System.Windows.Forms.Button();
            this.BtnObrisiJedinicu       = new System.Windows.Forms.Button();

            this.DGridKomandanti         = new System.Windows.Forms.DataGridView();
            this.LblImeKom               = new System.Windows.Forms.Label();
            this.TBoxImeKom              = new System.Windows.Forms.TextBox();
            this.LblPrezimeKom           = new System.Windows.Forms.Label();
            this.TBoxPrezimeKom          = new System.Windows.Forms.TextBox();
            this.LblCinKomandanta        = new System.Windows.Forms.Label();
            this.CBoxCinKomandanta       = new System.Windows.Forms.ComboBox();
            this.LblJmbgKom              = new System.Windows.Forms.Label();
            this.TBoxJmbgKom             = new System.Windows.Forms.TextBox();
            this.LblKontaktKom           = new System.Windows.Forms.Label();
            this.TBoxKontaktKom          = new System.Windows.Forms.TextBox();
            this.BtnDodajKomandanta      = new System.Windows.Forms.Button();
            this.BtnObrisiKomandanta     = new System.Windows.Forms.Button();
            this.LblDodelaKomandanta     = new System.Windows.Forms.Label();
            this.LblIzaberiKomandanta    = new System.Windows.Forms.Label();
            this.CBoxKomandant           = new System.Windows.Forms.ComboBox();
            this.LblJedinicaKomandanta   = new System.Windows.Forms.Label();
            this.CBoxJedinicaKomandanta  = new System.Windows.Forms.ComboBox();
            this.BtnDodeliKomandanta     = new System.Windows.Forms.Button();
            this.BtnUkloniKomandanta     = new System.Windows.Forms.Button();

            this.DGridMobilizacije       = new System.Windows.Forms.DataGridView();
            this.LblVojnik               = new System.Windows.Forms.Label();
            this.CBoxVojnik              = new System.Windows.Forms.ComboBox();
            this.LblJedinica             = new System.Windows.Forms.Label();
            this.CBoxJedinica            = new System.Windows.Forms.ComboBox();
            this.LblStatus               = new System.Windows.Forms.Label();
            this.CBoxStatus              = new System.Windows.Forms.ComboBox();
            this.BtnRasporedi            = new System.Windows.Forms.Button();
            this.BtnPromeniStatus        = new System.Windows.Forms.Button();

  
            this.DGridKorisnici          = new System.Windows.Forms.DataGridView();
            this.BtnDodajKorisnika       = new System.Windows.Forms.Button();
            this.BtnObrisiKorisnika      = new System.Windows.Forms.Button();
            this.BtnPromovisaj           = new System.Windows.Forms.Button();

            this.DGridStatistika         = new System.Windows.Forms.DataGridView();
            this.BtnOsveziStatistiku     = new System.Windows.Forms.Button();

            this.TabControl.SuspendLayout();
            this.SuspendLayout();

            this.TabControl.Location = new System.Drawing.Point(5, 5);
            this.TabControl.Name = "TabControl";
            this.TabControl.Size = new System.Drawing.Size(1100, 630);
            this.TabControl.TabPages.AddRange(new System.Windows.Forms.TabPage[] {
                this.TabVojnici, this.TabHierarchija, this.TabJedinice,
                this.TabKomandanti, this.TabMobilizacije,
                this.TabKorisnici, this.TabStatistika });

            this.TabVojnici.Text = "Vojnici";
            this.TabVojnici.Padding = new System.Windows.Forms.Padding(5);

            DataGridSetup(this.DGridVojnici, 8, 8, 1075, 380);

            Lbl(this.LblIme,    "Ime:",     8,  398); TBox(this.TBoxIme,    8,  416, 140);
            Lbl(this.LblPrezime,"Prezime:", 160, 398); TBox(this.TBoxPrezime,160, 416, 140);
            Lbl(this.LblJmbg,   "JMBG:",   312, 398); TBox(this.TBoxJmbg,   312, 416, 140);
            this.TBoxJmbg.MaxLength = 13;

            Lbl(this.LblCin, "Cin:", 464, 398);
            this.CBoxCin.Location = new System.Drawing.Point(464, 416);
            this.CBoxCin.Size     = new System.Drawing.Size(160, 22);
            this.CBoxCin.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;

            Lbl(this.LblKorisnikVojnik, "Korisnik nalog:", 636, 398);
            this.CBoxKorisnikVojnik.Location = new System.Drawing.Point(636, 416);
            this.CBoxKorisnikVojnik.Size     = new System.Drawing.Size(200, 22);
            this.CBoxKorisnikVojnik.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;

            Btn(this.BtnDodajVojnika,  "Dodaj vojnika",    8,   455, 130); this.BtnDodajVojnika.Click  += new System.EventHandler(this.BtnDodajVojnika_Click);
            Btn(this.BtnIzmeniVojnika, "Izmeni izabranog", 150, 455, 130); this.BtnIzmeniVojnika.Click += new System.EventHandler(this.BtnIzmeniVojnika_Click);
            Btn(this.BtnObrisiVojnika, "Obrisi izabranog", 292, 455, 130); this.BtnObrisiVojnika.Click += new System.EventHandler(this.BtnObrisiVojnika_Click);

            this.TabVojnici.Controls.AddRange(new System.Windows.Forms.Control[] {
                DGridVojnici, LblIme, TBoxIme, LblPrezime, TBoxPrezime,
                LblJmbg, TBoxJmbg, LblCin, CBoxCin,
                LblKorisnikVojnik, CBoxKorisnikVojnik,
                BtnDodajVojnika, BtnIzmeniVojnika, BtnObrisiVojnika });

            this.TabHierarchija.Text = "Hijerarhija";
            this.TabHierarchija.Padding = new System.Windows.Forms.Padding(5);

            Lbl(this.LblDivizije, "DIVIZIJE:", 8, 8); this.LblDivizije.Font =
                new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);

            DataGridSetup(this.DGridDivizije, 8, 26, 520, 220);

            Lbl(this.LblNazivDivizije,    "Naziv:",    8,  258); TBox(this.TBoxNazivDivizije,    8,  276, 180);
            Lbl(this.LblLokacijaDivizije, "Lokacija:", 200, 258); TBox(this.TBoxLokacijaDivizije, 200, 276, 140);
            Lbl(this.LblOpisDivizije,     "Opis:",     352, 258); TBox(this.TBoxOpisDivizije,     352, 276, 176);

            Btn(this.BtnDodajDiviziju,  "Dodaj diviziju",  8,   312, 130); this.BtnDodajDiviziju.Click  += new System.EventHandler(this.BtnDodajDiviziju_Click);
            Btn(this.BtnObrisiDiviziju, "Obrisi izabranu", 150, 312, 130); this.BtnObrisiDiviziju.Click += new System.EventHandler(this.BtnObrisiDiviziju_Click);

            Lbl(this.LblPukovi, "PUKOVI / REGIMENTI:", 545, 8); this.LblPukovi.Font =
                new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);

            DataGridSetup(this.DGridPukovi, 545, 26, 520, 220);

            Lbl(this.LblNazivPuka,    "Naziv:",    545, 258); TBox(this.TBoxNazivPuka,    545, 276, 160);
            Lbl(this.LblDivizijaPuk,  "Divizija:",  717, 258);
            this.CBoxDivizijaPuk.Location = new System.Drawing.Point(717, 276);
            this.CBoxDivizijaPuk.Size     = new System.Drawing.Size(160, 22);
            this.CBoxDivizijaPuk.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;

            Lbl(this.LblTipPuka,      "Tip:",      889, 258);
            this.CBoxTipPuka.Location = new System.Drawing.Point(889, 276);
            this.CBoxTipPuka.Size     = new System.Drawing.Size(170, 22);
            this.CBoxTipPuka.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;

            Lbl(this.LblLokacijaPuka, "Lokacija:", 545, 308); TBox(this.TBoxLokacijaPuka, 545, 326, 160);

            Btn(this.BtnDodajPuk,  "Dodaj puk",      545, 360, 120); this.BtnDodajPuk.Click  += new System.EventHandler(this.BtnDodajPuk_Click);
            Btn(this.BtnObrisiPuk, "Obrisi izabrani", 678, 360, 130); this.BtnObrisiPuk.Click += new System.EventHandler(this.BtnObrisiPuk_Click);

            this.TabHierarchija.Controls.AddRange(new System.Windows.Forms.Control[] {
                LblDivizije, DGridDivizije,
                LblNazivDivizije, TBoxNazivDivizije,
                LblLokacijaDivizije, TBoxLokacijaDivizije,
                LblOpisDivizije, TBoxOpisDivizije,
                BtnDodajDiviziju, BtnObrisiDiviziju,
                LblPukovi, DGridPukovi,
                LblNazivPuka, TBoxNazivPuka,
                LblDivizijaPuk, CBoxDivizijaPuk,
                LblTipPuka, CBoxTipPuka,
                LblLokacijaPuka, TBoxLokacijaPuka,
                BtnDodajPuk, BtnObrisiPuk });

            this.TabJedinice.Text = "Bataljoni";
            this.TabJedinice.Padding = new System.Windows.Forms.Padding(5);

            DataGridSetup(this.DGridJedinice, 8, 8, 1075, 390);

            Lbl(this.LblNazivJedinice,    "Naziv bataljona:", 8,   408); TBox(this.TBoxNazivJedinice,    8,   426, 200);
            Lbl(this.LblPukJedinice,      "Puk:",             220, 408);
            this.CBoxPukJedinice.Location = new System.Drawing.Point(220, 426);
            this.CBoxPukJedinice.Size     = new System.Drawing.Size(200, 22);
            this.CBoxPukJedinice.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;

            Lbl(this.LblLokacijaJedinice, "Lokacija:",  432, 408); TBox(this.TBoxLokacijaJedinice, 432, 426, 150);
            Lbl(this.LblKapacitet,        "Kapacitet:", 594, 408); TBox(this.TBoxKapacitet,        594, 426, 80);

            Btn(this.BtnDodajJedinicu,  "Dodaj bataljon",   8,   465, 140); this.BtnDodajJedinicu.Click  += new System.EventHandler(this.BtnDodajJedinicu_Click);
            Btn(this.BtnIzmeniJedinicu, "Izmeni izabrani",  160, 465, 140); this.BtnIzmeniJedinicu.Click += new System.EventHandler(this.BtnIzmeniJedinicu_Click);
            Btn(this.BtnObrisiJedinicu, "Obrisi izabrani",  312, 465, 140); this.BtnObrisiJedinicu.Click += new System.EventHandler(this.BtnObrisiJedinicu_Click);

            this.TabJedinice.Controls.AddRange(new System.Windows.Forms.Control[] {
                DGridJedinice,
                LblNazivJedinice, TBoxNazivJedinice,
                LblPukJedinice, CBoxPukJedinice,
                LblLokacijaJedinice, TBoxLokacijaJedinice,
                LblKapacitet, TBoxKapacitet,
                BtnDodajJedinicu, BtnIzmeniJedinicu, BtnObrisiJedinicu });

            this.TabKomandanti.Text = "Komandanti";
            this.TabKomandanti.Padding = new System.Windows.Forms.Padding(5);

            DataGridSetup(this.DGridKomandanti, 8, 8, 1075, 330);
            Lbl(this.LblImeKom,     "Ime:",      8,   350); TBox(this.TBoxImeKom,     8,   368, 130);
            Lbl(this.LblPrezimeKom, "Prezime:",  150, 350); TBox(this.TBoxPrezimeKom, 150, 368, 130);
            Lbl(this.LblCinKomandanta, "Cin:",   292, 350);
            this.CBoxCinKomandanta.Location = new System.Drawing.Point(292, 368);
            this.CBoxCinKomandanta.Size     = new System.Drawing.Size(180, 22);
            this.CBoxCinKomandanta.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            Lbl(this.LblJmbgKom,    "JMBG:",    484, 350); TBox(this.TBoxJmbgKom,    484, 368, 130);
            this.TBoxJmbgKom.MaxLength = 13;
            Lbl(this.LblKontaktKom, "Kontakt:", 626, 350); TBox(this.TBoxKontaktKom, 626, 368, 200);

            Btn(this.BtnDodajKomandanta,  "Dodaj komandanta",  8,   408, 150); this.BtnDodajKomandanta.Click  += new System.EventHandler(this.BtnDodajKomandanta_Click);
            Btn(this.BtnObrisiKomandanta, "Obrisi izabranog",  170, 408, 150); this.BtnObrisiKomandanta.Click += new System.EventHandler(this.BtnObrisiKomandanta_Click);

            Lbl(this.LblDodelaKomandanta, "── DODELA KOMANDANTA BATALJONU ──", 8, 450);
            this.LblDodelaKomandanta.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.LblDodelaKomandanta.AutoSize = true;

            Lbl(this.LblIzaberiKomandanta, "Komandant:", 8, 475);
            this.CBoxKomandant.Location = new System.Drawing.Point(8, 493);
            this.CBoxKomandant.Size     = new System.Drawing.Size(260, 22);
            this.CBoxKomandant.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;

            Lbl(this.LblJedinicaKomandanta, "Bataljon:", 280, 475);
            this.CBoxJedinicaKomandanta.Location = new System.Drawing.Point(280, 493);
            this.CBoxJedinicaKomandanta.Size     = new System.Drawing.Size(300, 22);
            this.CBoxJedinicaKomandanta.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;

            Btn(this.BtnDodeliKomandanta, "Dodeli komandanta", 592, 489, 160);
            this.BtnDodeliKomandanta.Click += new System.EventHandler(this.BtnDodeliKomandanta_Click);

            Btn(this.BtnUkloniKomandanta, "Ukloni komandanta", 764, 489, 160);
            this.BtnUkloniKomandanta.Click += new System.EventHandler(this.BtnUkloniKomandanta_Click);

            this.TabKomandanti.Controls.AddRange(new System.Windows.Forms.Control[] {
                DGridKomandanti,
                LblImeKom, TBoxImeKom, LblPrezimeKom, TBoxPrezimeKom,
                LblCinKomandanta, CBoxCinKomandanta,
                LblJmbgKom, TBoxJmbgKom, LblKontaktKom, TBoxKontaktKom,
                BtnDodajKomandanta, BtnObrisiKomandanta,
                LblDodelaKomandanta,
                LblIzaberiKomandanta, CBoxKomandant,
                LblJedinicaKomandanta, CBoxJedinicaKomandanta,
                BtnDodeliKomandanta, BtnUkloniKomandanta });

            this.TabMobilizacije.Text = "Mobilizacije";
            this.TabMobilizacije.Padding = new System.Windows.Forms.Padding(5);

            DataGridSetup(this.DGridMobilizacije, 8, 8, 1075, 400);

            Lbl(this.LblVojnik,  "Vojnik:",  8,   420);
            this.CBoxVojnik.Location = new System.Drawing.Point(8, 438);
            this.CBoxVojnik.Size     = new System.Drawing.Size(250, 22);
            this.CBoxVojnik.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;

            Lbl(this.LblJedinica, "Bataljon:", 270, 420);
            this.CBoxJedinica.Location = new System.Drawing.Point(270, 438);
            this.CBoxJedinica.Size     = new System.Drawing.Size(300, 22);
            this.CBoxJedinica.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;

            Lbl(this.LblStatus, "Status:", 582, 420);
            this.CBoxStatus.Location = new System.Drawing.Point(582, 438);
            this.CBoxStatus.Size     = new System.Drawing.Size(130, 22);
            this.CBoxStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CBoxStatus.Items.AddRange(new object[] { "Aktivan", "Rezerva", "Otpusten" });
            this.CBoxStatus.SelectedIndex = 0;

            Btn(this.BtnRasporedi,     "Rasporedi vojnika", 8,   475, 160); this.BtnRasporedi.Click     += new System.EventHandler(this.BtnRasporedi_Click);
            Btn(this.BtnPromeniStatus, "Promeni status",    180, 475, 150); this.BtnPromeniStatus.Click += new System.EventHandler(this.BtnPromeniStatus_Click);

            this.TabMobilizacije.Controls.AddRange(new System.Windows.Forms.Control[] {
                DGridMobilizacije,
                LblVojnik, CBoxVojnik, LblJedinica, CBoxJedinica,
                LblStatus, CBoxStatus, BtnRasporedi, BtnPromeniStatus });

            this.TabKorisnici.Text = "Korisnici";
            this.TabKorisnici.Padding = new System.Windows.Forms.Padding(5);

            DataGridSetup(this.DGridKorisnici, 8, 8, 1075, 490);

            Btn(this.BtnDodajKorisnika,  "Dodaj korisnika",           8,   510, 160); this.BtnDodajKorisnika.Click  += new System.EventHandler(this.BtnDodajKorisnika_Click);
            Btn(this.BtnObrisiKorisnika, "Obrisi korisnika",          180, 510, 160); this.BtnObrisiKorisnika.Click += new System.EventHandler(this.BtnObrisiKorisnika_Click);
            Btn(this.BtnPromovisaj,      "Promeni ulogu Admin/Korisnik", 352, 510, 220); this.BtnPromovisaj.Click      += new System.EventHandler(this.BtnPromovisaj_Click);

            this.TabKorisnici.Controls.AddRange(new System.Windows.Forms.Control[] {
                DGridKorisnici, BtnDodajKorisnika, BtnObrisiKorisnika, BtnPromovisaj });

            this.TabStatistika.Text = "Statistika";
            this.TabStatistika.Padding = new System.Windows.Forms.Padding(5);

            DataGridSetup(this.DGridStatistika, 8, 8, 1075, 540);

            Btn(this.BtnOsveziStatistiku, "Osvezi", 8, 560, 120);
            this.BtnOsveziStatistiku.Click += new System.EventHandler((s, e) => UpdateStatistika());

            this.TabStatistika.Controls.AddRange(new System.Windows.Forms.Control[] {
                DGridStatistika, BtnOsveziStatistiku });

            this.BtnLogout.Text = "Odjavi se";
            this.BtnLogout.Location = new System.Drawing.Point(995, 642);
            this.BtnLogout.Size     = new System.Drawing.Size(110, 30);
            this.BtnLogout.Click   += new System.EventHandler(this.BtnLogout_Click);

            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize    = new System.Drawing.Size(1115, 680);
            this.Controls.Add(this.TabControl);
            this.Controls.Add(this.BtnLogout);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox     = false;
            this.Name            = "AdminPage";
            this.StartPosition   = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text            = "Admin Panel - Mobilizacija";
            this.Load           += new System.EventHandler(this.AdminPage_Load);
            this.FormClosing    += new System.Windows.Forms.FormClosingEventHandler(this.AdminPage_FormClosing);

            this.TabControl.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        private static void DataGridSetup(System.Windows.Forms.DataGridView g, int x, int y, int w, int h)
        {
            g.Location = new System.Drawing.Point(x, y);
            g.Size     = new System.Drawing.Size(w, h);
            g.ReadOnly = true;
            g.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            g.AllowUserToAddRows = false;
            g.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
        }
        private static void Lbl(System.Windows.Forms.Label l, string text, int x, int y)
        {
            l.Text = text; l.Location = new System.Drawing.Point(x, y); l.AutoSize = true;
        }
        private static void TBox(System.Windows.Forms.TextBox t, int x, int y, int w)
        {
            t.Location = new System.Drawing.Point(x, y); t.Size = new System.Drawing.Size(w, 22);
        }
        private static void Btn(System.Windows.Forms.Button b, string text, int x, int y, int w)
        {
            b.Text = text; b.Location = new System.Drawing.Point(x, y);
            b.Size = new System.Drawing.Size(w, 30);
        }

        private System.Windows.Forms.TabControl TabControl;
        private System.Windows.Forms.TabPage TabVojnici, TabHierarchija, TabJedinice,
            TabKomandanti, TabMobilizacije, TabKorisnici, TabStatistika;
        private System.Windows.Forms.Button BtnLogout;

        private System.Windows.Forms.DataGridView DGridVojnici;
        private System.Windows.Forms.Label LblIme, LblPrezime, LblJmbg, LblCin, LblKorisnikVojnik;
        private System.Windows.Forms.TextBox TBoxIme, TBoxPrezime, TBoxJmbg;
        private System.Windows.Forms.ComboBox CBoxCin, CBoxKorisnikVojnik;
        private System.Windows.Forms.Button BtnDodajVojnika, BtnObrisiVojnika, BtnIzmeniVojnika;

        private System.Windows.Forms.DataGridView DGridDivizije, DGridPukovi;
        private System.Windows.Forms.Label LblDivizije, LblNazivDivizije, LblLokacijaDivizije, LblOpisDivizije;
        private System.Windows.Forms.TextBox TBoxNazivDivizije, TBoxLokacijaDivizije, TBoxOpisDivizije;
        private System.Windows.Forms.Button BtnDodajDiviziju, BtnObrisiDiviziju;
        private System.Windows.Forms.Label LblPukovi, LblNazivPuka, LblDivizijaPuk, LblTipPuka, LblLokacijaPuka;
        private System.Windows.Forms.TextBox TBoxNazivPuka, TBoxLokacijaPuka;
        private System.Windows.Forms.ComboBox CBoxDivizijaPuk, CBoxTipPuka;
        private System.Windows.Forms.Button BtnDodajPuk, BtnObrisiPuk;

        private System.Windows.Forms.DataGridView DGridJedinice;
        private System.Windows.Forms.Label LblNazivJedinice, LblPukJedinice, LblLokacijaJedinice, LblKapacitet;
        private System.Windows.Forms.TextBox TBoxNazivJedinice, TBoxLokacijaJedinice, TBoxKapacitet;
        private System.Windows.Forms.ComboBox CBoxPukJedinice;
        private System.Windows.Forms.Button BtnDodajJedinicu, BtnIzmeniJedinicu, BtnObrisiJedinicu;

        private System.Windows.Forms.DataGridView DGridKomandanti;
        private System.Windows.Forms.Label LblImeKom, LblPrezimeKom, LblCinKomandanta, LblJmbgKom, LblKontaktKom;
        private System.Windows.Forms.TextBox TBoxImeKom, TBoxPrezimeKom, TBoxJmbgKom, TBoxKontaktKom;
        private System.Windows.Forms.ComboBox CBoxCinKomandanta;
        private System.Windows.Forms.Button BtnDodajKomandanta, BtnObrisiKomandanta;
        private System.Windows.Forms.Label LblDodelaKomandanta, LblIzaberiKomandanta, LblJedinicaKomandanta;
        private System.Windows.Forms.ComboBox CBoxKomandant, CBoxJedinicaKomandanta;
        private System.Windows.Forms.Button BtnDodeliKomandanta, BtnUkloniKomandanta;

        private System.Windows.Forms.DataGridView DGridMobilizacije;
        private System.Windows.Forms.Label LblVojnik, LblJedinica, LblStatus;
        private System.Windows.Forms.ComboBox CBoxVojnik, CBoxJedinica, CBoxStatus;
        private System.Windows.Forms.Button BtnRasporedi, BtnPromeniStatus;

        private System.Windows.Forms.DataGridView DGridKorisnici;
        private System.Windows.Forms.Button BtnDodajKorisnika, BtnObrisiKorisnika, BtnPromovisaj;

        private System.Windows.Forms.DataGridView DGridStatistika;
        private System.Windows.Forms.Button BtnOsveziStatistiku;
    }
}
