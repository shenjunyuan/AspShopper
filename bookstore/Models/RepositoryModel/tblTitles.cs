using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using bookstore.Models;

public class tblTitles : BaseClass
{
    public IRepository<Titles> repo;
    public tblTitles()
    {
        repo = new EFGenericRepository<Titles>(new bookstoreEntities());
    }
    /// <summary>
    /// 用職稱代號取得職稱名稱
    /// </summary>
    /// <param name="titleNo">職稱代號</param>
    /// <returns></returns>
    public string GetTitleName(string titleNo)
    {
        string str_value = "";
        var data = repo.ReadSingle(m => m.title_no == titleNo);
        if (data != null) str_value = data.title_name;
        return str_value;
    }
}