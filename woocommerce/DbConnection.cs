using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace woocommerce
{

    public sealed class DbConnection
    {
        private static volatile DbConnection instance;
        private static SqlConnection cnn = new SqlConnection("Data Source=localhost;Initial Catalog=woocommerce;Integrated Security=SSPI;MultipleActiveResultSets=true");

        private DbConnection() { }
        static DbConnection()  { }
        public static DbConnection Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (cnn)
                    {
                        if (instance == null)
                            instance = new DbConnection();
                    }
                }

                return instance;
            }
        }
        static public SqlConnection getDBConnection()
        {
            if (cnn != null && cnn.State == ConnectionState.Closed) cnn.Open();
            return cnn;
        }
    }
}
