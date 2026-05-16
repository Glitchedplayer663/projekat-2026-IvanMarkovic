using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace projekat_2026_IvanMarkovic
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            if (TBoxEmail.Text == "" && TBoxPass.Text == "")
            {
                MessageBox.Show("Morate uneti email i lozinku!");
                return;
            }
            if (TBoxEmail.Text == "") { MessageBox.Show("Morate uneti email!"); return; }
            if (TBoxPass.Text == "") { MessageBox.Show("Morate uneti lozinku!"); return; }

            SqlConnection veza = Connection.Connect();
            try
            {
                SqlCommand cmd = new SqlCommand("Provera_Logina", veza);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@email", TBoxEmail.Text.Trim());
                cmd.Parameters.AddWithValue("@lozinka", TBoxPass.Text);

                SqlParameter outUloga = new SqlParameter("@uloga", SqlDbType.NVarChar, 20);
                outUloga.Direction = ParameterDirection.Output;
                outUloga.Value = DBNull.Value;
                cmd.Parameters.Add(outUloga);

                SqlParameter ret = cmd.Parameters.Add("@ReturnVal", SqlDbType.Int);
                ret.Direction = ParameterDirection.ReturnValue;

                veza.Open();
                cmd.ExecuteNonQuery();

                if ((int)ret.Value != 0)
                {
                    MessageBox.Show("Pogresni podaci za prijavu. Pokusajte ponovo.");
                    veza.Close();
                    return;
                }

                string uloga = outUloga.Value != null && outUloga.Value != DBNull.Value
                    ? outUloga.Value.ToString() : "Korisnik";

                SqlCommand cmdId = new SqlCommand("SELECT korisnik_id FROM Korisnik WHERE email = @e", veza);
                cmdId.Parameters.AddWithValue("@e", TBoxEmail.Text.Trim());

                object korisnikObj = cmdId.ExecuteScalar();
                if (korisnikObj == null || korisnikObj == DBNull.Value)
                {
                    MessageBox.Show("Greška: Nalog uspešno proveren, ali podaci o korisniku ne postoje direktno u tabeli Korisnik.", "Greška u bazi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    veza.Close();
                    return;
                }
                int korisnikId = Convert.ToInt32(korisnikObj);

                SqlCommand cmdVojnik = new SqlCommand("SELECT vojnik_id FROM Vojnik WHERE korisnik_id = @k", veza);
                cmdVojnik.Parameters.AddWithValue("@k", korisnikId);

                object vojnikObj = cmdVojnik.ExecuteScalar();
                int? vojnikId = (vojnikObj != null && vojnikObj != DBNull.Value)
                    ? (int?)Convert.ToInt32(vojnikObj) : null;

                veza.Close();

                MessageBox.Show("Uspesno ste se ulogovali.");
                this.Hide();

                if (uloga == "Admin" || uloga == "Komandir")
                {
                    AdminPage admin = new AdminPage(korisnikId, this);
                    admin.Show();
                }
                else
                {
                    GlavnaKorisnik glavna = new GlavnaKorisnik(korisnikId, vojnikId, this);
                    glavna.Show();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Greska: " + ex.Message);
                if (veza.State == ConnectionState.Open) veza.Close();
            }
        }

        private void LinkRegistracija_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Registracija nova = new Registracija();
            nova.ShowDialog();
        }

        public void ShowAgain()
        {
            TBoxPass.Clear();
            this.Show();
        }
    }
}
