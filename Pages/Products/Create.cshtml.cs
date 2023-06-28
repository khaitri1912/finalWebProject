using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebBanHang.Data;
using WebBanHang.Models;

namespace WebBanHang.Pages.Products
{
    public class CreateModel : PageModel
    {
        private readonly WebBanHang.Data.ApplicationDbContext _context;

        public CreateModel(WebBanHang.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public SanPham SanPham { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.SanPham == null || SanPham == null)
            {
                return Page();
            }

            _context.SanPham.Add(SanPham);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
