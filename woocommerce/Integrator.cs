using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace woocommerce
{
    class Integrator
    {
        static public void up(){
            Category.up();
        }
        static public void down(){
            MessageBox.Show("down");
        }
    }
}
