using System.ComponentModel.DataAnnotations;

namespace WebBanHang.Models
{
    public class SanPham
    {
        public int ID { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Photo { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }
        public int SoLuong { get; set; }
        public int Price { get; set; }
        public int TotalPrice
        {
            get { return SoLuong * Price; }
        }
    }
}
