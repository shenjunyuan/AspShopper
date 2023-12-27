using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using bookstore.Models;

public class tblBigSales : BaseClass
{
    public IRepository<BigSales> repo;
    public tblBigSales()
    {
        repo = new EFGenericRepository<BigSales>(new bookstoreEntities());
    }

    /* 下面開始撰寫程式-專門處理 tb 的 CRUD */
    /// <summary>
    ///  抓取 BigSales 資料集 先全部抓出來再用 Where 抓日期區間
    /// </summary>
    /// <returns></returns>
    public List<BigSales> GetBigSalesDataList()
    {
        return repo.ReadAll()
            .Where(m => m.start_time <= DateTime.Now)
            .Where(m => m.end_time >= DateTime.Now)
            .OrderBy(m => m.product_no).ToList() ;
    }



}