using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace bookstore.Models.MetadataModel
{
    [MetadataType(typeof(mdApplicationBanner))]
    public partial class ApplicationBanner : BaseClass
    {
        private class mdApplicationBanner
        {
            [Key]
            public int rowid { get; set; }
            [Display(Name = "顯示")]
            public bool is_show { get; set; }
            [Display(Name = "Banner名稱")]
            [Required(ErrorMessage = "Banner名稱不可空白!!")]
            public string banner_name { get; set; }
            [Display(Name = "商店名稱")]
            public string shop_name { get; set; }
            [Display(Name = "備註")]
            public string ramark { get; set; }
        }
    }



}