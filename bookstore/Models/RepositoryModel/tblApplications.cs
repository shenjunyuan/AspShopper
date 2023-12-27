using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using bookstore.Models;

public class tblApplications : BaseClass
{
    public IRepository<Applications> repo;
    public tblApplications()
    {
        repo = new EFGenericRepository<Applications>(new bookstoreEntities());
    }

    /* 下面開始撰寫程式-專門處理 tb 的 CRUD */
    /// <summary>
    ///  抓取 Applications 資料 : 只抓第一筆
    /// </summary>
    /// <returns></returns>
    public Applications GetApplicationsData()
    {
        return repo.ReadAll().FirstOrDefault();
    }




}