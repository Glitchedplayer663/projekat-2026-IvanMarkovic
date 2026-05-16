using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace projekat_2026_IvanMarkovic
{
    public partial class Registracija : Form
    {
        public Registracija()
        {
            InitializeComponent();
        }

        private void BtnRegistracija_Click(object sender, EventArgs e)
        {
            if (TBoxEmail.Text == "" || TBoxPass.Text == "" || TBoxPassOpet.Text == "")
            {
                MessageBox.Show("Sva polja su obavezna!");
                return;
            }
            if (TBoxPass.Text.Length < 6)
            {
                MessageBox.Show("Lozinka mora imati najmanje 6 karaktera.");
                return;
            }
            if (TBoxPass.Text != TBoxPassOpet.Text)
            {
                MessageBox.Show("Lozinke moraju biti iste.");
                return;
            }

            SqlConnection veza = Connection.Connect();
            SqlCommand cmd = new SqlCommand("Unos_Korisnika", veza);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@email", TBoxEmail.Text.Trim());
            cmd.Parameters.AddWithValue("@lozinka", TBoxPass.Text);
            var ret = cmd.Parameters.Add("@ReturnVal", SqlDbType.Int);
            ret.Direction = ParameterDirection.ReturnValue;

            try
            {
                veza.Open();
                cmd.ExecuteNonQuery();
                veza.Close();

                if ((int)ret.Value == 1)
                    MessageBox.Show("Uneti E-mail je vec vezan za postojeci nalog.");
                else
                {
                    MessageBox.Show("Nalog je uspesno kreiran. Mozete se prijaviti.");
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Greska: " + ex.Message);
                if (veza.State == ConnectionState.Open) veza.Close();
            }
        }
    }
}
