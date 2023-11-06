using System;
namespace AMTIPrueba.Repository.Models
{
	public class SuppliersProductsModel
	{
		public int supplier_id { get; set; }
		public int product_id { get; set; }
		public double supplier_price { get; set; }
		public string? supplier_clave { get; set; }
	}
}

