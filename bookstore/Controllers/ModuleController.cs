using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using bookstore.Models;
using PagedList;

namespace bookstore.Controllers
{
    public class ModuleController : Controller
    {
        //[SecurityAuthorize(SecurityMode = enumSecuritys.Browse)]
        public ActionResult Index(int page = 1, int pageSize = 10, string searchText = "")
        {
            ViewBag.PanelWidth = SessionService.SetPrgInfo("資料列表");
            using (tblModules modules = new tblModules())
            {
                var model = modules.GetModelList(0, page, pageSize, searchText);
                return View(model);
            }
        }

        /// <summary>
        /// 排序
        /// </summary>
        /// <param name="id">欄位名稱</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ColumnSort(string id)
        {
            int int_index = 0;
            var sort = SessionService.GetColumnSort(int_index);
            if (sort.SortColumn == id)
            {
                if (sort.SortDirection == enumSortDirection.Asc)
                    SessionService.SetColumnSort(int_index, sort.Page, id, enumSortDirection.Desc);
                else
                    SessionService.SetColumnSort(int_index, sort.Page, id, enumSortDirection.Asc);
            }
            else
            {
                SessionService.SetColumnSort(int_index, sort.Page, id, enumSortDirection.Asc);
            }
            var sortData = SessionService.GetColumnSort(int_index);
            return RedirectToAction("Index", new { page = sortData.Page });
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Add()
        {
            Modules model = new Modules();
            ViewBag.PanelWidth = SessionService.SetPrgInfo("新增", 6);
            return View(model);
        }

        /// <summary>
        /// 新增存檔
        /// </summary>
        /// <param name="model">Module 資料物件</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Add(Modules model)
        {
            if (!ModelState.IsValid) return View(model);
            using (tblModules modules = new tblModules())
            {
                modules.repo.Create(model);
                modules.repo.SaveChanges();
                return RedirectToAction("Index");
            }
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Edit(int id)
        {
            using (tblModules modules = new tblModules())
            {
                var model = modules.repo.ReadSingle(m => m.rowid == id);
                if (model == null) return RedirectToAction("Index");
                ViewBag.PanelWidth = SessionService.SetPrgInfo("修改", 6);
                return View(model);
            }
        }

        /// <summary>
        /// 修改存檔
        /// </summary>
        /// <param name="model">Module 資料物件</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Edit(Modules model)
        {
            if (!ModelState.IsValid) return View(model);
            using (tblModules modules = new tblModules())
            {
                modules.repo.Update(model);
                modules.repo.SaveChanges();
                return RedirectToAction("Index");
            }
        }
        /// <summary>
        /// 刪除
        /// </summary>
        /// <param name="id">記錄的 ID</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Delete(int id)
        {
            using (tblModules modules = new tblModules())
            {
                var model = modules.repo.ReadSingle(m => m.rowid == id);
                if (model != null)
                {
                    modules.repo.Delete(model);
                    modules.repo.SaveChanges();
                }
                return RedirectToAction("Index");
            }
        }

        /// <summary>
        /// 查詢
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Search()
        {
            string str_text = Request.Form["search_text"].ToString(); // 從 html Form ID 傳回來的文字
            return RedirectToAction("Index", "Module", new { searchText = str_text });
        }


        [HttpPost]
        public ActionResult Index(FormCollection model) // 存檔的時候 FormCollection 收集資訊 ，一次更新全部的資料
        {
            if (model != null)
            {
                using (tblModules modules = new tblModules())
                {
                    int int_rowid = 0;
                    string str_module_no = "";
                    bool bln_enabled = false;
                    List<string> rowidList = model["item.rowid"].Split(',').ToList();
                    List<string> noList = model["item.module_no"].Split(',').ToList();
                    List<bool> enabledList = Utility.GetCheckBoxListValue(model["item.is_enabled"]);
                    for (int i = 0; i < rowidList.Count; i++)
                    {
                        int_rowid = int.Parse(rowidList[i]);
                        str_module_no = noList[i];
                        bln_enabled = enabledList[i];
                        var data = modules.repo.ReadSingle(m => m.rowid == int_rowid);
                        if (data != null)
                        {
                            data.is_enabled = bln_enabled;
                            //  mdModule.cs 限制 module_name 不可空白，所以要把模組名稱 null 跟 "" 給值
                            if (data.module_name == "" || data.module_name == null) { data.module_name = "無模組名稱"; }
                            else { data.module_name = data.module_name; }                                                                                        
                            modules.repo.Update(data);
                            modules.repo.SaveChanges();
                        }
                    }
                }
                
            }
            return RedirectToAction("Index");
        }


    }
}