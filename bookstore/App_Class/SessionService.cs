using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Web;
using System.Web.Configuration;

public static class SessionService
{
    /// <summary>
    /// 登入帳號
    /// </summary>
    public static string AccountNo { get { return GetSessionValue("AccountNo", "guest"); } set { HttpContext.Current.Session["AccountNo"] = value; } }
    /// <summary>
    /// 登入名稱
    /// </summary>
    public static string AccountName { get { return GetSessionValue("AccountName", "遊客"); } set { HttpContext.Current.Session["AccountName"] = value; } }
    /// <summary>
    /// 登入角色代號
    /// </summary>
    public static string RoleNo { get { return GetSessionValue("RoleNo", ""); } set { HttpContext.Current.Session["RoleNo"] = value; } }
    /// <summary>
    /// 登入角色名稱
    /// </summary>
    public static string RoleName { get { return GetSessionValue("RoleName", "未登入"); } set { HttpContext.Current.Session["RoleName"] = value; } }
    /// <summary>
    /// 是否已登入
    /// </summary>
    public static bool IsLogined { get { return GetSessionBoolValue("IsLogined", false); } set { HttpContext.Current.Session["IsLogined"] = value; } }
    /// <summary>
    /// 模組代號
    /// </summary>
    public static string ModuleNo { get { return GetSessionValue("ModuleNo", ""); } set { HttpContext.Current.Session["ModuleNo"] = value; } }
    /// <summary>
    /// 模組名稱
    /// </summary>
    public static string ModuleName { get { return GetSessionValue("ModuleName", ""); } set { HttpContext.Current.Session["ModuleName"] = value; } }
    /// <summary>
    /// 程式代號
    /// </summary>
    public static string PrgNo { get { return GetSessionValue("PrgNo", ""); } set { HttpContext.Current.Session["PrgNo"] = value; } }
    /// <summary>
    /// 程式名稱
    /// </summary>
    public static string PrgName { get { return GetSessionValue("PrgName", ""); } set { HttpContext.Current.Session["PrgName"] = value; } }
    /// <summary>
    /// 程式功能
    /// </summary>
    public static string PrgAction { get { return GetSessionValue("PrgAction", ""); } set { HttpContext.Current.Session["PrgAction"] = value; } }
    /// <summary>
    /// 程式圖示
    /// </summary>
    public static string PrgIcon { get { return GetSessionValue("PrgIcon", "nav-icon fas fa-circle"); } set { HttpContext.Current.Session["PrgIcon"] = value; } }
    /// <summary>
    /// 程式資訊
    /// </summary>
    public static string PrgInfo { get { return (string.Format("{0} {1}", PrgNo, PrgName)); } }
        /// <summary>
    /// 事件資訊
    /// </summary>
    public static string ActionInfo { 
        get 
        {
            string str_value = "";
            if (!string.IsNullOrEmpty(PrgNo)) str_value += PrgNo;
            if (!string.IsNullOrEmpty(PrgName))
            {
                if (!string.IsNullOrEmpty(str_value)) str_value += " ";
                str_value += PrgName;
            }
            if (!string.IsNullOrEmpty(ActionName))
            {
                if (!string.IsNullOrEmpty(str_value)) str_value += " - ";
                str_value += ActionName;
            }
            return str_value; 
        } 
    }
    /// <summary>
    /// 區域名稱
    /// </summary>
    public static string AreaName { get { return GetSessionValue("AreaName", ""); } set { HttpContext.Current.Session["AreaName"] = value; } }
    /// <summary>
    /// 控制器名稱
    /// </summary>
    public static string ControllerName { get { return GetSessionValue("ControllerName", ""); } set { HttpContext.Current.Session["ControllerName"] = value; } }
    /// <summary>
    /// 動作名稱
    /// </summary>
    public static string ActionName { get { return GetSessionValue("ActionName", ""); } set { HttpContext.Current.Session["ActionName"] = value; } }
    /// <summary>
    /// 動作提示
    /// </summary>
    public static string ActionTips
    {
        get
        {
            string str_value = GetSessionValue("ActionTips", "");
            return (string.IsNullOrEmpty(str_value)) ? "" : string.Format(" - {0}", str_value);
        }
        set { HttpContext.Current.Session["ActionTips"] = value; }
    }
    /// <summary>
    /// 參數名稱
    /// </summary>
    public static string ActionParm { get { return GetSessionValue("ActionParm", ""); } set { HttpContext.Current.Session["ActionParm"] = value; } }
    /// <summary>
    /// 程式權限
    /// </summary>
    public static AccountSecurity PrgSecuritys
    {
        get
        {
            if (HttpContext.Current.Session["AccountSecurity"] != null) return (AccountSecurity)HttpContext.Current.Session["AccountSecurity"];
            return new AccountSecurity() { Add = false, Edit = false, Delete = false, Print = false, Upload = false, Download = false, Confirm = false, Undo = false };
        }
        set { HttpContext.Current.Session["AccountSecurity"] = value; }
    }
    /// <summary>
    /// 登入帳號圖像
    /// </summary>
    public static string AccountImage
    {
        get
        {
            string str_path = @"~\Images\Account";
            string str_now = DateTime.Now.ToString("yyyyMMddHHmmssffff");
            string str_file = string.Format(@"{0}\{1}.jpg", str_path, AccountNo);
            if (File.Exists(HttpContext.Current.Server.MapPath(str_file)))
            {
                str_file += string.Format(@"?t={0}", str_now);
                return str_file;
            }


            return string.Format(@"{0}\user.jpg", str_path);
        }
    }
    /// <summary>
    /// 網站名稱
    /// </summary>
    public static string AppName { get { return GetAppSettings("AppName", "網站名稱"); } }
    /// <summary>
    /// 網站版本
    /// </summary>
    public static string Version { get { return GetAppSettings("Version", "1.0.0"); } }
    /// <summary>
    /// 版權宣告
    /// </summary>
    public static string CopyRight { get { return GetAppSettings("CopyRight", DateTime.Now.Year.ToString()); } }
    /// <summary>
    /// 公司名稱
    /// </summary>
    public static string CompName { get { return GetAppSettings("CompName", "公司名稱"); } }
    /// <summary>
    /// 網站網址
    /// </summary>
    public static string WebSiteUrl { get { return GetAppSettings("WebSiteUrl", ""); } }
    /// <summary>
    /// 取得 Web.config 中的 appSettings 的值
    /// </summary>
    /// <param name="keyName">Key 名</param>
    /// <param name="defaultValue">預設值</param>
    /// <returns></returns>
    public static string GetAppSettings(string keyName, string defaultValue)
    {
        object obj_value = WebConfigurationManager.AppSettings[keyName];
        string str_value = (obj_value == null) ? defaultValue : obj_value.ToString();
        return str_value;
    }
    /// <summary>
    /// 取得 Session 值-文字型別
    /// </summary>
    /// <param name="sessionName">Session 名稱</param>
    /// <returns></returns>
    public static string GetSessionValue(string sessionName, string defauleValue)
    {
        return (HttpContext.Current.Session[sessionName] == null) ? defauleValue : HttpContext.Current.Session[sessionName].ToString();
    }
    /// <summary>
    /// 取得 Session 值-布林值型別
    /// </summary>
    /// <param name="sessionName">Session 名稱</param>
    /// <returns></returns>
    public static bool GetSessionBoolValue(string sessionName, bool defaultValue)
    {
        return (HttpContext.Current.Session[sessionName] == null) ? defaultValue : (bool)HttpContext.Current.Session[sessionName];
    }
    /// <summary>
    /// 登出改變屬性值
    /// </summary>
    public static void Logout()
    {
        AccountNo = "guest";
        AccountName = "遊客";
        RoleNo = "";
        RoleName = "未登入";
        IsLogined = false;
    }

    /// <summary>
    /// 設定程式資訊及 Panel 寬度(12)
    /// </summary>
    /// <param name="actionName">事件名稱</param>
    /// <returns></returns>
    public static string SetPrgInfo(string actionName)
    {
        return SetPrgInfo(actionName, 12);
    }

    /// <summary>
    /// 設定程式資訊及 Panel 寬度(1 - 12)
    /// </summary>
    /// <param name="actionName">事件名稱</param>
    /// <param name="panelWidth"> Panel 寬度 (1 - 12)</param>
    /// <returns></returns>
    public static string SetPrgInfo(string actionName, int panelWidth)
    {
        ActionName = actionName;
        if (panelWidth <= 0 || panelWidth > 12) panelWidth = 12;
        return string.Format("col-md-{0}", panelWidth);
    }

    /// <summary>
    /// 設定程式資訊及 Panel 寬度(12)
    /// </summary>
    /// <param name="prgNo">程式代號</param>
    /// <param name="prgName">程式名稱</param>
    /// <param name="actionName">事件名稱</param>
    /// <returns></returns>
    public static string SetPrgInfo(string prgNo, string prgName , string actionName)
    {
        return SetPrgInfo(prgNo, prgName, actionName , 1);
    }
    /// <summary>
    /// 設定程式資訊及 Panel 寬度
    /// </summary>
    /// <param name="prgNo">程式代號</param>
    /// <param name="prgName">程式名稱</param>
    /// <param name="actionName">事件名稱</param>
    /// <param name="panelWidth"> Panel 寬度 (1 - 12)</param>
    /// <returns></returns>
    public static string SetPrgInfo(string prgNo, string prgName ,string actionName , int panelWidth)
    {
        PrgNo = prgNo;
        PrgName = prgName;
        ActionName = actionName;
        if (panelWidth <= 0 || panelWidth > 12) panelWidth = 12;
        return string.Format("col-md-{0}", panelWidth);
    }
}
