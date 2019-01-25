using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication4.Models
{
    public class Album
    {
        public int AlbumId { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }

    }
}