using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace bookstore.Models
{
    [MetadataType(typeof(mdCarts))]
    public partial class Carts
    {
        [Display(Name = "目前數量")] 
        public int qty_now { get { using (tblBooks model = new tblBooks()) { return model.GetBooksQtyNow(product_no); } } }

        private class mdCarts
        {
            public int rowid { get; set; }
            public string lot_no { get; set; }
            public string user_no { get; set; }
            public string product_no { get; set; }
            public string product_name { get; set; }
            public string product_spec { get; set; }
            public Nullable<int> qty { get; set; }
            public Nullable<int> price { get; set; }
            public Nullable<System.DateTime> create_time { get; set; }
            public Nullable<int> amount { get; set; }
        }
    }
}