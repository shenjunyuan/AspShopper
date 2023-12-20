namespace bookstore.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    [MetadataType(typeof(mdAdmins))]
    public partial class Admins
    {
        private class mdAdmins
        {
            [Key]
            public int rowid { get; set; }
            [Display(Name ="會員驗證")]
            public bool is_validate { get; set; }
            [Display(Name = "登入代號")]
            [Required(ErrorMessage = "登入代號不可空白!!")]
            public string admin_no { get; set; }
            [Display(Name = "登入名稱")]
            [Required(ErrorMessage = "登入名稱不可空白!!")]
            public string admin_name { get; set; }
            [Display(Name = "登入密碼")]
            [Required(ErrorMessage = "登入密碼不可空白!!")]
            [DataType(DataType.Password)]
            public string admin_password { get; set; }
            [Display(Name = "電子信箱")]
            [Required(ErrorMessage = "電子信箱不可空白!!")]
            [DataType(DataType.EmailAddress)]
            public string contact_email { get; set; }
            [Display(Name = "驗證碼")]
            public string validate_code { get; set; }
            [Display(Name = "備註說明")]
            public string remark { get; set; }
        }
    }
}