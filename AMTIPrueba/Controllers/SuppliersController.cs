using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Data;

using AMTIPrueba.Repository;
using AMTIPrueba.Repository.Models;

namespace AMTIPrueba.Controllers
{
	public class SuppliersController: Controller
    {
        //Instancia reposiroties que se vayan a utilizar
        ProductsRepository productsRepository = new ProductsRepository();
        SuppliersRepository suppliersRepository = new SuppliersRepository();

        public IActionResult ToSave(int product_id)
		{
			ViewBag.product = productsRepository.Get(product_id);
            ViewBag.suppliers = suppliersRepository.getSuppliersCatalog();
			ViewBag.product_id = product_id;
            return View();
		}
		[HttpPost]
		public IActionResult ToSave(SuppliersProductsModel sp)
		{

            var isSuccess = suppliersRepository.SaveSupplierProduct(sp);
            if (isSuccess)
                return RedirectToAction("ToList", "Products");
            else
                return View();
		}

		public IActionResult ToEdit(int supplier_id, int product_id)
		{
			var product =  productsRepository.Get(product_id);
			var supplier = suppliersRepository.GetSupplierById(supplier_id);
			ViewBag.product = product;
			ViewBag.supplier = supplier;
			var supplier_product = suppliersRepository.getSupplierModel(product_id, supplier_id);
			return View(supplier_product);
		}
		[HttpPost]
		public IActionResult ToEdit(SuppliersProductsModel sp)
		{

            return RedirectToAction("ToList", "Products");
        }

		public IActionResult ConfirmDelete()
        {
            return RedirectToAction("ToList", "Products");
        }
		[HttpPost]
		public IActionResult ConfirmDelete(SuppliersProductsModel sp)
        {
            return RedirectToAction("ToList", "Products");
        }
	}
}

