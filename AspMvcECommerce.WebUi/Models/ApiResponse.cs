using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AspMvcECommerce.WebUi.Models
{
    public class ApiResponse
    {
        public IList data { get; set; }
        public string error { get; set; }
    }
}