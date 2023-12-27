using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace bookstore.Models
{
    [MetadataType(typeof(mdCategorys))]
    public partial class Categorys
    {
        public int book_counts { get; set; }
        private class mdCategorys
        {
            public int rowid { get; set; }
            public bool is_enabled { get; set; }
            public string parentno { get; set; }
            public string mno { get; set; }
            public string mname { get; set; }
            public string remark { get; set; }
        }
    }
}
