using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace bookstore.Models
{
    [MetadataType(typeof(mdCarts))]
    public partial class Carts : BaseClass
    {
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

        /// <summary>
        /// 取得購物車 row_id 的 product_no
        /// </summary>
        /// <param name="rowId"></param>
        /// <returns></returns>
        public string GetProductNo(string rowId)
        {
            string prod_no = "";
            string query = $"SELECT  [product_no] FROM [Carts] where rowid = @row_id ";
            prod_no = DapperHelp.GetTable<string>(query, new { @row_id = rowId }).FirstOrDefault().ToString();
                return prod_no;     
        }
    }
}