using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace bookstore.Models
{
    [MetadataType(typeof(mdFeatureds))]
    public partial class Featureds : BaseClass
    {
        /// <summary>
        /// 額外追加的欄位 Featureds Table 裡沒有的 虛擬欄位
        /// </summary>
        public string product_name { get; set; }
        public string category_name { get; set; }
        public int sale_price { get; set; }
        private class mdFeatureds
        {
            [Key]
            public int rowid { get; set; }
            public bool is_enabled { get; set; }
            public string sort_no { get; set; }
            public string product_no { get; set; }
        }
    }
}
