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

namespace WebBanHang.Pages.Cart
{
	[Authorize(Roles = "Customer")]
	public class IndexModel : PageModel
    {
        private readonly WebBanHang.Data.ApplicationDbContext _context;

        public IndexModel(WebBanHang.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<GioHang> GioHang { get;set; } = default!;
        public int TotalPrice { get; set; }
        public IList<GioHangDangXuLy>? CartHandling { get; set; } = new List<GioHangDangXuLy>();
        public async Task OnGetAsync()
        {
            GioHang = new List<GioHang>(); // Khởi tạo GioHang là một danh sách trống
            TotalPrice = 0; // Khởi tạo TotalPrice là 0

            if (_context.GioHang != null)
            {
                var gioHangItems = await _context.GioHang.ToListAsync();

                if (gioHangItems != null && gioHangItems.Any()) // Kiểm tra nếu gioHangItems không null và có phần tử
                {
                    var newCart = new List<GioHang>();

                    foreach (var item in gioHangItems)
                    {
                        var existingItem = newCart.FirstOrDefault(x => x.Name == item.Name);

                        if (existingItem != null)
                        {
                            existingItem.SoLuong += item.SoLuong;
                        }
                        else
                        {
                            newCart.Add(item);
                        }
                    }

                    GioHang = newCart;
                    TotalPrice = GioHang.Sum(item => item.TotalPrice);
                }
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (_context.GioHang != null)
            {
                GioHang = await _context.GioHang.ToListAsync();
            }

            CartHandling = GioHang.Select(g => new GioHangDangXuLy
            {
                Name = g.Name,
                Photo = g.Photo,
                Type = g.Type,
                Description = g.Description,
                SoLuong = g.SoLuong,
                ReleaseDate = g.ReleaseDate,
                Price = g.Price
            }).ToList();

            _context.GioHangDangXuLy.AddRange(CartHandling);

            _context.GioHang.RemoveRange(GioHang);

            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
