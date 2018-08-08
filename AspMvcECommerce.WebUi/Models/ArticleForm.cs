using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AspMvcECommerce.WebUi.Models
{
    public class ArticleForm
    {
        public string Title { get; set; }
        public int Categoryid { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string ImageBase64 { get; set; }
    }
}