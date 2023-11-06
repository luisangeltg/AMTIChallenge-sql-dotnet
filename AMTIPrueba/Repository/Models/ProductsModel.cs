using System;
using System.ComponentModel.DataAnnotations;

namespace AMTIPrueba.Repository.Models
{
	public class ProductsModel
	{
		public int ID { get; set; }
		[Required(ErrorMessage ="This field can't be empty")]
		public string? clave { get; set; }
        [Required(ErrorMessage = "This field can't be empty")]
        public string? name { get; set; }
        [Required(ErrorMessage = "This field can't be empty")]
        public int product_type_id { get; set; }
        [Required(ErrorMessage = "This field can't be empty")]
        public string? product_type { get; set; }
        [Required(ErrorMessage = "This field can't be empty")]
        public string? status { get; set; }
        [Required(ErrorMessage = "This field can't be empty")]
        public double price { get; set; }
	}
}

