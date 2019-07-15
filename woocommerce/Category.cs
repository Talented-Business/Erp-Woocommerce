using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using WooCommerceNET;
using WooCommerceNET.WooCommerce;
using WooCommerceNET.WooCommerce.v3;
using WooCommerceNET.WooCommerce.v3.Extension;

namespace woocommerce
{
    class Category
    {
        public string name { get; set; }
        public int id { get; set; }
        public int idwoocommerce { get; set; }

        private void update() {
            String updateCategorySql = "UPDATE categories  SET idwoocommerce = @idwoocommerce Where id =@id";
            SqlConnection cnn = DbConnection.getDBConnection();
            SqlCommand cmd = new SqlCommand(updateCategorySql, cnn);
            cmd.Parameters.AddWithValue("@idwoocommerce", idwoocommerce);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
        }
        private static async Task getProductCategories(List<Category> erpCategories) {
            MessageBox.Show("Async Start");
            WCObject wc = WC.getWC();
            List<ProductCategory> productCategories = await wc.Category.GetAll();
            ProductCategoryBatch productCategoryBatch = new ProductCategoryBatch();
            //List<ProductCategory> createCategories = new List<ProductCategory>();
            List<int> deleteCategories = new List<int>();
            List<ProductCategory> updateCategories = new List<ProductCategory>();
            List<Category> addCategories = erpCategories.GetRange(0, erpCategories.Count);
            foreach (ProductCategory category in productCategories)
            {
                //MessageBox.Show(category.id.ToString());
                bool isExist = false;
                foreach (Category cc in erpCategories) {
                    if (category.id.Equals(cc.idwoocommerce) == false)
                    {
                    }
                    else
                    {
                        addCategories.Remove(cc);
                        if (category.name.Equals(cc.name) == false)
                        {
                            ProductCategory updatecategory = new ProductCategory()
                            {
                                id = cc.idwoocommerce,
                                name = cc.name
                            };
                            updateCategories.Add(updatecategory);
                        }
                        isExist = true;
                    }
                }
                if (isExist == false)
                {
                    if(category.slug != "uncategorized" || true) deleteCategories.Add(Int32.Parse(category.id.ToString()));
                }
            }
            foreach (Category cc in addCategories) {
                MessageBox.Show("Add end");
                ProductCategory category = new ProductCategory()
                {
                    name = cc.name
                };
                ProductCategory categoryReturn = await wc.Category.Add(category);
                cc.idwoocommerce = Int32.Parse(categoryReturn.id.ToString());
                cc.update();
            }
            productCategoryBatch.update = updateCategories;
            productCategoryBatch.delete = deleteCategories;
            try
            {
                var result = await wc.Category.UpdateRange(productCategoryBatch);
                if (result.update != null) foreach (ProductCategory cc in result.update)
                {
                    Console.WriteLine(cc.name);
                }
            }
            finally
            {
                Console.WriteLine("Excute");
                MessageBox.Show("end");
            }
            Console.WriteLine("Excute End");
        }
        static public void up()
        {
            string queryString = "SELECT *  FROM  categories";
            SqlConnection cnn = DbConnection.getDBConnection();

            SqlCommand command = new SqlCommand(queryString,cnn);
            SqlDataReader reader = command.ExecuteReader();
            List<Category> erpCategories = new List<Category>();
            try
            {
                while (reader.Read())
                {
                    Category category = new Category();
                    category.name = reader["name"].ToString();
                    category.id = Int32.Parse(reader["id"].ToString());
                    category.idwoocommerce = Int32.Parse(reader["idwoocommerce"].ToString());
                    //MessageBox.Show(String.Format("{0}, {1}", reader["id"], reader["name"]));
                    erpCategories.Add(category);
                }
                erpCategories.ForEach(delegate(Category r)
                {
                    //MessageBox.Show(r.ToString());
                });
            }
            finally
            {
                // Always call Close when done reading.
                reader.Close();
            }
            getProductCategories(erpCategories);
        }
        /*        static public List<> getAll(){

                }*/
    }
}
