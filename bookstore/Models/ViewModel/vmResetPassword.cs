using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

public class vmResetPassword
{
    [Display(Name ="現在密碼")]
    [DataType(DataType.Password)]
    [Required(ErrorMessage = "現在密碼不可空白!!")]
    public string OldPassword { get; set; }
    [Display(Name = "新的密碼")]
    [DataType(DataType.Password)]
    [Required(ErrorMessage = "新的密碼不可空白!!")]
    [StringLength(50 , MinimumLength = 4, ErrorMessage = "密碼不可小於 4 個字!!")]
    public string NewPassword { get; set; }
}