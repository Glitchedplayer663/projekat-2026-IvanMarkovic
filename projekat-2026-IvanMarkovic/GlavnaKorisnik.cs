using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace projekat_2026_IvanMarkovic
{
    public partial class GlavnaKorisnik : Form
    {
        private bool ExitApp = true;
        private Form parent;
        private int KorisnikId;
        private int? VojnikId;

        DataTable tabelaJedinice  = new DataTable();
        DataTable tabelaMojVojnik = new DataTable();

        public GlavnaKorisnik(int korisnikId, int? vojnikId, Form parent)
        {
            InitializeComponent();
            this.KorisnikId = korisnikId;
            this.VojnikId   = vojnikId;
            this.parent     = parent;
        }

        private void GlavnaKorisnik_Load(object sender, EventArgs e)
        {
            UpdateJedinice();
            UpdateMojVojnik();
        }

        private void UpdateJedinice()
        {
            tabelaJedinice = new DataTable();
            new SqlDataAdapter(
                @"SELECT D.naziv_divizije AS divizija,
                         P.naziv_puka AS puk,
                         T.naziv_tipa AS tip,
                         J.naziv_jedinice AS bataljon,
                         J.lokacija,
                         J.maks_kapacitet AS kapacitet,
                         ISNULL(KOM.ime + ' ' + KOM.prezime, '—') AS komandant,
                         ISNULL(C.naziv_cina, '—') AS cin_komandanta,
                         COUNT(CASE WHEN M.status_aktivnosti='Aktivan' THEN 1 END) AS aktivnih,
                         J.maks_kapacitet - COUNT(CASE WHEN M.status_aktivnosti='Aktivan' THEN 1 END) AS slobodnih
                  FROM Jedinica J
                  JOIN Puk P ON J.puk_id = P.puk_id
                  JOIN TipJedinice T ON P.tip_id = T.tip_id
                  JOIN Divizija D ON P.divizija_id = D.divizija_id
                  LEFT JOIN Komandant KOM ON J.komandant_id = KOM.komandant_id
                  LEFT JOIN Cin C ON KOM.cin_id = C.cin_id
                  LEFT JOIN Mobilizacija M ON J.jedinica_id = M.jedinica_id
                  GROUP BY D.naziv_divizije, P.naziv_puka, T.naziv_tipa,
                           J.naziv_jedinice, J.lokacija, J.maks_kapacitet,
                           KOM.ime, KOM.prezime, C.naziv_cina
                  ORDER BY D.naziv_divizije, P.naziv_puka, J.naziv_jedinice",
                Connection.Connect()).Fill(tabelaJedinice);
            DGridJedinice.DataSource = tabelaJedinice;
        }

        private void UpdateMojVojnik()
        {
            tabelaMojVojnik = new DataTable();
            if (VojnikId == null)
            {
                DGridMojVojnik.DataSource = null;
                LblMojVojnik.Text = "Moj vojnicki profil: (nije vezan za vojnika)";
                return;
            }
            LblMojVojnik.Text = "Moj vojnicki profil i status:";

            // ── BUGFIX: removed 'C.kategorija' — column does not exist in Cin table ──
            new SqlDataAdapter(
                @"SELECT V.ime, V.prezime, V.jmbg,
                         C.naziv_cina AS cin,
                         ISNULL(J.naziv_jedinice, '—') AS bataljon,
                         ISNULL(P.naziv_puka, '—') AS puk,
                         ISNULL(D.naziv_divizije, '—') AS divizija,
                         ISNULL(M.status_aktivnosti, '—') AS status,
                         M.datum_pocetka
                  FROM Vojnik V
                  JOIN Cin C ON V.cin_id = C.cin_id
                  LEFT JOIN Mobilizacija M ON V.vojnik_id = M.vojnik_id
                  LEFT JOIN Jedinica J ON M.jedinica_id = J.jedinica_id
                  LEFT JOIN Puk P ON J.puk_id = P.puk_id
                  LEFT JOIN Divizija D ON P.divizija_id = D.divizija_id
                  WHERE V.vojnik_id = " + VojnikId.Value,
                Connection.Connect()).Fill(tabelaMojVojnik);
            DGridMojVojnik.DataSource = tabelaMojVojnik;
        }

        private void BtnOsvezi_Click(object sender, EventArgs e)
        {
            UpdateJedinice();
            UpdateMojVojnik();
        }

        private void BtnLogout_Click(object sender, EventArgs e)
        {
            ExitApp = false;
            this.Close();
        }

        private void GlavnaKorisnik_FormClosing(object sender, FormClosingEventArgs e)
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
