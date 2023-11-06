using System;
using System.Data.SqlClient;
using System.Data;
using AMTIPrueba.Repository.Models;

namespace AMTIPrueba.Repository
{
	public class ProductTypesRepository
	{
		public List<ProductTypesModel> getAll()
		{
			var items = new List<ProductTypesModel>();

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
                        items.Add(new ProductTypesModel
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

