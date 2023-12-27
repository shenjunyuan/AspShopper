using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using bookstore.Models;

public class tblProgramTypes : BaseClass
{
    public IRepository<ProgramTypes> repo;
    public tblProgramTypes()
    {
        repo = new EFGenericRepository<ProgramTypes>(new bookstoreEntities());
    }
    /// <summary>
    /// 取得類別圖示名稱
    /// </summary>
    /// <param name="typeNo">類別代號</param>
    /// <returns></returns>
    public string GetIconName (string typeNo)
    {
        string str_value = "";
        var data = repo.ReadSingle(m => m.type_no == typeNo);
        if (data != null) str_value = data.icon_name;
        return str_value;
    }

    /// <summary>
    /// 取得類別名稱
    /// </summary>
    /// <param name="typeNo">類別代號</param>
    /// <returns></returns>
    public string GetTypeName(string typeNo)
    {
        string str_value = "";
        var data = repo.ReadSingle(m => m.type_no == typeNo);
        if (data != null) str_value = data.type_name;
        return str_value;
    }
}