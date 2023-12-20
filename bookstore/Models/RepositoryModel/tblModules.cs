using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using bookstore.Models;

public class tblModules : BaseClass
{
    public IRepository<Modules> repo;
    public tblModules()
    {
        repo = new EFGenericRepository<Modules>(new bookstoreEntities());
    }
    /// <summary>
    /// 取得模組名稱
    /// </summary>
    /// <param name="moduleNo">模組代號</param>
    /// <returns></returns>
    public string GetModuleName(string moduleNo)
    {
        string str_value = "";
        var data = repo.ReadSingle(m => m.module_no == moduleNo);
        if (data != null) str_value = data.module_name;
        return str_value;
    }
}
