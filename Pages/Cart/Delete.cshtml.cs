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
    public class DeleteModel : PageModel
    {
        private readonly WebBanHang.Data.ApplicationDbContext _context;

        public DeleteModel(WebBanHang.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.GioHang == null)
            {
                return NotFound();
            }
            var giohang = await _context.GioHang.FindAsync(id);

            if (giohang != null)
            {
                GioHang = giohang;
                _context.GioHang.Remove(GioHang);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
