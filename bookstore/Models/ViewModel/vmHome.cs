using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using bookstore.Models;

public class vmHome : BaseClass
{
    /// <summary>
    /// 應用程式 - 購物車首頁相關資料
    /// </summary>
    public Applications ApplicationsData { get; set; }
    /// <summary>
    /// 促銷商品資料 - 多筆使用 List
    /// </summary>
    public List<BigSales> BigSalesData { get; set; }
    /// <summary>
    /// 商品分類資料
    /// </summary>
    public List<Categorys> CategorysData { get; set; }
    /// <summary>
    /// 特色商品資料
    /// </summary>
    public List<Featureds> FeaturedsData { get; set; }
    /// <summary>
    /// 最新商品資料
    /// </summary>
    public List<Books> NewItemData { get; set; }

    /// <summary>
    ///  建構子 抓每個 Table 裡面的資料
    /// </summary>
    public vmHome()
    {
        using (tblApplications applications = new tblApplications())
        { ApplicationsData = applications.GetApplicationsData(); }
        using (tblBigSales bigSales = new tblBigSales())
        { BigSalesData = bigSales.GetBigSalesDataList(); }

        using (tblCategorys categorys = new tblCategorys())
        { CategorysData = categorys.GetCategoryDataList(); }

        using (tblFeatureds featureds = new tblFeatureds())
        { FeaturedsData = featureds.GetFeaturedsDataList(); }

        using (tblBooks books = new tblBooks())
        {
           NewItemData = books.GetNewBooksList();
        }
    }


}