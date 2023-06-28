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

namespace WebBanHang.Pages.Products
{
    public class IndexModel : PageModel
    {
        private readonly WebBanHang.Data.ApplicationDbContext _context;

        public IndexModel(WebBanHang.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<SanPham> SanPham { get;set; } = default!;
        [BindProperty(SupportsGet = true)]
        public string? SearchString { get; set; }
        public SelectList? Types { get; set; }
        [BindProperty(SupportsGet = true)]
        public string? ProductType { get; set; }

        public async Task OnGetAsync()
        {
            IQueryable<string> typeQuery = from m in _context.SanPham
                                           orderby m.Type
                                           select m.Type;
            var products = from m in _context.SanPham
                           select m;
            if (!string.IsNullOrEmpty(SearchString))
            {
                products = products.Where(s => s.Name.Contains(SearchString));
            }
            Types = new SelectList(await typeQuery.Distinct().ToListAsync());
            if (!string.IsNullOrEmpty(ProductType))
            {
                products = products.Where(x => x.Type == ProductType);
            }
            SanPham = await products.ToListAsync();
        }
    }
}
