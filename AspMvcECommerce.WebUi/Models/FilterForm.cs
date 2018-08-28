using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AspMvcECommerce.WebUi.Models
{
    public class FilterForm
    {
        public enum OrderBy {
            sortPriceDesc
            , sortPriceAsc
        }

        public int[] categories { get; set; }
        public OrderBy sort { get; set; }
    }
}