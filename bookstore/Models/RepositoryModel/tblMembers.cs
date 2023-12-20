using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using bookstore.Models;

public class tblMembers : BaseClass
{
    public IRepository<Members> repo;
    public tblMembers()
    {
        repo = new EFGenericRepository<Members>(new bookstoreEntities());
    }

    /// <summary>
    /// //空白密碼加密
    /// </summary>
    /// <param name="accountNo">帳號</param>
    /// <param name="passWord">密碼</param>
    public void EncodeEmptyPassword(string accountNo, string passWord)
    {
        using (Cryptographys cryp = new Cryptographys())
        {
            var data = repo.ReadSingle(m =>
                   (m.member_no == accountNo || m.contact_email == accountNo) && m.member_password == "");
            if (data != null)
            {
                data.member_password = cryp.SHA256Encode(accountNo);
                repo.Update(data);
                repo.SaveChanges();
            }
        }
    }

    /// <summary>
    /// 檢查是否帳號登入
    /// </summary>
    /// <param name="accountNo">帳號</param>
    /// <param name="passWord">密碼</param>
    /// <returns></returns>
    public bool CheckAccountLogin(string accountNo, string passWord)
    {
        SessionService.AccountNo = "";
        SessionService.AccountName = "";
        SessionService.RoleNo = "";
        SessionService.RoleName = "";
        SessionService.IsLogined = false;

        var data = repo.ReadSingle(m =>
                 (m.member_no == accountNo || m.contact_email == accountNo) &&
                 m.member_password == passWord &&
                 m.is_validate == true);
        if (data != null)
        {
            SessionService.AccountNo = data.member_no;
            SessionService.AccountName = data.member_name;
            SessionService.RoleNo = "Member";
            SessionService.RoleName = "會員";
            SessionService.IsLogined = true;
            return true;
        }
        return false;
    }

    /// <summary>
    /// 檢查電子郵件是否存在
    /// </summary>
    /// <param name="emailAddress">電子郵件</param>
    /// <param name="validateCode">驗證碼</param>
    /// <param name="userName">名稱</param>
    /// <returns></returns>
    public bool CheckEmailExists(string emailAddress, string validateCode, out string userName)
    {
        var data = repo.ReadSingle(m => m.contact_email == emailAddress);
        if (data != null)
        {
            userName = data.member_name;
            data.validate_code = validateCode;
            repo.Update(data);
            repo.SaveChanges();
            return true;
        }
        userName = "";
        return false;
    }

    /// <summary>
    /// 依據驗證碼重設新密碼
    /// </summary>
    /// <param name="validateCode"></param>
    /// <returns></returns>
    public string ForgetPasswordReset(string validateCode)
    {
        string str_password = "";
        var data = repo.ReadSingle(m => m.validate_code == validateCode);
        if (data != null)
        {
            using (Cryptographys cryp = new Cryptographys())
            {
                //亂數產生一組8位數的密碼
                str_password = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 8).ToUpper();
                //寫回資料庫中
                data.member_password = cryp.SHA256Encode(str_password);
                data.validate_code = "";
                repo.Update(data);
                repo.SaveChanges();
            }
        }
        return str_password;
    }

    /// <summary>
    /// 取得會員 Profile 資訊
    /// </summary>
    /// <returns></returns>
    public vmAccountProfile GetAccountProfile()
    {
        vmAccountProfile model = new vmAccountProfile();
        var data = repo.ReadSingle(m => m.member_no == SessionService.AccountNo);
        if (data != null)
        {
            model.AccountNo = data.member_no;
            model.AccountName = data.member_name;
            model.GenderCode = data.gender_code;
            model.BirthDate = data.birth_date;
            model.ContactEmail = data.contact_email;
            model.ContactPhone = data.contact_phone;
            model.ContactZip = data.contact_zip;
            model.ContactAddress = data.contact_address;
        }
        return model;
    }

    /// <summary>
    /// 更新會員 Profile 資訊
    /// </summary>
    /// <param name="model">會員 Profile 資訊</param>
    public void UpdateAccountProfile(vmAccountProfile model)
    {
        var data = repo.ReadSingle(m => m.member_no == SessionService.AccountNo);
        if (data != null)
        {
            data.gender_code = model.GenderCode;
            data.birth_date = model.BirthDate;
            data.contact_email = model.ContactEmail;
            data.contact_phone = model.ContactPhone ;
            data.contact_zip = model.ContactZip;
            data.contact_address = model.ContactAddress;
            repo.Update(data);
            repo.SaveChanges();
        }
    }

    /// <summary>
    /// 變更帳號密碼
    /// </summary>
    /// <param name=""></param>
    public void ResetAccountPassword(vmResetPassword model)
    {
        using (Cryptographys cryp = new Cryptographys())
        {
            string str_password = cryp.SHA256Encode(model.NewPassword);
            var data = repo.ReadSingle(m => m.member_no == SessionService.AccountNo);
            if (data != null)
            {
                data.member_password = str_password;
                repo.Update(data);
                repo.SaveChanges();
            }
        }
    }

    /// <summary>
    /// 檢查密碼是否正確
    /// </summary>
    /// <param name="passWord">密碼</param>
    /// <returns></returns>
    public bool ValidAccountPassword(string passWord)
    {
        bool bln_valid = false;
        var data = repo.ReadSingle(m => m.member_no == SessionService.AccountNo);
        if (data != null) { if (data.member_password == passWord) bln_valid = true; }
        return bln_valid;
    }
}
