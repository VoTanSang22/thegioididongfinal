using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using thegioididong.Models;
using thegioididong.Models.DAO;

namespace thegioididong.Controllers
{
    public class ProductsController : Controller
    {
        private readonly MyDBConext _context;
        string con = "Server=(local);uid=sa;pwd=123456;Database=thegioididongVer1;Trusted_Connection=True;";

        public ProductsController(MyDBConext context)
        {
            _context = context;
        }

        // GET: Products
        public async Task<IActionResult> Index(int id)
        {

            if (HttpContext.Session.GetString("Login") == null)
            {
                return RedirectToRoute(new { Controller = "Admin", Action = "Login" });
            }
            else
            {
                List<Product> product = dao.getProductAll(id);
                var count = _context.Product.Count();
                var endPage = count / 10;
                if (count % 10 != 0)
                {
                    endPage++;
                }
                ViewBag.endPage = endPage;
                ViewBag.listorder = product;
                return View();

            }

        }
        public ActionResult Logout()
        {
            HttpContext.Session.Clear();//remove session
            return RedirectToRoute(new { Controller = "Admin", Action = "Login" });
        }
        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (HttpContext.Session.GetString("Login") == null)
            {
                return RedirectToRoute(new { Controller = "Admin", Action = "Login" });
            }
            else
            {
                if (id == null || _context.Product == null)
                {
                    return NotFound();
                }

                var product = await _context.Product
                    .Include(p => p.Category)
                    .Include(p => p.Manufacturer)
                    .FirstOrDefaultAsync(m => m.Id_pro == id);
                if (product == null)
                {
                    return NotFound();
                }

                return View(product);


            }

        }


        // GET: Products/Create
        public IActionResult Create()
        {
            if (HttpContext.Session.GetString("Login") == null)
            {
                return RedirectToRoute(new { Controller = "Admin", Action = "Login" });
            }
            else
            {
                Product product = new Product();
                ViewData["Category"] = new SelectList(_context.Category, "Id_Category", "Name_Category", product.Category);
                ViewData["Manufacturer"] = new SelectList(_context.Manufacturers, "Id_manufacturer", "Name_manufacturer", product.Manufacturer);
                return View();

            }


        }


        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id_pro,Pro_Name,Des,Id_manufacturer,Id_Category,Price,Ram,Rom,Amount")] Product product)
        {
            if (HttpContext.Session.GetString("Login") == null)
            {
                return RedirectToRoute(new { Controller = "Admin", Action = "Login" });
            }
            else
            {
                if (true)
                {
                    _context.Add(product);
                    await _context.SaveChangesAsync();
                    return RedirectToRoute(new { Controller = "Images", Action = "Create" });
                }
                ViewData["Category"] = new SelectList(_context.Category, "Id_Category", "Name_Category", product.Category);
                ViewData["Manufacturer"] = new SelectList(_context.Manufacturers, "Id_manufacturer", "Name_manufacturer", product.Manufacturer);
                return View(product);

            }


        }

        private IActionResult RedirectResult(string v)
        {
            throw new NotImplementedException();
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Product == null)
            {
                return NotFound();
            }

            var product = await _context.Product.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["Category"] = new SelectList(_context.Category, "Id_Category", "Name_Category", product.Category);

            ViewData["Manufacturer"] = new SelectList(_context.Manufacturers, "Id_manufacturer", "Name_manufacturer", product.Manufacturer);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id_pro,Pro_Name,Des,Id_manufacturer,Id_Category,Price,Ram,Rom")] Product product)
        {
            if (id != product.Id_pro)
            {
                return NotFound();
            }

            if (true)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id_pro))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["Category"] = new SelectList(_context.Category, "Id_Category", "Name_Category", product.Category);

            ViewData["Manufacturer"] = new SelectList(_context.Manufacturers, "Id_manufacturer", "Name_manufacturer", product.Manufacturer);
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Product == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .Include(p => p.Category)
                .Include(p => p.Manufacturer)
                .FirstOrDefaultAsync(m => m.Id_pro == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Product == null)
            {
                return Problem("Entity set 'MyDBConext.Product'  is null.");
            }
            var product = await _context.Product.FindAsync(id);
            if (product != null)
            {
                _context.Product.Remove(product);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Search(string key)
        {
            var myDBConext = from p in _context.Product.Include(p => p.Category).Include(p => p.Manufacturer).Include(p => p.Images) where EF.Functions.Like(p.Pro_Name, "%" + key + "%") select p;
            return View(await myDBConext.ToListAsync());

        }
        private bool ProductExists(int id)
        {
            return (_context.Product?.Any(e => e.Id_pro == id)).GetValueOrDefault();
        }
        DAO dao = new DAO();
        public IActionResult resultSearch()
        {
            List<Order> orders = dao.getOrderAll();
            ViewBag.listorder = orders;
            return View();
        }
        //public IActionResult phantrang(int id)
        //{
            
        //}


    }
}
