using AMTIPrueba.Repository.Models;
using System.Data.SqlClient;
using System.Data;
using System.Net.NetworkInformation;
using System.Xml.Linq;

namespace AMTIPrueba.Repository
{
	public class ProductsRepository
	{
		public List<ProductsModel> getAll()
		{
			var productsList = new List<ProductsModel>();
			var db = new DBConnection();

			using (var _db = new SqlConnection(db.getSQLString()))
			{
				_db.Open();
				SqlCommand cmd = new SqlCommand("SP_Products", _db);
				cmd.CommandType = CommandType.StoredProcedure;

				using(var dr = cmd.ExecuteReader())
				{
					while (dr.Read())
					{
						productsList.Add(new ProductsModel
						{
							ID = Convert.ToInt32(dr["ID"]),
							clave = dr["clave"].ToString(),
							name = dr["name"].ToString(),
							product_type_id = Convert.ToInt32(dr["product_type_id"]),
							product_type = dr["product_type"].ToString(),
							status = dr["status"].ToString(),
							price = Convert.ToDouble(dr["price"]),
						});
					}
                    dr.Close();
				}

			}
			return productsList;
		}

        public bool Save(ProductsModel product)
        {
            bool success = false;

            try
            {
                var db = new DBConnection();

                using (var connection = new SqlConnection(db.getSQLString()))
                {
                    connection.Open();
                    using (var cmd = new SqlCommand("SP_InsertProduct", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Agrega parámetros al procedimiento almacenado
                        cmd.Parameters.AddWithValue("@Name", product.name);
                        cmd.Parameters.AddWithValue("@Clave", product.clave);
                        cmd.Parameters.AddWithValue("@ProductTypeID", product.product_type_id);
                        cmd.Parameters.AddWithValue("@Status", product.status);
                        cmd.Parameters.AddWithValue("@Price", product.price);

                        // Ejecuta el procedimiento almacenado
                        cmd.ExecuteNonQuery();

                        success = true;
                    }
                }
            }
            catch (Exception ex)
            {
                // Puedes personalizar el mensaje de error o manejar la excepción de otra manera
                string errorMessage = "Error al insertar el producto: " + ex.Message;
                throw new Exception(errorMessage);
            }

            return success;
        }


        public ProductsModel Get(int product_id)
		{
			var product = new ProductsModel();

            var cn = new DBConnection();

            using (var conexion = new SqlConnection(cn.getSQLString()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("SP_GetProductByID", conexion);
                cmd.Parameters.AddWithValue("ProductID", product_id);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = cmd.ExecuteReader())
                {

                    while (dr.Read())
                    {
						product.ID = Convert.ToInt32(dr["ID"]);
						product.clave = dr["clave"].ToString();
						product.name = dr["name"].ToString();
						product.product_type_id = Convert.ToInt32(dr["product_type_id"]);
						product.status = dr["status"].ToString();
						product.price = Convert.ToDouble(dr["price"]);
                    }
                    dr.Close();
                }
            }

            return product;
		}

		public bool Update(ProductsModel product)
		{
            bool success = false;
            try
            {
                var db = new DBConnection();
                using (var connection = new SqlConnection(db.getSQLString()))
                {
                    connection.Open();
                    using (var cmd = new SqlCommand("SP_UpdateProduct", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@ProductID", product.ID);
                        cmd.Parameters.AddWithValue("@NewName", product.name);
                        cmd.Parameters.AddWithValue("@NewClave", product.clave);
                        cmd.Parameters.AddWithValue("@NewProductTypeID", product.product_type_id);
                        cmd.Parameters.AddWithValue("@NewStatus", product.status);
                        cmd.Parameters.AddWithValue("@NewPrice", product.price);

                        // Ejecuta el procedimiento almacenado
                        cmd.ExecuteNonQuery();

                        success = true;
                    }
                }
            }
            catch (Exception ex)
            {
                // Puedes personalizar el mensaje de error o manejar la excepción de otra manera
                string errorMessage = "Error al insertar el producto: " + ex.Message;
                throw new Exception(errorMessage);
            }

            return success;
        }

        public bool Delete(ProductsModel product)
        {
            bool success = false;

            try
            {
                var db = new DBConnection();

                using (var connection = new SqlConnection(db.getSQLString()))
                {
                    connection.Open();
                    using (var cmd = new SqlCommand("SP_DeleteProduct", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ProductID", product.ID);

                        // Ejecuta el procedimiento almacenado
                        cmd.ExecuteNonQuery();

                        success = true;
                    }
                }
            }
            catch (Exception ex)
            {
                // Puedes personalizar el mensaje de error o manejar la excepción de otra manera
                string errorMessage = "Error al insertar el producto: " + ex.Message;
                throw new Exception(errorMessage);
            }

            return success;
        }
    }
}

