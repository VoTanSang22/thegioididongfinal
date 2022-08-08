using Microsoft.AspNetCore.Mvc;
using thegioididong.Models;
using thegioididong.Models.DAO;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;

namespace thegioididong.Controllers
{
    public class OrdersController : Controller
    {
        DAO dao = new DAO();
        public IActionResult addtoCart(string id)
        {
            CookieOptions cookieOptions = new CookieOptions();
            cookieOptions.Expires = new DateTimeOffset(DateTime.Now.AddDays(7));
            var _p = dao.getProductbyId(id);

            var product = new Product
            {
                Id_pro = _p.Id_pro,               
                Pro_Name = _p.Pro_Name,
                Amount = _p.Amount,
                Price = _p.Price
            };

            string oldCart = Request.Cookies["cart"];
            var cart = JsonSerializer.Deserialize<List<Product>>(oldCart ?? "[]")!;

            cart.Add(product);

            var cartJson = JsonSerializer.Serialize(cart);
            Console.WriteLine(cartJson);

            Response.Cookies.Append("cart", cartJson, cookieOptions);

            return RedirectToAction("showCart");
        }
        private readonly MyDBConext _context;
        public OrdersController(MyDBConext context)
        {
            _context = context;
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
                .Include(p => p.Comments)
                .FirstOrDefaultAsync(m => m.Id_pro == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }
        public async Task<IActionResult> comment(int id_pro, string cusName, string cusPhone, string cusMail, string noidung)
        {
            if (true)
            {
                Comment comment = new Comment { Id_pro = id_pro, Name_cus = cusName, phone = cusPhone, mail = cusMail, content = noidung };

                _context.Add(comment);
                await _context.SaveChangesAsync();
                return RedirectToRoute(new { Controller = "Home", Action = "Details", ID = id_pro });
            }
            //ViewData["Id_pro"] = new SelectList(_context.Product, "Id_pro", "Id_pro", comment.Id_pro);
            return View(comment);
        }
        public async Task<IActionResult> Search(string key)
        {
            var myDBConext = from p in _context.Product.Include(p => p.Category).Include(p => p.Manufacturer).Include(p => p.Images) where EF.Functions.Like(p.Pro_Name, "%" + key + "%") select p;
            return View(await myDBConext.ToListAsync());

        }
        public async Task<IActionResult> showmore(string id)
        {
            var myDBConext = from p in _context.Product.Include(p => p.Category).Include(p => p.Manufacturer).Include(p => p.Images) where p.Category.Name_Category == id select p;
            return View(await myDBConext.ToListAsync());


        }
        public IActionResult showCart()
        {
            string cartJson = Request.Cookies["cart"] ?? "[]";
            var cart = JsonSerializer.Deserialize<List<Product>>(cartJson)!;
            ViewBag.listcart = cart;
            return View();
        }

        public IActionResult viewOrder()
        {
            string cartJson = Request.Cookies["cart"] ?? "[]";
            var cart = JsonSerializer.Deserialize<List<Product>>(cartJson)!;
            ViewBag.namecus = TempData["namecus"];
            ViewBag.phone = TempData["phone"];
            ViewBag.address = TempData["address"];



            // 3. View current order
            // cart = read from db
            ViewBag.listcart = cart;
            return View();
        }
        /*   public IActionResult buyHistory(string phone)
           {
               productDAO.getOrderByPhone(phone);
               return View();
           }*/

        public IActionResult deleteCartItem(int idx)
        {
            string oldcart = Request.Cookies["cart"] ?? "[]";
            var cart = JsonSerializer.Deserialize<List<Product>>(oldcart)!;
            cart.RemoveAt(idx);

            var cartJson = JsonSerializer.Serialize(cart);
            CookieOptions cookieOptions = new CookieOptions();
            cookieOptions.Expires = new DateTimeOffset(DateTime.Now.AddDays(7));
            Response.Cookies.Append("cart", cartJson, cookieOptions);
            return RedirectToAction("showCart");
        }
        public IActionResult check_out(string namecus, string phone, string address)
        {
            TempData["namecus"] = namecus;
            TempData["phone"] = phone;
            TempData["address"] = address;

            return RedirectToAction("viewOrder");
        }
        public IActionResult historyOrder()
        {
            return View();
        }
        public IActionResult resultSearch(string txtPhoneNumber)
        {
            List<Order> orders = dao.getOrderByPhone(txtPhoneNumber);
            ViewBag.listorder = orders;
            return View();
        }

        public IActionResult dathang(string namecus, string phone, string address)
        {
            string cartJson = Request.Cookies["cart"] ?? "[]";
            var cart = JsonSerializer.Deserialize<List<Product>>(cartJson)!;
            foreach (var order in cart)
            {
                dao.Check_Out(namecus, phone, address, order);
            }
            // 1. update database 'order'
            // 2. Remove cart in cookie
            Response.Cookies.Delete("cart");
            return RedirectToAction("showCart");
        }
    }
}
