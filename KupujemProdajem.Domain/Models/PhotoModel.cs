using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KupujemProdajem.Domain.Models
{
    public class PhotoModel
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public int AdId { get; set; }
        public AdModel Ad { get; set; }
    }
}
