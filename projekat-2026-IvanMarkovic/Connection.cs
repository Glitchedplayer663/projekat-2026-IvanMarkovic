using System.Configuration;
using System.Data.SqlClient;

namespace projekat_2026_IvanMarkovic
{
    internal class Connection
    {
        public static SqlConnection Connect()
        {
            string cs = ConfigurationManager.ConnectionStrings["kuca"].ConnectionString;
            return new SqlConnection(cs);
        }
    }
}
