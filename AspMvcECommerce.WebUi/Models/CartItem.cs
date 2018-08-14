using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AspMvcECommerce.WebUi.Models
{
    public class CartItem
    {
        public int ArticleId { get; set; }
        public int Count { get; set; }
    }
}