using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Web;
using System.Web.Mvc;
using bookstore.Models;
using Dapper;

namespace bookstore.Controllers
{
    public class AccountController : Controller
    {
        [AllowAnonymous]
        [HttpGet]
        public ActionResult Login()
        {         
            SessionService.Logout();
            vmLogin model = new vmLogin();

            return View(model);
        }


        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login(vmLogin model)
        {
            if (!ModelState.IsValid) return View(model);
            using (tblAdmins admins = new tblAdmins())
            {
                using (tblMembers members = new tblMembers())
                {
                    using (tblUsers users = new tblUsers())
                    {
                        using (Cryptographys cryp = new Cryptographys())
                        {
                            // 空白密碼加密
                            admins.EncodeEmptyPassword(model.AccountNo, model.Password);
                            members.EncodeEmptyPassword(model.AccountNo, model.Password);
                            users.EncodeEmptyPassword(model.AccountNo, model.Password);

                            // 檢查登入角色
                            string str_password = cryp.SHA256Encode(model.Password);
                            if (admins.CheckAccountLogin(model.AccountNo, str_password))  return RedirectToAction("Index", "Admin");
                            if (users.CheckAccountLogin(model.AccountNo, str_password))  return RedirectToAction("Index", "Admin");
                            if (members.CheckAccountLogin(model.AccountNo, str_password))
                            {
                                SessionService.AccountNo = model.AccountNo;
                                // 登入時將現有遊客的購物車加入客戶的購物車
                                CarPage.LoginCart();                               
                                return RedirectToAction("Index", "Home");
                            }
                           
                            //引發一段錯誤訊息
                            ModelState.AddModelError("AccountNo", "帳號或密碼輸入錯誤!!");
                            return View(model);
                        }
                    }
                }
            }
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult Register()
        {
            vmRegister model = new vmRegister();
            return View(model);
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Register(vmRegister model)
        {
            if (!ModelState.IsValid) return View(model);
            using (bookstoreEntities db = new bookstoreEntities())
            {
                using (Cryptographys crpy = new Cryptographys())
                {
                    //檢查電子信箱重覆註冊
                    var memberData = db.Members
                        .Where(m => m.contact_email == model.ContactEmail).FirstOrDefault();
                    if (memberData != null)
                    {
                        ModelState.AddModelError("ContactEmail", "此電子郵件已有註冊記錄!!");
                        return View(model);
                    }

                    //新增未驗證信箱的會員資料
                    string str_validate_code = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 20);
                    string str_password = crpy.SHA256Encode(model.Password);

                    Members newData = new Members();
                    newData.member_name = model.AccountName;
                    newData.member_password = str_password;
                    newData.gender_code = model.GenderCode;
                    newData.birth_date = model.Birthday;
                    newData.contact_email = model.ContactEmail;
                    newData.contact_phone = model.ContactPhone;
                    newData.contact_address = model.ContactAddress;
                    newData.is_validate = false;
                    newData.validate_code = str_validate_code;
                    db.Members.Add(newData);
                    db.SaveChanges();

                    //寄出電子信箱驗證信
                    using (SendMailService sendMail = new SendMailService())
                    {
                        sendMail.MemberRegister(str_validate_code);
                    }

                    //顯示註冊完成並提示收信資訊
                    return RedirectToAction("Registered");
                }
            }
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult Registered()
        {
            ViewBag.MessageText = "親愛的會員您好，您的註冊完成，";
            ViewBag.MessageText += "請您記得到您的電子信箱中執行電子信箱的驗證功能";
            ViewBag.MessageText += "，以完成正式會員的資格!!";
            return View();
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult ValidateCode(string id)
        {
          
            ViewBag.MessageText = "";
            if (string.IsNullOrEmpty(id)) { ViewBag.MessageText = "驗證碼空白!!"; return View(); }
            using (bookstoreEntities db = new bookstoreEntities())
            {
                var memberData = db.Members.Where(m => m.validate_code == id).FirstOrDefault();
                //檢查是否合法驗證               
                if (memberData == null) { ViewBag.MessageText = "驗證碼不存在!!"; return View(); }
                bool bln_validate = (memberData.is_validate == null) ? false : memberData.is_validate.GetValueOrDefault();
                if (bln_validate) { ViewBag.MessageText = "會員已驗證，不可重覆驗證!!"; return View(); }
                //修改驗證狀態
                memberData.is_validate = true;
                db.SaveChanges();
                ViewBag.MessageText = "會員電子郵件已驗證成功，您可以進入登入頁登入系統!!";
                //顯示訊息畫面
                return View();
            }
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult Forget()
        {
            vmForget model = new vmForget();
            return View(model);
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Forget(vmForget model)
        {
            if (!ModelState.IsValid) return View(model);
            string str_validate_code = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 20).ToUpper();
            string str_user_name = "";
            bool bln_exists = false;

            // 檢查電子郵件是否存在，若存在就更新驗證碼
            using (tblAdmins admins = new tblAdmins())
            { bln_exists = admins.CheckEmailExists(model.AccountEmail, str_validate_code, out str_user_name); }
            if (!bln_exists)
            {
                using (tblMembers members = new tblMembers())
                { bln_exists = members.CheckEmailExists(model.AccountEmail, str_validate_code, out str_user_name); }
            }
            string str = str_user_name;
            if (!bln_exists)
            {
                using (tblUsers users = new tblUsers())
                { bln_exists = users.CheckEmailExists(model.AccountEmail, str_validate_code, out str_user_name); }
            }
            if (!bln_exists)
            {
                ModelState.AddModelError("AccountEmail", "電子信箱不存在!!");
                return View(model);
            }

            //寄出電子信箱驗證信
            using (SendMailService sendMail = new SendMailService())
            {
                sendMail.AccountForget(model.AccountEmail, str_validate_code, str_user_name);
            }

            //提示收信資訊
            return RedirectToAction("Forgeted");
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult Forgeted()
        {
            ViewBag.MessageText = "您的忘記密碼需求已核准，";
            ViewBag.MessageText += "請您到您的電子信箱中執行忘記密碼的驗證功能";
            ViewBag.MessageText += "，以完成密碼重設的目的!!";
            return View();
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult ValidateForgetCode(string id)
        {
            ViewBag.MessageText = "";
            if (string.IsNullOrEmpty(id)) { ViewBag.MessageText = "驗證碼空白!!"; return View(); }
            string str_password = "";

            using (tblAdmins admins = new tblAdmins()) { str_password = admins.ForgetPasswordReset(id); }
            if (string.IsNullOrEmpty(str_password))
                using (tblMembers members = new tblMembers()) { str_password = members.ForgetPasswordReset(id); }
            if (string.IsNullOrEmpty(str_password))
                using (tblUsers users = new tblUsers()) { str_password = users.ForgetPasswordReset(id); }
            if (!string.IsNullOrEmpty(str_password))
                ViewBag.MessageText = string.Format("您的密碼已重新設定成功，新的密碼為：{0}!!", str_password);
            else
                ViewBag.MessageText = "新的密碼重設失敗，請通知管理員!!";
            //顯示訊息畫面
            return View();

        }

        /// <summary>
        /// 我的帳號
        /// </summary>
        /// <returns></returns>
        [LoginedAuthorize()]
        [HttpGet]
        public ActionResult AccountProfile()
        {
            using (AccountPage account = new AccountPage())
            {
                ViewBag.PanelWidth = SessionService.SetPrgInfo("", "我的帳號", "帳號資訊", 6);
                vmAccountProfile model = new vmAccountProfile();
                model = account.GetAccountProfile();
                return View(model);
            }
        }

        /// <summary>
        /// 我的帳號-修改
        /// </summary>
        /// <returns></returns>
        [LoginedAuthorize()]
        [HttpGet]
        public ActionResult ProfileEdit()
        {
            using (AccountPage account = new AccountPage())
            {
                ViewBag.PanelWidth = SessionService.SetPrgInfo("", "我的帳號", "修改" , 6);
                vmAccountProfile model = new vmAccountProfile();
                model = account.GetAccountProfile();
                return View(model);
            }
        }

        /// <summary>
        /// 我的帳號-修改
        /// </summary>
        /// <returns></returns>
        [LoginedAuthorize()]
        [HttpPost]
        public ActionResult ProfileEdit(vmAccountProfile model)
        {
            using (AccountPage account = new AccountPage())
            {
                account.UpdateAccountProfile(model);
                return RedirectToAction("AccountProfile");
            }
        }

        /// <summary>
        /// 上傳圖像
        /// </summary>
        /// <returns></returns>
        [LoginedAuthorize()]
        public ActionResult ProfileUpload()
        {
            ViewBag.PanelWidth = SessionService.SetPrgInfo("", "我的帳號", "上傳圖像" , 6);
            return View();
        }

        /// <summary>
        /// 上傳圖像
        /// </summary>
        /// <param name="file">指定上傳的檔案</param>
        /// <returns></returns>
        [LoginedAuthorize()]
        [HttpPost]
        public ActionResult ProfileUploaded(HttpPostedFileBase file)
        {
            if (file != null)
            {
                if (file.ContentLength > 0)
                {
                    var fileName = SessionService.AccountNo + ".jpg";
                    var path = Path.Combine(Server.MapPath("~/Images/Account"), fileName);
                    if (System.IO.File.Exists(path)) System.IO.File.Delete(path);
                    file.SaveAs(path);
                }
            }
            return RedirectToAction("AccountProfile");
        }


        /// <summary>
        /// 變更密碼
        /// </summary>
        /// <returns></returns>
        [LoginedAuthorize()]
        public ActionResult ResetPassword()
        {
            ViewBag.PanelWidth = SessionService.SetPrgInfo("" , "我的帳號" , "變更密碼" , 6 );
            vmResetPassword model = new vmResetPassword();
            return View(model);
        }

        /// <summary>
        /// 變更密碼
        /// </summary>
        /// <returns></returns>
        [LoginedAuthorize()]
        [HttpPost]
        public ActionResult ResetPassword(vmResetPassword model)
        {
            if (!ModelState.IsValid) return View(model);
            using (AccountPage account = new AccountPage())
            {
                if (!account.ValidAccountPassword(model.OldPassword))
                {
                    ModelState.AddModelError("OldPassword", "現在密碼輸入錯誤!!");
                    return View(model);
                }
                account.ResetAccountPassword(model);
                return RedirectToAction("Index", "Admin");
            }
        }
    }
}