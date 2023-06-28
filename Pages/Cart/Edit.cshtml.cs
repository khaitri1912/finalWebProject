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

namespace WebBanHang.Pages.Cart
{
    public class EditModel : PageModel
    {
        private readonly WebBanHang.Data.ApplicationDbContext _context;

        public EditModel(WebBanHang.Data.ApplicationDbContext context)
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

            var giohang =  await _context.GioHang.FirstOrDefaultAsync(m => m.ID == id);
            if (giohang == null)
            {
                return NotFound();
            }
            GioHang = giohang;
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

            _context.Attach(GioHang).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GioHangExists(GioHang.ID))
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

        private bool GioHangExists(int id)
        {
          return (_context.GioHang?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
