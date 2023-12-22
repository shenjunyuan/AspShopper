using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using bookstore.Models;
using PagedList;

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
    public IPagedList<Modules> GetModelList(int index, int page, int pageSize, string searchText) // IPagedList 有分頁的型別
    {
        var model = repo.ReadAll();
        var dataSort = SessionService.GetColumnSort(index);
        if (!string.IsNullOrEmpty(searchText))
        {
            // 要搜尋的欄位
            model = model.Where(m =>
            m.module_no.Contains(searchText) ||
            m.module_name.Contains(searchText) ||
            m.icon_name.Contains(searchText) ||
            m.remark.Contains(searchText));
        }
        if (model != null)
        {
            // 要排序的欄位
            if (string.IsNullOrEmpty(dataSort.SortColumn)) dataSort.SortColumn = "module_no";
            if (dataSort.SortColumn == "is_enabled" && dataSort.SortDirection == enumSortDirection.Asc) model = model.OrderBy(m => m.is_enabled);
            if (dataSort.SortColumn == "is_enabled" && dataSort.SortDirection == enumSortDirection.Desc) model = model.OrderByDescending(m => m.is_enabled);
            if (dataSort.SortColumn == "module_no" && dataSort.SortDirection == enumSortDirection.Asc) model = model.OrderBy(m => m.module_no);
            if (dataSort.SortColumn == "module_no" && dataSort.SortDirection == enumSortDirection.Desc) model = model.OrderByDescending(m => m.module_no);
            if (dataSort.SortColumn == "module_name" && dataSort.SortDirection == enumSortDirection.Asc) model = model.OrderBy(m => m.module_name);
            if (dataSort.SortColumn == "module_name" && dataSort.SortDirection == enumSortDirection.Desc) model = model.OrderByDescending(m => m.module_name);
            if (dataSort.SortColumn == "icon_name" && dataSort.SortDirection == enumSortDirection.Asc) model = model.OrderBy(m => m.icon_name);
            if (dataSort.SortColumn == "icon_name" && dataSort.SortDirection == enumSortDirection.Desc) model = model.OrderByDescending(m => m.icon_name);
        }

        var datas = model.ToPagedList(page, pageSize);
        SessionService.SetCurrentPage(index, page, searchText, model.ToList().Count, datas.PageCount); // model.ToList().Count : 總筆數
        return datas;
    }


}
