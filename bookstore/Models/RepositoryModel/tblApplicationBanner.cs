﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using bookstore.Models;

public class tblApplicationBanner : BaseClass
{
    public IRepository<ApplicationBanner> repo;
    public tblApplicationBanner()
    {
        repo = new EFGenericRepository<ApplicationBanner>(new bookstoreEntities());
    }
    /* 下面開始撰寫程式-專門處理 tb 的 CRUD */

    /// <summary>
    /// 取得 shopName 的 Banner 資料列表-倒排
    /// </summary>
    /// <param name="shopName"></param>
    /// <returns></returns>
    public List<string> GetBannerDataList(string shopName) 
    {
        using (tblApplicationBanner banners = new tblApplicationBanner()) 
        { 
            var data = repo
                .ReadAll(m=>m.is_show == true && 
                    m.shop_name == shopName)
                .OrderByDescending(m => m.rowid)
                .Select(m=>m.banner_name)
                .ToList();                
            return data;
        }  
    }

}