using System.Data;
using Microsoft.Data.SqlClient;
using thegioididong.Models;
namespace thegioididong.Models.DAO
{
    public class DAO
    {
        string con = "Server=(local);uid=sa;pwd=123456;Database=thegioididongVer1;Trusted_Connection=True;";
        public List<Product> getProductAll(int id)
        {
            List<Product> list = new List<Product>();
            using (SqlConnection connection = new SqlConnection(con))
            {
                try
                {
                    var key = 0;
                    connection.Open();
                    SqlCommand cmd = connection.CreateCommand();
                    if(id > 1)
                    {
                        key = (id - 1) * 10;
                    }
                    cmd.CommandText = "select ROW_NUMBER() over (Order by Product.Id_pro) AS[STT], * from Product,Images,Category,Manufacturers where Product.Id_Category=Category.Id_Category AND Product.Id_manufacturer = Manufacturers.Id_manufacturer AND Product.Id_pro = Images.Id_pro ORDER BY [STT] OFFSET "+ key +"  ROWS FETCH NEXT 10 ROWS ONLY; ";
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        list.Add(new Product
                        {
                            Id_pro = Convert.ToInt32(dr["Id_pro"]),
                            Des = dr["link_image"].ToString(),
                            Pro_Name = dr["Pro_Name"].ToString(),
                            Ram = Convert.ToInt32(dr["Ram"]),
                            Rom = Convert.ToInt32(dr["Rom"]),
                            Price = Convert.ToDecimal(dr["Price"])

                        });
                    }
                }
                finally
                {
                    connection.Close();

                }
            }
            return list;
        }
        public List<Order> getOrderAll()
        {
            List<Order> list = new List<Order>();
            using (SqlConnection connection = new SqlConnection(con))
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = connection.CreateCommand();
                    cmd.CommandText = "select * from [order] ";
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        list.Add(new Order
                        {
                            namecus = dr["name_cus"].ToString(),
                            phone = dr["phone"].ToString(),
                            address = dr["address"].ToString()
                        });
                    }
                }
                finally
                {
                    connection.Close();

                }
            }
            return list;
        }
        public Product getProductbyId(string id)
        {
            using (SqlConnection connection = new SqlConnection(con))
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = connection.CreateCommand();
                    cmd.CommandText = "select * from Product,Images where Product.Id_pro= Images.Id_pro AND Product.Id_pro =" + id;
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);


                    foreach (DataRow dr in dataTable.Rows)
                    {
                        Product product = new Product
                        {
                            Id_pro = Convert.ToInt32(dr["Id_pro"]),
                            Amount = dr["link_image"].ToString(),  
                            Pro_Name = dr["Pro_Name"].ToString(),
                            Price = Convert.ToDecimal(dr["Price"])
                        };
                        connection.Close();

                        return product;
                    }

                }
                finally
                {
                    connection.Close();

                }

            }

            return null;
        }
        public void Check_Out(string namecus, string phone, string address, Product product)
        {
            using (SqlConnection connection = new SqlConnection(con))
            {
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "insert into [order] (pro_name,pro_price,pro_image,name_cus,phone,address) values (@pro_name,@pro_price,@pro_image,@namecus,@phone,@address)";
                cmd.Parameters.AddWithValue("@pro_name", product.Pro_Name);
                cmd.Parameters.AddWithValue("@pro_price", product.Price);
                cmd.Parameters.AddWithValue("@pro_image", product.Amount);
                cmd.Parameters.AddWithValue("@namecus", namecus);
                cmd.Parameters.AddWithValue("@phone", phone);
                cmd.Parameters.AddWithValue("@address", address);
                connection.Open();
                cmd.ExecuteNonQuery();
                connection.Close();
            }
        }
        public List<Order> getOrderByPhone(string phone)
        {
            List<Order> list = new List<Order>();
            using (SqlConnection connection = new SqlConnection(con))
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = connection.CreateCommand();
                    cmd.CommandText = "select * from [order] where phone = " + phone;
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        list.Add(new Order
                        {
                            pro_name = dr["pro_name"].ToString(),
                            pro_price = Convert.ToDecimal(dr["pro_price"]),
                            pro_image = dr["pro_image"].ToString()
                        });
                    }
                }
                finally
                {
                    connection.Close();

                }
            }
            return list;
        }
    }
}
