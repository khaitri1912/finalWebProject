using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebBanHang.Data;
using WebBanHang.Models;

namespace WebBanHang.Pages.Cart
{
    public class DetailsModel : PageModel
    {
        private readonly WebBanHang.Data.ApplicationDbContext _context;

        public DetailsModel(WebBanHang.Data.ApplicationDbContext context)
        {
            _context = context;
        }

      public GioHang GioHang { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.GioHang == null)
            {
                return NotFound();
            }

            var giohang = await _context.GioHang.FirstOrDefaultAsync(m => m.ID == id);
            if (giohang == null)
            {
                return NotFound();
            }
            else 
            {
                GioHang = giohang;
            }
            return Page();
        }
    }
}
