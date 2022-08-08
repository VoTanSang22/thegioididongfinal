using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using thegioididong.Models;

namespace hegioididong.Controllers
{
    public class HomeController : Controller
    {
        private readonly MyDBConext _context;
        public HomeController(MyDBConext context)
        {
            _context = context;
        }
        

        // POST: Comments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        

        // GET: Products
        public async Task<IActionResult> Index()
        {
            
            var myDBConext = _context.Product.Include(p => p.Category).Include(p => p.Manufacturer).Include(p => p.Images);
            return View(await myDBConext.ToListAsync());
        }
        public async Task<IActionResult> Search(string key)
        {
            var myDBConext = from p in _context.Product.Include(p => p.Category).Include(p => p.Manufacturer).Include(p => p.Images) where EF.Functions.Like(p.Pro_Name, "%"+key+"%") select p;
            return View(await myDBConext.ToListAsync());
            
        }
        public async Task<IActionResult> Details(int? id)
        {
            
            if (id == null || _context.Product == null)
            {
                return NotFound();
            }
           
            var product = await _context.Product
                .Include(p => p.Category)
                .Include(p => p.Manufacturer)
                .Include(p => p.Images)
                .Include(p =>p.Comments)
                .FirstOrDefaultAsync(m => m.Id_pro == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }
        public async Task<IActionResult> showmore(string id)
        {
            var myDBConext = from p in _context.Product.Include(p => p.Category).Include(p => p.Manufacturer).Include(p => p.Images) where p.Category.Name_Category ==id select p;
            return View(await myDBConext.ToListAsync());

            
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> comment(int id_pro,string cusName,string cusPhone,string cusMail,string noidung)
        {
            if (true)
            {
                Comment comment = new Comment { Id_pro=id_pro,Name_cus=cusName,phone=cusPhone,mail=cusMail,content=noidung};
                
                _context.Add(comment);
                await _context.SaveChangesAsync();
                return RedirectToRoute(new { Controller = "Home", Action = "Details", ID=id_pro });
            }
            //ViewData["Id_pro"] = new SelectList(_context.Product, "Id_pro", "Id_pro", comment.Id_pro);
            return View(comment);
        }
    }
}
