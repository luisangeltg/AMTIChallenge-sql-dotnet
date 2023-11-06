using System;
namespace AMTIPrueba.Repository.Models
{
	public class CSuppliersProductsModel
	{
		public int supplier_id { get; set; }
		public int product_id { get; set; }
		public double supplier_price { get; set; }
		public string? supplier_clave { get; set; }
		public string? supplier_name { get; set; }

		public string? product_type { get; set; }
		public string? product_description { get; set; }
		public string? product_name { get; set; }
		public double product_price { get; set; }
		public string? product_status { get; set; }
    }
}

