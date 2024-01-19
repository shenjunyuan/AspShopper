using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace bookstore.Models
{
    [MetadataType(typeof(mdApplications))] // 改 mdTableName
    public partial class Applications : BaseClass // TableName
    {
        public class mdApplications // mdTableName
        {
            // Table 欄位宣告
            public int rowid { get; set; }
            public string shop_name { get; set; }
            public string banner_header { get; set; }
            public string banner_description { get; set; }
            public string shipping_description { get; set; }
            public string return_description { get; set; }
            public string support_description { get; set; }
            public string contact_address { get; set; }
            public string contact_tel { get; set; }
            public string contact_email { get; set; }
            public string shop_about { get; set; }
        }
    }
}





