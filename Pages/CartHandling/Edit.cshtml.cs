using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebBanHang.Data;
using WebBanHang.Models;

namespace WebBanHang.Pages.CartHandling
{
    public class EditModel : PageModel
    {
        private readonly WebBanHang.Data.ApplicationDbContext _context;

        public EditModel(WebBanHang.Data.ApplicationDbContext context)
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

            var giohangdangxuly =  await _context.GioHangDangXuLy.FirstOrDefaultAsync(m => m.ID == id);
            if (giohangdangxuly == null)
            {
                return NotFound();
            }
            GioHangDangXuLy = giohangdangxuly;
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

            _context.Attach(GioHangDangXuLy).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GioHangDangXuLyExists(GioHangDangXuLy.ID))
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

        private bool GioHangDangXuLyExists(int id)
        {
          return (_context.GioHangDangXuLy?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
