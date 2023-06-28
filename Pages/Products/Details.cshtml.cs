using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebBanHang.Data;
using WebBanHang.Models;

namespace WebBanHang.Pages.Products
{
    public class DetailsModel : PageModel
    {
        private readonly WebBanHang.Data.ApplicationDbContext _context;

        public DetailsModel(WebBanHang.Data.ApplicationDbContext context)
        {
            _context = context;
        }

      public SanPham SanPham { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.SanPham == null)
            {
                return NotFound();
            }

            var sanpham = await _context.SanPham.FirstOrDefaultAsync(m => m.ID == id);
            if (sanpham == null)
            {
                return NotFound();
            }
            else 
            {
                SanPham = sanpham;
            }
            return Page();
        }
    }
}
