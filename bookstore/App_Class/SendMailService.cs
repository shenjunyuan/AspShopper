using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using bookstore.Models;

public class SendMailService : BaseClass
{
    /// <summary>
    /// 會員註冊寄發驗證的電子郵件
    /// </summary>
    /// <param name="validateCode">驗證碼</param>
    /// <returns></returns>
    public string MemberRegister(string validateCode)
    {
        using (bookstoreEntities db = new bookstoreEntities())
        {
            using (GmailService gmail = new GmailService())
            {
                //驗證
                var memberData = db.Members.Where(m => m.validate_code == validateCode).FirstOrDefault();
                if (memberData == null) { return "查無驗證碼!!"; }
                bool bln_validate = (memberData.is_validate == null) ? false : memberData.is_validate.GetValueOrDefault();
                if (bln_validate) { return "此驗證碼已通過驗證!!"; }
                if (string.IsNullOrEmpty(memberData.contact_email)) { return "此會員未輸入電子信箱!!"; }
                if (string.IsNullOrEmpty(SessionService.WebSiteUrl)) { return "Web.config 未設定 WebSiteUrl 參數!!"; }
                //變數
                string str_member_no = memberData.member_no;
                string str_member_name = memberData.member_name;
                string str_member_email = memberData.contact_email;
                string str_reg_date = DateTime.Now.ToString("yyyy/MM/dd HH:mm");
                string str_url = SessionService.WebSiteUrl;
                string str_validate_url = string.Format("{0}/Account/ValidateCode/{1}", str_url, validateCode);
                //信件內容
                gmail.ReceiveEmail = str_member_email;
                gmail.Subject = string.Format("{0} 會員註冊驗證通知信", SessionService.AppName);
                gmail.Body = string.Format("敬愛的會員 {0} 您好!! <br /><br />", str_member_name);
                gmail.Body += string.Format("您於 {0} 在我們網站註冊了會員帳號<br />", str_reg_date);
                gmail.Body += string.Format("您的會員帳號為：{0}<br />", str_member_no);
                gmail.Body += "請您點擊以下連結進行帳號電子郵件驗證<br /><br />";
                gmail.Body += string.Format("<a href=\"{0}\" target=\"_blank\">{1}</a><br /><br />", str_validate_url, str_validate_url);
                gmail.Body += "本信件為系統自動寄出,請勿回覆!!<br /><br />";
                gmail.Body += "-------------------------------------------<br />";
                gmail.Body += string.Format("{0}<br />", SessionService.AppName);
                gmail.Body += string.Format("{0}<br />", str_url);
                gmail.Body += "-------------------------------------------<br />";
                //寄信
                gmail.Send();
                return gmail.MessageText;
            }
        }
    }

    /// <summary>
    /// 帳號忘記密碼寄發密碼重設的電子郵件
    /// </summary>
    /// <param name="validateCode">驗證碼</param>
    /// <returns></returns>
    public string AccountForget(string emailAddress, string validateCode, string accountName)
    {
        using (GmailService gmail = new GmailService())
        {
            if (string.IsNullOrEmpty(SessionService.WebSiteUrl)) { return "Web.config 未設定 WebSiteUrl 參數!!"; }
            //變數
            string str_reg_date = DateTime.Now.ToString("yyyy/MM/dd HH:mm");
            string str_url = SessionService.WebSiteUrl;
            string str_validate_url = string.Format("{0}/Account/ValidateForgetCode/{1}", str_url, validateCode);
            //信件內容
            gmail.ReceiveEmail = emailAddress;
            gmail.Subject = string.Format("{0} 帳號忘記密碼重新設定通知信", SessionService.AppName);
            gmail.Body = string.Format("敬愛的會員 {0} 您好!! <br /><br />", accountName);
            gmail.Body += string.Format("您於 {0} 在我們網站執行了忘記密碼的功能，<br />", str_reg_date);
            gmail.Body += "請您點擊以下連結進行忘記密碼重新設定<br /><br />";
            gmail.Body += string.Format("<a href=\"{0}\" target=\"_blank\">{1}</a><br /><br />", str_validate_url, str_validate_url);
            gmail.Body += "本信件為系統自動寄出,請勿回覆!!<br /><br />";
            gmail.Body += "-------------------------------------------<br />";
            gmail.Body += string.Format("{0}<br />", SessionService.AppName);
            gmail.Body += string.Format("{0}<br />", str_url);
            gmail.Body += "-------------------------------------------<br />";
            //寄信
            gmail.Send();
            return gmail.MessageText;
        }
    }
}
