using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Data;

using AMTIPrueba.Repository;
using AMTIPrueba.Repository.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AMTIPrueba.Controllers
{
    public class ProductsController : Controller
    {
        //Instancia reposiroties que se vayan a utilizar
        ProductsRepository productsRepository = new ProductsRepository();
        ProductTypesRepository productTypesRepository = new ProductTypesRepository();
        SuppliersRepository suppliersRepository = new SuppliersRepository();

        // GET: /<controller>/
        public IActionResult ToList()
        {
            var productsList = productsRepository.getAll();
            return View(productsList);
        }

        public IActionResult ToSave()
        {
            var product_types = productTypesRepository.getAll();
            ViewBag.product_types = product_types;
            return View();
        }

        [HttpPost]
        public IActionResult ToSave(ProductsModel product)
        {

            var product_types = productTypesRepository.getAll();
            ViewBag.product_types = product_types;

            var isSuccess = productsRepository.Save(product);
            if (isSuccess)
                return RedirectToAction("ToList");
            else
                return View();
        }

        public IActionResult ToEdit(int productId)
        {
            var product = productsRepository.Get(productId);
            var product_types = productTypesRepository.getAll();
            ViewBag.product_types = product_types;
            var suppliers = suppliersRepository.getCustomSuppliers(productId);
            ViewBag.suppliers = suppliers;
            return View(product);
        }

        [HttpPost]
        public IActionResult ToEdit(ProductsModel product)
        {

            var product_types = productTypesRepository.getAll();
            ViewBag.product_types = product_types;

            //if (!ModelState.IsValid)
            //    return View();

            var resp = productsRepository.Update(product);
            if (resp)
                return RedirectToAction("ToList");
            else
                return View();
        }

        public IActionResult ConfirmDelete(int productId)
        {
            var product = productsRepository.Get(productId);
            var suppliers = suppliersRepository.getCustomSuppliers(productId);
            ViewBag.suppliers = suppliers;

            return View(product);
        }

        [HttpPost]
        public IActionResult ConfirmDelete(ProductsModel product)
        {
            var resp = productsRepository.Delete(product);
            if (resp)
                return RedirectToAction("ToList");
            else
                return View();
        }
    }
}

