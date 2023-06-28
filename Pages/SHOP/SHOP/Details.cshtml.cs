using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebBanHang.Models;
using Microsoft.EntityFrameworkCore;

namespace WebBanHang.Pages.SHOP.SHOP
{
    public class DetailsModel : PageModel
    {
        private readonly WebBanHang.Data.ApplicationDbContext _context;

        public DetailsModel(WebBanHang.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public SanPham Product { get; set; } = default!;

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
            else
            {
                Product = product;
            }
            return Page();
        }
    }
}
