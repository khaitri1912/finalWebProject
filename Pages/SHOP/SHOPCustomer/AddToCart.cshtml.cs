using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebBanHang.Models;

namespace WebBanHang.Pages.SHOP.SHOPCustomer
{
    public class AddToCartModel : PageModel
    {
        private readonly WebBanHang.Data.ApplicationDbContext _context;

        public AddToCartModel(WebBanHang.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public SanPham Product { get; set; } = default!;
        [BindProperty]
        public int Quantity { get; set; } = 1;

        public GioHang productInCart { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.SanPham == null)
            {
                return NotFound();
            }

            var product = await _context.SanPham.FirstOrDefaultAsync(m => m.ID == id);
            if (product == null)
            {
                return NotFound();
            }
            Product = product;

            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var product = await _context.SanPham.FirstOrDefaultAsync(m => m.ID == Product.ID);
            if (product == null)
            {
                return NotFound();
            }
            Product = product;

            try
            {
                await _context.SaveChangesAsync();

                var productInCart = new GioHang
                {
                    Name = Product.Name,
                    Photo = Product.Photo,
                    Type = Product.Type,
                    Description = Product.Description,
                    SoLuong = Quantity,
                    Price = Product.Price,
                    ReleaseDate = Product.ReleaseDate
                };

                _context.GioHang.Add(productInCart);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(Product.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool ProductExists(int id)
        {
            return (_context.SanPham?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
