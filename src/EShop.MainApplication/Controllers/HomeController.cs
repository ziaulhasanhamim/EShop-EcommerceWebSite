using EShop.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EShop.MainApplication.Controllers
{
    [Route("")]
    public class HomeController : Controller
    {
        IProductsRepository _productRepo;

        public HomeController(IProductsRepository productRepo)
        {
            _productRepo = productRepo;
        }

        [HttpGet("", Name = "Index")]
        public async Task<IActionResult> Index()
        {
            return View(await _productRepo.GetAllRowsAsync());
        }
    }
}
