using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KupujemProdajem.Domain.Models
{
    public class AdModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? UserId { get; set; }
        public UserModel? User { get; set; }
        public int CategoryId { get; set; }
        public CategoryModel Category { get; set; }
        //public string? ImageUrl { get; set; }
    }
}
