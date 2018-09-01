using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using late.Models;
using late.Data;
using Microsoft.EntityFrameworkCore;

namespace late.Controllers
{
    public class HomeController : BaseController
    {
        private readonly LateContext _context;

        public HomeController(LateContext context)
        {
            _context = context;
            ViewBag.Catalogs = _context.Catalog.ToArray();
        }

        public async Task<IActionResult> Index()
        {
            ViewData["Title"] = "首页";
            var posts = await _context.Posts.OrderByDescending(p => p.Time).AsNoTracking().ToArrayAsync();

            return View(posts);
        }


        public async Task<IActionResult> Catalog(string catalogUrl)
        {
            var catalog = await _context.Catalog.FirstOrDefaultAsync(c => c.Url == catalogUrl);

            if (catalog == null)
            {
                return NotFound();
            }

            var posts = await _context.Posts.Where(p => p.Catalog == catalog).OrderByDescending(p => p.Time).AsNoTracking().ToArrayAsync();

            return View(nameof(Index), posts);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
