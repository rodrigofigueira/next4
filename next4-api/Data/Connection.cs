using System.Data.SqlClient;

namespace next4_api.Data
{
    public class Connection
    {
        public SqlConnection connection = null;

        public Connection()
        {
            //Persist Security Info=False;uid=sa;server=.\SQLEXPRESS2019;database=next4;uid=sa;pwd=339023
            connection = new SqlConnection(@"Persist Security Info=False;;server=.\SQLEXPRESS2019;database=next4;uid=sa;pwd=sql339023");
        }

    }
}