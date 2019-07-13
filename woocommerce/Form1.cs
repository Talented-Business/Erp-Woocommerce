using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace woocommerce
{
    public partial class Form1 : Form
    {
        private SqlConnection cnn;
        public Form1()
        {
            InitializeComponent();
            dbConnection();
        }
        private void dbConnection() {
            cnn = DbConnection.getDBConnection();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string queryString = "SELECT *  FROM  categories";
                SqlCommand command = new SqlCommand(queryString, cnn);
                SqlDataReader reader = command.ExecuteReader();
                MessageBox.Show("Connection Open ! ");
                try
                {
                    List<String> readers = new List<String>();
                    while (reader.Read())
                    {
                        //Console.WriteLine(String.Format("{0}, {1}", reader["id"], reader["name"]));// etc
                        readers.Add(String.Format("{0}, {1}", reader["id"], reader["name"]));
                    }
                    readers.ForEach(delegate(String r)
                    {
                        MessageBox.Show(r);
                    });
                }
                finally
                {
                    // Always call Close when done reading.
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Can not open connection ! ");
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {

        }
        public void Form1_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MessageBox.Show("Form Closing Event....");
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Integrator.up();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Integrator.down(cnn);
        }
    }
}
