using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

public class vmRegister
{
    [Display(Name = "會員名稱")]
    [Required(ErrorMessage = "會員名稱不可空白!!")]
    public string AccountName { get; set; }
    [Display(Name = "登入密碼")]
    [Required(ErrorMessage = "登入密碼不可空白!!")]
    [DataType(DataType.Password)]
    public string Password { get; set; }
    [Display(Name = "確認密碼")]
    [Required(ErrorMessage = "確認密碼不可空白!!")]
    [DataType(DataType.Password)]
    [System.ComponentModel.DataAnnotations.Compare("Password" , ErrorMessage ="驗證密碼不正確!!") ]
    public string ConfirmPassword { get; set; }
    [Display(Name = "會員性別")]
    public string GenderCode { get; set; }
    [Display(Name = "出生日期")]
    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd}")]
    public DateTime Birthday { get; set; }
    [Display(Name = "電子信箱")]
    [Required(ErrorMessage = "電子信箱不可空白!!")]
    [DataType(DataType.EmailAddress)]
    public string ContactEmail { get; set; }
    [Display(Name = "聯絡電話")]
    public string ContactPhone { get; set; }
    [Display(Name = "聯絡地址")]
    public string ContactAddress { get; set; }

    public List<SelectListItem> GetGenderCodeList()
    {
        return new List<SelectListItem>() {
            new SelectListItem() { Text = "男", Value = "M" },
            new SelectListItem() { Text = "女", Value = "F" }
        };
    }
}
