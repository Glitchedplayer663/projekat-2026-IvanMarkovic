using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace projekat_2026_IvanMarkovic
{
    public partial class AdminPage : Form
    {
        private bool ExitApp = true;
        private Form parent;
        private int KorisnikId;

        DataTable tabelaVojnici    = new DataTable();
        DataTable tabelaJedinice   = new DataTable();
        DataTable tabelaKomandanti = new DataTable();
        DataTable tabelaMobilizacije = new DataTable();
        DataTable tabelaKorisnici  = new DataTable();
        DataTable tabelaStatistika = new DataTable();
        DataTable tabelaDivizije   = new DataTable();
        DataTable tabelaPukovi     = new DataTable();

        public AdminPage(int korisnikId, Form parent)
        {
            InitializeComponent();
            this.KorisnikId = korisnikId;
            this.parent = parent;
        }


        private void AdminPage_Load(object sender, EventArgs e)
        {
            PopuniCinove();
            PopuniTipoveJedinica();
            UpdateDivizije();
            UpdatePukovi();
            UpdateJedinice();
            UpdateVojnici();
            UpdateKomandanti();
            UpdateMobilizacije();
            UpdateKorisnici();
            UpdateStatistika();
            PopuniComboVojnici();
            PopuniComboJedinice();
            PopuniComboKomandanti();
        }

        private void PopuniCinove()
        {
            DataTable dt = new DataTable();
            new SqlDataAdapter("SELECT cin_id, naziv_cina, nivo FROM Cin ORDER BY nivo",
                Connection.Connect()).Fill(dt);
            CBoxCin.DataSource = dt.Copy();
            CBoxCin.DisplayMember = "naziv_cina";
            CBoxCin.ValueMember = "cin_id";
            CBoxCinKomandanta.DataSource = dt.Copy();
            CBoxCinKomandanta.DisplayMember = "naziv_cina";
            CBoxCinKomandanta.ValueMember = "cin_id";
        }

        private void PopuniTipoveJedinica()
        {
            DataTable dt = new DataTable();
            new SqlDataAdapter("SELECT tip_id, naziv_tipa FROM TipJedinice ORDER BY naziv_tipa",
                Connection.Connect()).Fill(dt);
            CBoxTipPuka.DataSource = dt;
            CBoxTipPuka.DisplayMember = "naziv_tipa";
            CBoxTipPuka.ValueMember = "tip_id";
        }

        private void PopuniComboVojnici()
        {
            DataTable dt = new DataTable();
            new SqlDataAdapter(
                "SELECT vojnik_id, ime + ' ' + prezime AS ime_prezime FROM Vojnik ORDER BY prezime",
                Connection.Connect()).Fill(dt);
            CBoxVojnik.DataSource = dt;
            CBoxVojnik.DisplayMember = "ime_prezime";
            CBoxVojnik.ValueMember = "vojnik_id";
        }

        private void PopuniComboJedinice()
        {
            DataTable dt = new DataTable();
            new SqlDataAdapter(
                @"SELECT J.jedinica_id, J.naziv_jedinice + ' (' + P.naziv_puka + ')' AS prikaz
                  FROM Jedinica J JOIN Puk P ON J.puk_id = P.puk_id ORDER BY J.naziv_jedinice",
                Connection.Connect()).Fill(dt);
            CBoxJedinica.DataSource = dt.Copy();
            CBoxJedinica.DisplayMember = "prikaz";
            CBoxJedinica.ValueMember = "jedinica_id";
            CBoxJedinicaKomandanta.DataSource = dt.Copy();
            CBoxJedinicaKomandanta.DisplayMember = "prikaz";
            CBoxJedinicaKomandanta.ValueMember = "jedinica_id";

            DataTable dtKor = new DataTable();
            new SqlDataAdapter(
                @"SELECT K.korisnik_id, K.email
                  FROM Korisnik K
                  JOIN Uloga U ON K.uloga_id = U.uloga_id
                  WHERE U.naziv_uloga = 'Korisnik'
                  ORDER BY K.email",
                Connection.Connect()).Fill(dtKor);
            DataRow prazan = dtKor.NewRow();
            prazan["korisnik_id"] = DBNull.Value;
            prazan["email"] = "(bez naloga)";
            dtKor.Rows.InsertAt(prazan, 0);
            CBoxKorisnikVojnik.DataSource = dtKor;
            CBoxKorisnikVojnik.DisplayMember = "email";
            CBoxKorisnikVojnik.ValueMember = "korisnik_id";
        }

        private void PopuniComboKomandanti()
        {
            DataTable dt = new DataTable();
            new SqlDataAdapter(
                "SELECT komandant_id, ime + ' ' + prezime AS ime_prezime FROM Komandant ORDER BY prezime",
                Connection.Connect()).Fill(dt);
            CBoxKomandant.DataSource = dt;
            CBoxKomandant.DisplayMember = "ime_prezime";
            CBoxKomandant.ValueMember = "komandant_id";
        }

        private void PopuniComboDivizije()
        {
            DataTable dt = new DataTable();
            new SqlDataAdapter("SELECT divizija_id, naziv_divizije FROM Divizija ORDER BY naziv_divizije",
                Connection.Connect()).Fill(dt);
            CBoxDivizijaPuk.DataSource = dt.Copy();
            CBoxDivizijaPuk.DisplayMember = "naziv_divizije";
            CBoxDivizijaPuk.ValueMember = "divizija_id";
        }

        private void UpdateVojnici()
        {
            tabelaVojnici = new DataTable();
            try
            {
                new SqlDataAdapter(
                    @"SELECT V.vojnik_id, V.ime, V.prezime, V.jmbg,
                             C.naziv_cina AS cin,
                             K.email AS nalog
                      FROM Vojnik V
                      JOIN Cin C ON V.cin_id = C.cin_id
                      LEFT JOIN Korisnik K ON V.korisnik_id = K.korisnik_id
                      ORDER BY V.prezime",
                    Connection.Connect()).Fill(tabelaVojnici);
                DGridVojnici.DataSource = tabelaVojnici;
                if (DGridVojnici.Columns.Contains("vojnik_id"))
                    DGridVojnici.Columns["vojnik_id"].Visible = false;
            }
            catch (Exception ex) { MessageBox.Show("Greska pri ucitavanju vojnika: " + ex.Message); }
        }

        private void BtnDodajVojnika_Click(object sender, EventArgs e)
        {
            if (TBoxIme.Text == "" || TBoxPrezime.Text == "" || TBoxJmbg.Text == "")
            { MessageBox.Show("Ime, prezime i JMBG su obavezni."); return; }
            if (TBoxJmbg.Text.Length != 13)
            { MessageBox.Show("JMBG mora imati tacno 13 cifara."); return; }
            if (CBoxCin.SelectedValue == null)
            { MessageBox.Show("Izaberite cin vojnika."); return; }

            int cinId = Convert.ToInt32(CBoxCin.SelectedValue);
            object korisnikIdParam = DBNull.Value;
            if (CBoxKorisnikVojnik.SelectedValue != null &&
                CBoxKorisnikVojnik.SelectedValue != DBNull.Value &&
                CBoxKorisnikVojnik.SelectedValue.ToString() != "")
            {
                int parsed;
                if (int.TryParse(CBoxKorisnikVojnik.SelectedValue.ToString(), out parsed))
                    korisnikIdParam = parsed;
            }

            SqlConnection veza = Connection.Connect();
            SqlCommand cmd = new SqlCommand("Unos_Vojnika", veza);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ime", TBoxIme.Text);
            cmd.Parameters.AddWithValue("@prezime", TBoxPrezime.Text);
            cmd.Parameters.AddWithValue("@jmbg", TBoxJmbg.Text);
            cmd.Parameters.AddWithValue("@cin_id", cinId);
            cmd.Parameters.AddWithValue("@korisnik_id", korisnikIdParam);
            var ret = cmd.Parameters.Add("@ReturnVal", SqlDbType.Int);
            ret.Direction = ParameterDirection.ReturnValue;
            try
            {
                veza.Open(); cmd.ExecuteNonQuery(); veza.Close();
                if ((int)ret.Value == 1)
                    MessageBox.Show("Vojnik sa tim JMBG-om vec postoji.");
                else
                {
                    MessageBox.Show("Vojnik je uspesno dodat.");
                    TBoxIme.Clear(); TBoxPrezime.Clear(); TBoxJmbg.Clear();
                    UpdateVojnici(); PopuniComboVojnici();
                }
            }
            catch (Exception ex) { MessageBox.Show("Greska: " + ex.Message); veza.Close(); }
        }

        private void BtnObrisiVojnika_Click(object sender, EventArgs e)
        {
            if (DGridVojnici.CurrentRow == null) return;
            if (MessageBox.Show("Obrisati ovog vojnika?", "Potvrda",
                MessageBoxButtons.YesNo) != DialogResult.Yes) return;

            int id = Convert.ToInt32(DGridVojnici.CurrentRow.Cells["vojnik_id"].Value);
            SqlConnection veza = Connection.Connect();
            SqlCommand cmd = new SqlCommand(
                "DELETE FROM Mobilizacija WHERE vojnik_id=@id; DELETE FROM Vojnik WHERE vojnik_id=@id", veza);
            cmd.Parameters.AddWithValue("@id", id);
            try
            {
                veza.Open(); cmd.ExecuteNonQuery(); veza.Close();
                MessageBox.Show("Vojnik je obrisan.");
                UpdateVojnici(); UpdateMobilizacije(); PopuniComboVojnici();
            }
            catch (Exception ex) { MessageBox.Show("Greska: " + ex.Message); veza.Close(); }
        }

        private void BtnIzmeniVojnika_Click(object sender, EventArgs e)
        {
            if (DGridVojnici.CurrentRow == null) return;
            int id = Convert.ToInt32(DGridVojnici.CurrentRow.Cells["vojnik_id"].Value);

            string ime = InputBoxDialog.Show("Novo ime:", "Izmena vojnika",
                DGridVojnici.CurrentRow.Cells["ime"].Value?.ToString() ?? "");
            if (ime == null) return;
            string prezime = InputBoxDialog.Show("Novo prezime:", "Izmena vojnika",
                DGridVojnici.CurrentRow.Cells["prezime"].Value?.ToString() ?? "");
            if (prezime == null) return;

            DataTable dtCin = new DataTable();
            new SqlDataAdapter("SELECT cin_id, naziv_cina FROM Cin ORDER BY nivo",
                Connection.Connect()).Fill(dtCin);
            string cinLista = "Dostupni cinovi:\n";
            foreach (DataRow r in dtCin.Rows)
                cinLista += r["cin_id"] + " - " + r["naziv_cina"] + "\n";

            string cinIdStr = InputBoxDialog.Show(cinLista + "\nUnesite ID cina:", "Izmena vojnika", "");
            if (cinIdStr == null) return;
            int cinId;
            if (!int.TryParse(cinIdStr, out cinId)) { MessageBox.Show("Neispravan ID cina."); return; }

            SqlConnection veza = Connection.Connect();
            SqlCommand cmd = new SqlCommand("Izmena_Vojnika", veza);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@vojnik_id", id);
            cmd.Parameters.AddWithValue("@ime", ime);
            cmd.Parameters.AddWithValue("@prezime", prezime);
            cmd.Parameters.AddWithValue("@cin_id", cinId);
            try
            {
                veza.Open(); cmd.ExecuteNonQuery(); veza.Close();
                MessageBox.Show("Vojnik je izmenjen.");
                UpdateVojnici(); PopuniComboVojnici();
            }
            catch (Exception ex) { MessageBox.Show("Greska: " + ex.Message); veza.Close(); }
        }

        private void UpdateDivizije()
        {
            tabelaDivizije = new DataTable();
            new SqlDataAdapter("SELECT divizija_id, naziv_divizije, lokacija, opis FROM Divizija ORDER BY naziv_divizije",
                Connection.Connect()).Fill(tabelaDivizije);
            DGridDivizije.DataSource = tabelaDivizije;
            if (DGridDivizije.Columns.Contains("divizija_id"))
                DGridDivizije.Columns["divizija_id"].Visible = false;
            PopuniComboDivizije();
        }

        private void UpdatePukovi()
        {
            tabelaPukovi = new DataTable();
            new SqlDataAdapter(
                @"SELECT P.puk_id, P.naziv_puka, D.naziv_divizije AS divizija,
                         T.naziv_tipa AS tip, P.lokacija
                  FROM Puk P
                  JOIN Divizija D ON P.divizija_id = D.divizija_id
                  JOIN TipJedinice T ON P.tip_id = T.tip_id
                  ORDER BY D.naziv_divizije, P.naziv_puka",
                Connection.Connect()).Fill(tabelaPukovi);
            DGridPukovi.DataSource = tabelaPukovi;
            if (DGridPukovi.Columns.Contains("puk_id"))
                DGridPukovi.Columns["puk_id"].Visible = false;
        }

        private void BtnDodajDiviziju_Click(object sender, EventArgs e)
        {
            if (TBoxNazivDivizije.Text == "") { MessageBox.Show("Naziv divizije je obavezan."); return; }
            SqlConnection veza = Connection.Connect();
            SqlCommand cmd = new SqlCommand("Unos_Divizije", veza);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@naziv", TBoxNazivDivizije.Text);
            cmd.Parameters.AddWithValue("@lokacija", TBoxLokacijaDivizije.Text);
            cmd.Parameters.AddWithValue("@opis", TBoxOpisDivizije.Text);
            var ret = cmd.Parameters.Add("@ReturnVal", SqlDbType.Int);
            ret.Direction = ParameterDirection.ReturnValue;
            try
            {
                veza.Open(); cmd.ExecuteNonQuery(); veza.Close();
                if ((int)ret.Value == 1)
                    MessageBox.Show("Divizija sa tim nazivom vec postoji.");
                else
                {
                    MessageBox.Show("Divizija je dodata.");
                    TBoxNazivDivizije.Clear(); TBoxLokacijaDivizije.Clear(); TBoxOpisDivizije.Clear();
                    UpdateDivizije(); PopuniComboJedinice();
                }
            }
            catch (Exception ex) { MessageBox.Show("Greska: " + ex.Message); veza.Close(); }
        }

        private void BtnObrisiDiviziju_Click(object sender, EventArgs e)
        {
            if (DGridDivizije.CurrentRow == null) return;
            int id = Convert.ToInt32(DGridDivizije.CurrentRow.Cells["divizija_id"].Value);
            if (MessageBox.Show("Obrisati ovu diviziju? (mora biti bez pukova)",
                "Potvrda", MessageBoxButtons.YesNo) != DialogResult.Yes) return;

            SqlConnection veza = Connection.Connect();
            SqlCommand cmd = new SqlCommand("Brisanje_Divizije", veza);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@divizija_id", id);
            var ret = cmd.Parameters.Add("@ReturnVal", SqlDbType.Int);
            ret.Direction = ParameterDirection.ReturnValue;
            try
            {
                veza.Open(); cmd.ExecuteNonQuery(); veza.Close();
                if ((int)ret.Value == 0)
                    MessageBox.Show("Divizija ima pukove i ne moze biti obrisana.");
                else { MessageBox.Show("Divizija je obrisana."); UpdateDivizije(); }
            }
            catch (Exception ex) { MessageBox.Show("Greska: " + ex.Message); veza.Close(); }
        }

        private void BtnDodajPuk_Click(object sender, EventArgs e)
        {
            if (TBoxNazivPuka.Text == "") { MessageBox.Show("Naziv puka je obavezan."); return; }
            if (CBoxDivizijaPuk.SelectedValue == null) { MessageBox.Show("Izaberite diviziju."); return; }
            if (CBoxTipPuka.SelectedValue == null) { MessageBox.Show("Izaberite tip jedinice."); return; }

            SqlConnection veza = Connection.Connect();
            SqlCommand cmd = new SqlCommand("Unos_Puka", veza);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@naziv", TBoxNazivPuka.Text);
            cmd.Parameters.AddWithValue("@divizija_id", Convert.ToInt32(CBoxDivizijaPuk.SelectedValue));
            cmd.Parameters.AddWithValue("@tip_id", Convert.ToInt32(CBoxTipPuka.SelectedValue));
            cmd.Parameters.AddWithValue("@lokacija", TBoxLokacijaPuka.Text);
            try
            {
                veza.Open(); cmd.ExecuteNonQuery(); veza.Close();
                MessageBox.Show("Puk je dodat.");
                TBoxNazivPuka.Clear(); TBoxLokacijaPuka.Clear();
                UpdatePukovi(); PopuniComboJedinice();
            }
            catch (Exception ex) { MessageBox.Show("Greska: " + ex.Message); veza.Close(); }
        }

        private void BtnObrisiPuk_Click(object sender, EventArgs e)
        {
            if (DGridPukovi.CurrentRow == null) return;
            int id = Convert.ToInt32(DGridPukovi.CurrentRow.Cells["puk_id"].Value);
            if (MessageBox.Show("Obrisati ovaj puk? (mora biti bez bataljona)",
                "Potvrda", MessageBoxButtons.YesNo) != DialogResult.Yes) return;

            SqlConnection veza = Connection.Connect();
            SqlCommand cmd = new SqlCommand("Brisanje_Puka", veza);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@puk_id", id);
            var ret = cmd.Parameters.Add("@ReturnVal", SqlDbType.Int);
            ret.Direction = ParameterDirection.ReturnValue;
            try
            {
                veza.Open(); cmd.ExecuteNonQuery(); veza.Close();
                if ((int)ret.Value == 0)
                    MessageBox.Show("Puk ima bataljone i ne moze biti obrisan.");
                else { MessageBox.Show("Puk je obrisan."); UpdatePukovi(); PopuniComboJedinice(); }
            }
            catch (Exception ex) { MessageBox.Show("Greska: " + ex.Message); veza.Close(); }
        }


        private void UpdateJedinice()
        {
            tabelaJedinice = new DataTable();
            new SqlDataAdapter(
                @"SELECT J.jedinica_id, J.naziv_jedinice AS bataljon,
                         P.naziv_puka AS puk, T.naziv_tipa AS tip,
                         D.naziv_divizije AS divizija, J.lokacija,
                         J.maks_kapacitet AS kapacitet,
                         ISNULL(KOM.ime + ' ' + KOM.prezime, '—') AS komandant,
                         ISNULL(C.naziv_cina, '—') AS cin_komandanta
                  FROM Jedinica J
                  JOIN Puk P ON J.puk_id = P.puk_id
                  JOIN TipJedinice T ON P.tip_id = T.tip_id
                  JOIN Divizija D ON P.divizija_id = D.divizija_id
                  LEFT JOIN Komandant KOM ON J.komandant_id = KOM.komandant_id
                  LEFT JOIN Cin C ON KOM.cin_id = C.cin_id
                  ORDER BY D.naziv_divizije, P.naziv_puka, J.naziv_jedinice",
                Connection.Connect()).Fill(tabelaJedinice);
            DGridJedinice.DataSource = tabelaJedinice;
            if (DGridJedinice.Columns.Contains("jedinica_id"))
                DGridJedinice.Columns["jedinica_id"].Visible = false;

            DataTable dtPuk = new DataTable();
            new SqlDataAdapter("SELECT puk_id, naziv_puka FROM Puk ORDER BY naziv_puka",
                Connection.Connect()).Fill(dtPuk);
            CBoxPukJedinice.DataSource = dtPuk;
            CBoxPukJedinice.DisplayMember = "naziv_puka";
            CBoxPukJedinice.ValueMember = "puk_id";
        }

        private void BtnDodajJedinicu_Click(object sender, EventArgs e)
        {
            if (TBoxNazivJedinice.Text == "") { MessageBox.Show("Naziv bataljona je obavezan."); return; }
            if (!int.TryParse(TBoxKapacitet.Text, out int kap) || kap <= 0)
            { MessageBox.Show("Kapacitet mora biti pozitivan broj."); return; }
            if (CBoxPukJedinice.SelectedValue == null) { MessageBox.Show("Izaberite puk."); return; }

            SqlConnection veza = Connection.Connect();
            SqlCommand cmd = new SqlCommand("Unos_Jedinice", veza);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@naziv", TBoxNazivJedinice.Text);
            cmd.Parameters.AddWithValue("@puk_id", Convert.ToInt32(CBoxPukJedinice.SelectedValue));
            cmd.Parameters.AddWithValue("@lokacija", TBoxLokacijaJedinice.Text);
            cmd.Parameters.AddWithValue("@kapacitet", kap);
            try
            {
                veza.Open(); cmd.ExecuteNonQuery(); veza.Close();
                MessageBox.Show("Bataljon je dodat.");
                TBoxNazivJedinice.Clear(); TBoxLokacijaJedinice.Clear(); TBoxKapacitet.Clear();
                UpdateJedinice(); PopuniComboJedinice(); UpdateStatistika();
            }
            catch (Exception ex) { MessageBox.Show("Greska: " + ex.Message); veza.Close(); }
        }

        private void BtnIzmeniJedinicu_Click(object sender, EventArgs e)
        {
            if (DGridJedinice.CurrentRow == null) return;
            int id = Convert.ToInt32(DGridJedinice.CurrentRow.Cells["jedinica_id"].Value);

            string naziv = InputBoxDialog.Show("Novi naziv:", "Izmena bataljona",
                DGridJedinice.CurrentRow.Cells["bataljon"].Value?.ToString() ?? "");
            if (naziv == null) return;
            string lok = InputBoxDialog.Show("Nova lokacija:", "Izmena bataljona",
                DGridJedinice.CurrentRow.Cells["lokacija"].Value?.ToString() ?? "");
            if (lok == null) return;
            string kapStr = InputBoxDialog.Show("Novi kapacitet:", "Izmena bataljona",
                DGridJedinice.CurrentRow.Cells["kapacitet"].Value?.ToString() ?? "");
            if (kapStr == null) return;
            if (!int.TryParse(kapStr, out int kap) || kap <= 0)
            { MessageBox.Show("Neispravan kapacitet."); return; }

            SqlConnection veza = Connection.Connect();
            SqlCommand cmd = new SqlCommand("Izmena_Jedinice", veza);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@jedinica_id", id);
            cmd.Parameters.AddWithValue("@naziv", naziv);
            cmd.Parameters.AddWithValue("@lokacija", lok);
            cmd.Parameters.AddWithValue("@novi_kapacitet", kap);
            try
            {
                veza.Open(); cmd.ExecuteNonQuery(); veza.Close();
                MessageBox.Show("Bataljon je izmenjen.");
                UpdateJedinice(); UpdateStatistika();
            }
            catch (Exception ex) { MessageBox.Show("Greska: " + ex.Message); veza.Close(); }
        }

        private void BtnObrisiJedinicu_Click(object sender, EventArgs e)
        {
            if (DGridJedinice.CurrentRow == null) return;
            if (MessageBox.Show("Obrisati ovaj bataljon? (mora biti bez vojnika)",
                "Potvrda", MessageBoxButtons.YesNo) != DialogResult.Yes) return;

            int id = Convert.ToInt32(DGridJedinice.CurrentRow.Cells["jedinica_id"].Value);
            SqlConnection veza = Connection.Connect();
            SqlCommand cmd = new SqlCommand("Brisanje_Jedinice", veza);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@jedinica_id", id);
            var ret = cmd.Parameters.Add("@ReturnVal", SqlDbType.Int);
            ret.Direction = ParameterDirection.ReturnValue;
            try
            {
                veza.Open(); cmd.ExecuteNonQuery(); veza.Close();
                if ((int)ret.Value == 0)
                    MessageBox.Show("Bataljon ima vojnike i ne moze biti obrisan.");
                else
                {
                    MessageBox.Show("Bataljon je obrisan.");
                    UpdateJedinice(); PopuniComboJedinice(); UpdateStatistika();
                }
            }
            catch (Exception ex) { MessageBox.Show("Greska: " + ex.Message); veza.Close(); }
        }


        private void UpdateKomandanti()
        {
            tabelaKomandanti = new DataTable();
            new SqlDataAdapter(
                @"SELECT K.komandant_id, K.ime, K.prezime, C.naziv_cina AS cin,
                         C.nivo AS cin_nivo, K.jmbg, K.kontakt,
                         ISNULL(J.naziv_jedinice, '—') AS komanduje
                  FROM Komandant K
                  JOIN Cin C ON K.cin_id = C.cin_id
                  LEFT JOIN Jedinica J ON J.komandant_id = K.komandant_id
                  ORDER BY C.nivo DESC, K.prezime",
                Connection.Connect()).Fill(tabelaKomandanti);
            DGridKomandanti.DataSource = tabelaKomandanti;
            if (DGridKomandanti.Columns.Contains("komandant_id"))
                DGridKomandanti.Columns["komandant_id"].Visible = false;
            if (DGridKomandanti.Columns.Contains("cin_nivo"))
                DGridKomandanti.Columns["cin_nivo"].Visible = false;
            PopuniComboKomandanti();
        }

        private void BtnDodajKomandanta_Click(object sender, EventArgs e)
        {
            if (TBoxImeKom.Text == "" || TBoxPrezimeKom.Text == "" || TBoxJmbgKom.Text == "")
            { MessageBox.Show("Ime, prezime i JMBG su obavezni."); return; }
            if (TBoxJmbgKom.Text.Length != 13)
            { MessageBox.Show("JMBG mora imati tacno 13 cifara."); return; }
            if (CBoxCinKomandanta.SelectedValue == null)
            { MessageBox.Show("Izaberite cin."); return; }

            SqlConnection veza = Connection.Connect();
            SqlCommand cmd = new SqlCommand("Unos_Komandanta", veza);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ime", TBoxImeKom.Text);
            cmd.Parameters.AddWithValue("@prezime", TBoxPrezimeKom.Text);
            cmd.Parameters.AddWithValue("@cin_id", Convert.ToInt32(CBoxCinKomandanta.SelectedValue));
            cmd.Parameters.AddWithValue("@jmbg", TBoxJmbgKom.Text);
            cmd.Parameters.AddWithValue("@kontakt", TBoxKontaktKom.Text);
            var ret = cmd.Parameters.Add("@ReturnVal", SqlDbType.Int);
            ret.Direction = ParameterDirection.ReturnValue;
            try
            {
                veza.Open(); cmd.ExecuteNonQuery(); veza.Close();
                if ((int)ret.Value == 1)
                    MessageBox.Show("Komandant sa tim JMBG-om vec postoji.");
                else
                {
                    MessageBox.Show("Komandant je dodat.");
                    TBoxImeKom.Clear(); TBoxPrezimeKom.Clear();
                    TBoxJmbgKom.Clear(); TBoxKontaktKom.Clear();
                    UpdateKomandanti(); UpdateJedinice();
                }
            }
            catch (Exception ex) { MessageBox.Show("Greska: " + ex.Message); veza.Close(); }
        }

        private void BtnObrisiKomandanta_Click(object sender, EventArgs e)
        {
            if (DGridKomandanti.CurrentRow == null) return;
            int id = Convert.ToInt32(DGridKomandanti.CurrentRow.Cells["komandant_id"].Value);
            if (MessageBox.Show("Obrisati ovog komandanta? Bice uklonjen sa svih jedinica.",
                "Potvrda", MessageBoxButtons.YesNo) != DialogResult.Yes) return;

            SqlConnection veza = Connection.Connect();
            SqlCommand cmd = new SqlCommand("Brisanje_Komandanta", veza);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@komandant_id", id);
            try
            {
                veza.Open(); cmd.ExecuteNonQuery(); veza.Close();
                MessageBox.Show("Komandant je obrisan.");
                UpdateKomandanti(); UpdateJedinice();
            }
            catch (Exception ex) { MessageBox.Show("Greska: " + ex.Message); veza.Close(); }
        }

        private void BtnDodeliKomandanta_Click(object sender, EventArgs e)
        {
            if (CBoxKomandant.SelectedValue == null)
            { MessageBox.Show("Izaberite komandanta."); return; }
            if (CBoxJedinicaKomandanta.SelectedValue == null)
            { MessageBox.Show("Izaberite bataljon."); return; }

            int komId = Convert.ToInt32(CBoxKomandant.SelectedValue);
            int jedId = Convert.ToInt32(CBoxJedinicaKomandanta.SelectedValue);

            SqlConnection veza = Connection.Connect();
            SqlCommand cmd = new SqlCommand("Dodeli_Komandanta", veza);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@jedinica_id", jedId);
            cmd.Parameters.AddWithValue("@komandant_id", komId);
            var ret = cmd.Parameters.Add("@ReturnVal", SqlDbType.Int);
            ret.Direction = ParameterDirection.ReturnValue;
            try
            {
                veza.Open(); cmd.ExecuteNonQuery(); veza.Close();
                switch ((int)ret.Value)
                {
                    case 0: MessageBox.Show("Komandant je uspesno dodeljen bataljonu."); break;
                    case 1: MessageBox.Show("Bataljon nije pronadjen."); break;
                    case 2:
                        MessageBox.Show("GRESKA: Cin komandanta je PRENIZAK za ovaj tip jedinice!\n" +
                            "Proverite minimalni cin koji se zahteva za dati tip bataljona.",
                            "Neprihvatljiv cin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;
                    case 3: MessageBox.Show("Komandant nije pronadjen."); break;
                }
                UpdateKomandanti(); UpdateJedinice();
            }
            catch (Exception ex) { MessageBox.Show("Greska: " + ex.Message); veza.Close(); }
        }

        private void BtnUkloniKomandanta_Click(object sender, EventArgs e)
        {
            if (CBoxJedinicaKomandanta.SelectedValue == null)
            { MessageBox.Show("Izaberite bataljon."); return; }
            int jedId = Convert.ToInt32(CBoxJedinicaKomandanta.SelectedValue);

            SqlConnection veza = Connection.Connect();
            SqlCommand cmd = new SqlCommand("Ukloni_Komandanta", veza);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@jedinica_id", jedId);
            try
            {
                veza.Open(); cmd.ExecuteNonQuery(); veza.Close();
                MessageBox.Show("Komandant je uklonjen sa bataljona.");
                UpdateKomandanti(); UpdateJedinice();
            }
            catch (Exception ex) { MessageBox.Show("Greska: " + ex.Message); veza.Close(); }
        }


        private void UpdateMobilizacije()
        {
            tabelaMobilizacije = new DataTable();
            new SqlDataAdapter(
                @"SELECT M.mobilizacija_id,
                         V.ime + ' ' + V.prezime AS vojnik,
                         C.naziv_cina AS cin,
                         J.naziv_jedinice AS bataljon,
                         P.naziv_puka AS puk,
                         D.naziv_divizije AS divizija,
                         M.status_aktivnosti AS status,
                         M.datum_pocetka
                  FROM Mobilizacija M
                  JOIN Vojnik V ON M.vojnik_id = V.vojnik_id
                  JOIN Cin C ON V.cin_id = C.cin_id
                  JOIN Jedinica J ON M.jedinica_id = J.jedinica_id
                  JOIN Puk P ON J.puk_id = P.puk_id
                  JOIN Divizija D ON P.divizija_id = D.divizija_id
                  ORDER BY D.naziv_divizije, P.naziv_puka, J.naziv_jedinice, V.prezime",
                Connection.Connect()).Fill(tabelaMobilizacije);
            DGridMobilizacije.DataSource = tabelaMobilizacije;
            if (DGridMobilizacije.Columns.Contains("mobilizacija_id"))
                DGridMobilizacije.Columns["mobilizacija_id"].Visible = false;
        }

        private void BtnRasporedi_Click(object sender, EventArgs e)
        {
            if (CBoxVojnik.SelectedValue == null || CBoxJedinica.SelectedValue == null)
            { MessageBox.Show("Izaberite vojnika i bataljon."); return; }

            int vojnikId   = Convert.ToInt32(CBoxVojnik.SelectedValue);
            int jedinicaId = Convert.ToInt32(CBoxJedinica.SelectedValue);
            string status  = CBoxStatus.Text;

            SqlConnection veza = Connection.Connect();
            SqlCommand cmd = new SqlCommand("Rasporedi_Vojnika", veza);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@vojnik_id", vojnikId);
            cmd.Parameters.AddWithValue("@jedinica_id", jedinicaId);
            cmd.Parameters.AddWithValue("@status", status);
            var ret = cmd.Parameters.Add("@ReturnVal", SqlDbType.Int);
            ret.Direction = ParameterDirection.ReturnValue;
            try
            {
                veza.Open(); cmd.ExecuteNonQuery(); veza.Close();
                switch ((int)ret.Value)
                {
                    case 1: MessageBox.Show("Vojnik je uspesno rasporedjen."); break;
                    case 0: MessageBox.Show("Bataljon je popunjen do maksimalnog kapaciteta."); break;
                    case 2: MessageBox.Show("Vojnik je vec mobilizovan u drugoj jedinici."); break;
                }
                UpdateMobilizacije(); UpdateStatistika();
            }
            catch (Exception ex) { MessageBox.Show("Greska: " + ex.Message); veza.Close(); }
        }

        private void BtnPromeniStatus_Click(object sender, EventArgs e)
        {
            if (DGridMobilizacije.CurrentRow == null) return;

            string noviStatus = InputBoxDialog.Show(
                "Novi status (Aktivan / Rezerva / Otpusten):",
                "Izmena statusa",
                DGridMobilizacije.CurrentRow.Cells["status"].Value?.ToString() ?? "");
            if (noviStatus == null) return;
            if (noviStatus != "Aktivan" && noviStatus != "Rezerva" && noviStatus != "Otpusten")
            { MessageBox.Show("Status mora biti: Aktivan, Rezerva ili Otpusten."); return; }

            int mobId = Convert.ToInt32(DGridMobilizacije.CurrentRow.Cells["mobilizacija_id"].Value);

            SqlConnection veza = Connection.Connect();
            SqlCommand cmd = new SqlCommand(
                "UPDATE Mobilizacija SET status_aktivnosti=@s WHERE mobilizacija_id=@id", veza);
            cmd.Parameters.AddWithValue("@s", noviStatus);
            cmd.Parameters.AddWithValue("@id", mobId);
            try
            {
                veza.Open(); cmd.ExecuteNonQuery(); veza.Close();
                MessageBox.Show("Status je promenjen.");
                UpdateMobilizacije(); UpdateStatistika();
            }
            catch (Exception ex) { MessageBox.Show("Greska: " + ex.Message); veza.Close(); }
        }


        private void UpdateKorisnici()
        {
            tabelaKorisnici = new DataTable();
            new SqlDataAdapter(
                @"SELECT K.korisnik_id, K.email, U.naziv_uloga AS uloga
                  FROM Korisnik K
                  JOIN Uloga U ON K.uloga_id = U.uloga_id
                  ORDER BY U.naziv_uloga, K.email",
                Connection.Connect()).Fill(tabelaKorisnici);
            DGridKorisnici.DataSource = tabelaKorisnici;
            if (DGridKorisnici.Columns.Contains("korisnik_id"))
                DGridKorisnici.Columns["korisnik_id"].Visible = false;
        }

        private void BtnDodajKorisnika_Click(object sender, EventArgs e)
        {
            string email = InputBoxDialog.Show("Unesite email:", "Novi korisnik", "");
            if (string.IsNullOrWhiteSpace(email)) return;
            string lozinka = InputBoxDialog.Show("Unesite lozinku:", "Novi korisnik", "");
            if (string.IsNullOrWhiteSpace(lozinka)) return;

            SqlConnection veza = Connection.Connect();
            SqlCommand cmd = new SqlCommand("Unos_Korisnika", veza);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@email", email.Trim());
            cmd.Parameters.AddWithValue("@lozinka", lozinka);
            var ret = cmd.Parameters.Add("@ReturnVal", SqlDbType.Int);
            ret.Direction = ParameterDirection.ReturnValue;
            try
            {
                veza.Open(); cmd.ExecuteNonQuery(); veza.Close();
                if ((int)ret.Value == 1)
                    MessageBox.Show("Korisnik sa tim emailom vec postoji.");
                else
                {
                    MessageBox.Show("Korisnik je dodat.");
                    UpdateKorisnici(); PopuniComboJedinice();
                }
            }
            catch (Exception ex) { MessageBox.Show("Greska: " + ex.Message); veza.Close(); }
        }

        private void BtnObrisiKorisnika_Click(object sender, EventArgs e)
        {
            if (DGridKorisnici.CurrentRow == null) return;
            string email = DGridKorisnici.CurrentRow.Cells["email"].Value?.ToString();
            if (MessageBox.Show("Obrisati korisnika '" + email + "'?\nBice obrisani i svi vojnici i mobilizacije vezani za ovaj nalog.",
                "Potvrda", MessageBoxButtons.YesNo) != DialogResult.Yes) return;

            SqlConnection veza = Connection.Connect();
            SqlCommand cmd = new SqlCommand("Brisanje_Korisnika", veza);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@email", email);
            try
            {
                veza.Open(); cmd.ExecuteNonQuery(); veza.Close();
                MessageBox.Show("Korisnik je obrisan.");
                UpdateKorisnici(); UpdateVojnici(); UpdateMobilizacije(); PopuniComboVojnici();
            }
            catch (Exception ex) { MessageBox.Show("Greska: " + ex.Message); veza.Close(); }
        }

        private void BtnPromovisaj_Click(object sender, EventArgs e)
        {
            if (DGridKorisnici.CurrentRow == null) return;
            string email         = DGridKorisnici.CurrentRow.Cells["email"].Value?.ToString();
            string trenutnaUloga = DGridKorisnici.CurrentRow.Cells["uloga"].Value?.ToString();

            string novaUloga = trenutnaUloga == "Admin" ? "Korisnik" : "Admin";

            SqlConnection veza = Connection.Connect();
            SqlCommand cmd = new SqlCommand(
                @"UPDATE Korisnik
                  SET uloga_id = (SELECT uloga_id FROM Uloga WHERE naziv_uloga = @novaUloga)
                  WHERE email = @email", veza);
            cmd.Parameters.AddWithValue("@novaUloga", novaUloga);
            cmd.Parameters.AddWithValue("@email", email);
            try
            {
                veza.Open(); cmd.ExecuteNonQuery(); veza.Close();
                MessageBox.Show("Uloga korisnika '" + email + "' promenjena u: " + novaUloga);
                UpdateKorisnici();
            }
            catch (Exception ex) { MessageBox.Show("Greska: " + ex.Message); veza.Close(); }
        }


        private void UpdateStatistika()
        {
            tabelaStatistika = new DataTable();
            try
            {
                new SqlDataAdapter(
                    @"SELECT D.naziv_divizije AS divizija,
                             P.naziv_puka AS puk,
                             T.naziv_tipa AS tip,
                             J.naziv_jedinice AS bataljon,
                             J.maks_kapacitet AS kapacitet,
                             COUNT(CASE WHEN M.status_aktivnosti='Aktivan'  THEN 1 END) AS aktivnih,
                             COUNT(CASE WHEN M.status_aktivnosti='Rezerva'  THEN 1 END) AS rezerva,
                             COUNT(CASE WHEN M.status_aktivnosti='Otpusten' THEN 1 END) AS otpustenih,
                             J.maks_kapacitet - COUNT(CASE WHEN M.status_aktivnosti='Aktivan' THEN 1 END) AS slobodnih_mesta
                      FROM Jedinica J
                      JOIN Puk P ON J.puk_id = P.puk_id
                      JOIN TipJedinice T ON P.tip_id = T.tip_id
                      JOIN Divizija D ON P.divizija_id = D.divizija_id
                      LEFT JOIN Mobilizacija M ON J.jedinica_id = M.jedinica_id
                      GROUP BY D.naziv_divizije, P.naziv_puka, T.naziv_tipa,
                               J.naziv_jedinice, J.maks_kapacitet
                      ORDER BY D.naziv_divizije, P.naziv_puka, J.naziv_jedinice",
                    Connection.Connect()).Fill(tabelaStatistika);
                DGridStatistika.DataSource = tabelaStatistika;
            }
            catch (Exception ex) { MessageBox.Show("Greska pri ucitavanju statistike: " + ex.Message); }
        }


        private void BtnLogout_Click(object sender, EventArgs e)
        {
            ExitApp = false;
            this.Close();
        }

        private void AdminPage_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.ApplicationExitCall) return;
            if (ExitApp) Application.Exit();
            else
            {
                Login loginForm = parent as Login;
                if (loginForm != null) loginForm.ShowAgain();
                else parent.Show();
            }
        }
    }
}
