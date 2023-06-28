using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebBanHang.Data;
using WebBanHang.Models;

namespace WebBanHang.Pages.CartHandling
{
    [Authorize(Roles = "Admin")]
    public class IndexModel : PageModel
    {
        private readonly WebBanHang.Data.ApplicationDbContext _context;

        public IndexModel(WebBanHang.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<GioHangDangXuLy> GioHangDangXuLy { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.GioHangDangXuLy != null)
            {
                GioHangDangXuLy = await _context.GioHangDangXuLy.ToListAsync();
            }
        }
    }
}
