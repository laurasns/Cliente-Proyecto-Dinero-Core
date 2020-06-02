using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProyectoCoreLauraSanNicolas.Models;
using ProyectoCoreLauraSanNicolas.Services;

namespace ProyectoCoreLauraSanNicolas.Controllers
{
    public class ProductController : Controller
    {
        IProductService productService;
        StockApiService stockApiService;

        public ProductController(IProductService productService)
        {
            this.productService = productService;
            stockApiService = new StockApiService();
        }

        // GET: Product
        public async Task<IActionResult> RawMaterials()
        {
            List<Product> products = await productService.GetProducts("RAW MATERIAL");
            return View(products);           
        }

        public async Task<IActionResult> StockExchange()
        {
            List<Product> products = await productService.GetProducts("STOCK EXCHANGE");
            return View(products);
        }

        public async Task<IActionResult> Forex()
        {
            List<Product> products = await productService.GetProducts("FOREX");
            return View(products);
        }

        public IActionResult GetStockChartData(string symbol)
        {
            Dictionary<string, double> json = stockApiService.ConvertStockDataToHighChartJson(StockApiService.TimeSerie.DAILY, symbol, 20);
            return Json(json);
        }

        public IActionResult _StockTable()
        {
            return PartialView();
        }

    }
}