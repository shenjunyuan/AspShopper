﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using bookstore.Models;
using PagedList;

public class tblPrograms : BaseClass
{
    public IRepository<Programs> repo;
    public tblPrograms()
    {
        repo = new EFGenericRepository<Programs>(new bookstoreEntities());
    }
    /// <summary>
    /// 開啟程式
    /// </summary>
    /// <param name="prgNo">程式代號</param>
    public void PrgOpen(string prgNo)
    {
        var data = repo.ReadSingle(m => m.program_no == prgNo);
        if (data != null)
        {
            using (tblModules modules = new tblModules())
            {
                using (tblSecuritys securitys = new tblSecuritys())
                {
                    using (tblProgramTypes programTypes = new tblProgramTypes())
                    {
                        SessionService.ModuleNo = data.module_no;
                        SessionService.ModuleName = modules.GetModuleName(data.module_no);
                        SessionService.PrgNo = data.program_no;
                        SessionService.PrgName = data.program_name;
                        SessionService.PrgIcon = programTypes.GetIconName(data.program_type_no) + " mr-1";
                        SessionService.AreaName = data.area_name;
                        SessionService.ControllerName = data.controller_name;
                        SessionService.ActionName = data.action_name;
                        SessionService.ActionParm = data.parameter_value;
                        SessionService.PrgSecuritys = securitys.GetProgramSecurity(data.program_no);
                    }
                }
            }
        }
    }
    /// <summary>
    /// 取得圖示名稱
    /// </summary>
    /// <param name="prgNo">程式代號</param>
    /// <returns></returns>
    public string GetProgramIconName(string prgNo)
    {
        using (tblProgramTypes programTypes = new tblProgramTypes())
        {
            string str_value = "";
            var data = repo.ReadSingle(m => m.program_no == prgNo);
            if (data != null) str_value = programTypes.GetIconName(data.program_type_no);
            return str_value;
        }
    }


    /// <summary>
    /// 取得程式資料集
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public IPagedList<Programs> GetModelList(int index, int page, int pageSize, string searchText) // IPagedList 有分頁的型別
    {
        var model = repo.ReadAll();
        var dataSort = SessionService.GetColumnSort(index);
        if (!string.IsNullOrEmpty(searchText))
        {
            // 要搜尋的欄位
            model = model.Where(m =>
            m.module_no.Contains(searchText) ||        
            m.program_no.Contains(searchText) ||
            m.program_name.Contains(searchText) ||
            //m.module_name.Contains(searchText) ||
            //m.program_type_name.Contains(searchText) ||
            m.area_name.Contains(searchText) ||          
            m.controller_name.Contains(searchText) ||
            m.action_name.Contains(searchText) ||
            m.remark.Contains(searchText));
        }
        if (model != null)
        {
            // 要排序的欄位
            if (string.IsNullOrEmpty(dataSort.SortColumn)) dataSort.SortColumn = "program_no";
            if (dataSort.SortColumn == "is_enabled" && dataSort.SortDirection == enumSortDirection.Asc) model = model.OrderBy(m => m.is_enabled);
            if (dataSort.SortColumn == "is_enabled" && dataSort.SortDirection == enumSortDirection.Desc) model = model.OrderByDescending(m => m.is_enabled);
            if (dataSort.SortColumn == "module_no" && dataSort.SortDirection == enumSortDirection.Asc) model = model.OrderBy(m => m.module_no);
            if (dataSort.SortColumn == "module_no" && dataSort.SortDirection == enumSortDirection.Desc) model = model.OrderByDescending(m => m.module_no);
            if (dataSort.SortColumn == "program_no" && dataSort.SortDirection == enumSortDirection.Asc) model = model.OrderBy(m => m.program_no);
            if (dataSort.SortColumn == "program_no" && dataSort.SortDirection == enumSortDirection.Desc) model = model.OrderByDescending(m => m.program_no);
            if (dataSort.SortColumn == "program_name" && dataSort.SortDirection == enumSortDirection.Asc) model = model.OrderBy(m => m.program_name);
            if (dataSort.SortColumn == "program_name" && dataSort.SortDirection == enumSortDirection.Desc) model = model.OrderByDescending(m => m.program_name);
            if (dataSort.SortColumn == "area_name" && dataSort.SortDirection == enumSortDirection.Asc) model = model.OrderBy(m => m.area_name);
            if (dataSort.SortColumn == "area_name" && dataSort.SortDirection == enumSortDirection.Desc) model = model.OrderByDescending(m => m.area_name);
            if (dataSort.SortColumn == "controller_name" && dataSort.SortDirection == enumSortDirection.Asc) model = model.OrderBy(m => m.controller_name);
            if (dataSort.SortColumn == "controller_name" && dataSort.SortDirection == enumSortDirection.Desc) model = model.OrderByDescending(m => m.controller_name);
            if (dataSort.SortColumn == "action_name" && dataSort.SortDirection == enumSortDirection.Asc) model = model.OrderBy(m => m.action_name);
            if (dataSort.SortColumn == "action_name" && dataSort.SortDirection == enumSortDirection.Desc) model = model.OrderByDescending(m => m.action_name);
            if (dataSort.SortColumn == "remark" && dataSort.SortDirection == enumSortDirection.Asc) model = model.OrderBy(m => m.remark);
            if (dataSort.SortColumn == "remark" && dataSort.SortDirection == enumSortDirection.Desc) model = model.OrderByDescending(m => m.remark);

            //if (dataSort.SortColumn == "module_name" && dataSort.SortDirection == enumSortDirection.Asc) model = model.OrderBy(m => m.module_name);
            //if (dataSort.SortColumn == "module_name" && dataSort.SortDirection == enumSortDirection.Desc) model = model.OrderByDescending(m => m.module_name);
            if (dataSort.SortColumn == "program_type_name" && dataSort.SortDirection == enumSortDirection.Asc) model = model.OrderBy(m => m.program_type_name);
            if (dataSort.SortColumn == "program_type_name" && dataSort.SortDirection == enumSortDirection.Desc) model = model.OrderByDescending(m => m.program_type_name);

        }

        var datas = model.ToPagedList(page, pageSize);
        SessionService.SetCurrentPage(index, page, searchText, model.ToList().Count, datas.PageCount); // model.ToList().Count : 總筆數
        return datas;
    }


}
