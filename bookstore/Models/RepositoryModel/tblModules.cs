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

    /// <summary>
    /// 取得模組資料集
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public List<Modules> GetModuleList(int index)
    {
        var model = repo.ReadAll().OrderBy(m => m.module_no);
        if (model != null)
        {
            var dataSort = SessionService.GetColumnSort(index);
            if (dataSort.SortColumn == "is_enabled" && dataSort.SortDirection == enumSortDirection.Asc) model = model.OrderBy(m => m.is_enabled);
            if (dataSort.SortColumn == "is_enabled" && dataSort.SortDirection == enumSortDirection.Desc) model = model.OrderByDescending(m => m.is_enabled);
            if (dataSort.SortColumn == "module_no" && dataSort.SortDirection == enumSortDirection.Asc) model = model.OrderBy(m => m.module_no);
            if (dataSort.SortColumn == "module_no" && dataSort.SortDirection == enumSortDirection.Desc) model = model.OrderByDescending(m => m.module_no);
            if (dataSort.SortColumn == "module_name" && dataSort.SortDirection == enumSortDirection.Asc) model = model.OrderBy(m => m.module_name);
            if (dataSort.SortColumn == "module_name" && dataSort.SortDirection == enumSortDirection.Desc) model = model.OrderByDescending(m => m.module_name);
            if (dataSort.SortColumn == "icon_name" && dataSort.SortDirection == enumSortDirection.Asc) model = model.OrderBy(m => m.icon_name);
            if (dataSort.SortColumn == "icon_name" && dataSort.SortDirection == enumSortDirection.Desc) model = model.OrderByDescending(m => m.icon_name);
        }
        return model.ToList();
    }



}
