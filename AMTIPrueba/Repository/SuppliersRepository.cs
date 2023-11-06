using System;
using System.Data.SqlClient;
using System.Data;
using AMTIPrueba.Repository.Models;

namespace AMTIPrueba.Repository
{
	public class SuppliersRepository
	{
        public bool SaveSupplierProduct(SuppliersProductsModel sp)
        {

            bool success = false;
            try
            {
                var db = new DBConnection();

                using (var connection = new SqlConnection(db.getSQLString()))
                {
                    connection.Open();
                    using (var cmd = new SqlCommand("SP_InsertSupplier_Product", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Agrega parámetros al procedimiento almacenado
                        cmd.Parameters.AddWithValue("@ProductID", sp.product_id);
                        cmd.Parameters.AddWithValue("@SupplierID", sp.supplier_id);
                        cmd.Parameters.AddWithValue("@SupplierClave", sp.supplier_clave);
                        cmd.Parameters.AddWithValue("@SupplierPrice", sp.supplier_price);

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

		public List<SuppliersModel> getAll()
		{
			var items = new List<SuppliersModel>();

            var db = new DBConnection();
            using (var _db = new SqlConnection(db.getSQLString()))
            {
                _db.Open();
                SqlCommand cmd = new SqlCommand("SP_ProductTypes", _db);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        items.Add(new SuppliersModel
                        {
                            ID = Convert.ToInt32(dr["ID"]),
                            name = dr["name"].ToString(),
                            description = dr["description"].ToString(),
                        });
                    }
                }
            }

            return items;
		}

        public List<CSuppliersProductsModel> getCustomSuppliers(int productID) {
            var items = new List<CSuppliersProductsModel>();


            var db = new DBConnection();
            using (var _db = new SqlConnection(db.getSQLString()))
            {
                _db.Open();
                SqlCommand cmd = new SqlCommand("SP_GetCustomSuppliersProducts", _db);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ProductID", productID);

                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        items.Add(new CSuppliersProductsModel
                        {
                            supplier_id = Convert.ToInt32(dr["supplier_id"]),
                            product_id = Convert.ToInt32(dr["product_id"]),
                            supplier_price = Convert.ToDouble(dr["supplier_price"]),
                            supplier_clave = dr["supplier_clave"].ToString(),
                            supplier_name = dr["supplier_name"].ToString(),
                            product_type = dr["product_type"].ToString(),
                            product_description = dr["product_description"].ToString(),
                            product_name = dr["product_name"].ToString(),
                            product_price = Convert.ToDouble(dr["product_price"]),
                            product_status = dr["product_status"].ToString()
                        });
                    }
                }
            }

            return items;
        }

        public SuppliersModel GetSupplierById(int supplier_id)
        {
            var item = new SuppliersModel();

            //SP_GetSupplierByID


            var cn = new DBConnection();

            using (var conexion = new SqlConnection(cn.getSQLString()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("SP_GetSupplierByID", conexion);
                cmd.Parameters.AddWithValue("@SupplierID", supplier_id);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = cmd.ExecuteReader())
                {

                    while (dr.Read())
                    {
                        item.ID = Convert.ToInt32(dr["ID"]);
                        item.description = dr["description"].ToString();
                        item.name = dr["name"].ToString();
                    }
                    dr.Close();
                }
            }


            return item;
        }

        public SuppliersProductsModel getSupplierModel(int product_id, int supplier_id)
        {
            var item = new SuppliersProductsModel();



            var cn = new DBConnection();

            using (var conexion = new SqlConnection(cn.getSQLString()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("SP_GetSupplierProductByIds", conexion);
                cmd.Parameters.AddWithValue("@SupplierID", supplier_id);
                cmd.Parameters.AddWithValue("@ProductID", product_id);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = cmd.ExecuteReader())
                {

                    while (dr.Read())
                    {
                        item.supplier_id = Convert.ToInt32(dr["supplier_id"]);
                        item.product_id= Convert.ToInt32(dr["product_id"]);
                        item.supplier_clave = dr["supplier_clave"].ToString();
                        item.supplier_price = Convert.ToDouble(dr["supplier_price"]);
                    }
                    dr.Close();
                }
            }

            return item;
        }

        public List<SuppliersModel> getSuppliersCatalog()
        {
            var items = new List<SuppliersModel>();

            var db = new DBConnection();

            using (var _db = new SqlConnection(db.getSQLString()))
            {
                _db.Open();
                SqlCommand cmd = new SqlCommand("SP_Suppliers", _db);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        items.Add(new SuppliersModel
                        {
                            ID = Convert.ToInt32(dr["ID"]),
                            name = dr["name"].ToString(),
                            description = dr["description"].ToString(),
                        });
                    }
                }
            }

            return items;
        }
	}
}

