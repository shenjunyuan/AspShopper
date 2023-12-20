﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

/// <summary>
/// 自定義權限 Filter
/// </summary>
public class LoginAuthorize : AuthorizeAttribute
{
    /// <summary>
    /// 角色代號
    /// </summary>
    public string RoleNo { get; set; } = "";
    /// <summary>
    /// 覆寫 Authorize 設定
    /// </summary>
    /// <param name="httpContext">httpContext</param>
    /// <returns>驗證結果</returns>
    protected override bool AuthorizeCore(HttpContextBase httpContext)
    {
        //未限制角色不檢查權限
        if (string.IsNullOrEmpty(RoleNo)) return true;

        //檢查登入者角色是否包含在限制的角色中
        bool bln_authorized = false;
        List<string> lists = RoleNo.Split(',').ToList();
        foreach (string role in lists)
        {
            if (SessionService.RoleNo == role) { bln_authorized = true; break; }
        }
        return bln_authorized;
    }

    // 驗證失敗要做什麼
    protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
    {
        // 設定要執行的Result
        filterContext.Result = new RedirectResult("/Account/Login");
    }
}
