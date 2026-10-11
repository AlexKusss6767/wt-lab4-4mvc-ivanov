using Microsoft.AspNetCore.Mvc;
using wt_lab4_4mvc_ivanov.Models;

namespace wt_lab4_4mvc_ivanov.Controllers
{
    public class ProductController : Controller
    {
        private static readonly List<Product> Products = new()
        {
            new Product { Id = 1, Name = "Ноутбук", Category = "Компьютеры", Price = 2500, IsAvailable = true, Description = "Мощный ноутбук" },
            new Product { Id = 2, Name = "Мышь", Category = "Аксессуары", Price = 80, IsAvailable = true, Description = "Беспроводная мышь" },
            new Product { Id = 3, Name = "Клавиатура", Category = "Аксессуары", Price = 120, IsAvailable = false, Description = "Механическая клавиатура" },
            new Product { Id = 4, Name = "Монитор", Category = "Компьютеры", Price = 800, IsAvailable = true, Description = "27-дюймовый монитор" },
            new Product { Id = 5, Name = "Наушники", Category = "Аксессуары", Price = 150, IsAvailable = true, Description = "Игровые наушники" }
        };

        public IActionResult Index()
        {
            return View(Products);
        }

        public IActionResult Details(int id)
        {
            var product = Products.FirstOrDefault(p => p.Id == id);
            if (product == null)
                return NotFound();
            return View(product);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                product.Id = Products.Any() ? Products.Max(p => p.Id) + 1 : 1;
                Products.Add(product);
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        public IActionResult Edit(int id)
        {
            var product = Products.FirstOrDefault(p => p.Id == id);
            if (product == null)
                return NotFound();
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Product product)
        {
            if (id != product.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                var existing = Products.FirstOrDefault(p => p.Id == id);
                if (existing == null)
                    return NotFound();

                existing.Name = product.Name;
                existing.Category = product.Category;
                existing.Price = product.Price;
                existing.IsAvailable = product.IsAvailable;
                existing.Description = product.Description;

                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }
    }
}