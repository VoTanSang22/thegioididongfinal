using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using thegioididong.Models;

namespace thegioididong.Controllers
{
    public class ImagesController : Controller
    {
        private readonly MyDBConext _context;

        public ImagesController(MyDBConext context)
        {
            _context = context;
        }

        // GET: Images
        public async Task<IActionResult> Index()
        {
            var myDBConext = _context.Images.Include(i => i.Product);
            return View(await myDBConext.ToListAsync());
        }
        public ActionResult Logout()
        {
            HttpContext.Session.Clear();//remove session
            return RedirectToAction("Login");
        }
        // GET: Images/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Images == null)
            {
                return NotFound();
            }

            var image = await _context.Images
                .Include(i => i.Product)
                .FirstOrDefaultAsync(m => m.Id_image == id);
            if (image == null)
            {
                return NotFound();
            }

            return View(image);
        }

        // GET: Images/Create
        public IActionResult Create()
        {
            Image image = new Image();
            ViewData["Product"] = new SelectList(_context.Product, "Id_pro", "Pro_Name", image.Product);
            return View();
        }

        // POST: Images/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id_image,link_image,Id_pro")] Image image)
        {
            if (true)
            {
                _context.Add(image);
                await _context.SaveChangesAsync();
                return RedirectToRoute(new {Controller ="Products" , Action ="Index"});
            }
            ViewData["Product"] = new SelectList(_context.Product, "Id_pro", "Pro_Name", image.Product);
            return View(image);
        }

        // GET: Images/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Images == null)
            {
                return NotFound();
            }

            var image = await _context.Images.FindAsync(id);
            if (image == null)
            {
                return NotFound();
            }
            ViewData["Id_pro"] = new SelectList(_context.Product, "Id_pro", "Id_pro", image.Id_pro);
            return View(image);
        }

        // POST: Images/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id_image,link_image,Id_pro")] Image image)
        {
            if (id != image.Id_image)
            {
                return NotFound();
            }

            if (true)
            {
                try
                {
                    _context.Update(image);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ImageExists(image.Id_image))
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
            ViewData["Id_pro"] = new SelectList(_context.Product, "Id_pro", "Id_pro", image.Id_pro);
            return View(image);
        }

        // GET: Images/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Images == null)
            {
                return NotFound();
            }

            var image = await _context.Images
                .Include(i => i.Product)
                .FirstOrDefaultAsync(m => m.Id_image == id);
            if (image == null)
            {
                return NotFound();
            }

            return View(image);
        }

        // POST: Images/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Images == null)
            {
                return Problem("Entity set 'MyDBConext.Images'  is null.");
            }
            var image = await _context.Images.FindAsync(id);
            if (image != null)
            {
                _context.Images.Remove(image);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ImageExists(int id)
        {
          return (_context.Images?.Any(e => e.Id_image == id)).GetValueOrDefault();
        }
    }
}
