using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebBanHang.Data;
using WebBanHang.Models;

namespace WebBanHang.Pages.CartHandling
{
    public class DeleteModel : PageModel
    {
        private readonly WebBanHang.Data.ApplicationDbContext _context;

        public DeleteModel(WebBanHang.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
      public GioHangDangXuLy GioHangDangXuLy { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.GioHangDangXuLy == null)
            {
                return NotFound();
            }

            var giohangdangxuly = await _context.GioHangDangXuLy.FirstOrDefaultAsync(m => m.ID == id);

            if (giohangdangxuly == null)
            {
                return NotFound();
            }
            else 
            {
                GioHangDangXuLy = giohangdangxuly;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.GioHangDangXuLy == null)
            {
                return NotFound();
            }
            var giohangdangxuly = await _context.GioHangDangXuLy.FindAsync(id);

            if (giohangdangxuly != null)
            {
                GioHangDangXuLy = giohangdangxuly;
                _context.GioHangDangXuLy.Remove(GioHangDangXuLy);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
