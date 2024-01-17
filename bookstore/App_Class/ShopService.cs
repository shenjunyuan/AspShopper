using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

/// <summary>
/// 購物商城頁專用類別
/// </summary>
public static class ShopService
{
    /// <summary>
    /// 分類方式
    /// </summary>
    public static string CategoryNo { get { return Utility.GetSessionValue("ShopCategoryNo", ""); } set { HttpContext.Current.Session["ShopCategoryNo"] = value; } }
    /// <summary>
    /// 最低售價
    /// </summary>
    public static int PriceLow { get { return Utility.GetSessionIntegerValue("ShopPriceLow", 800); } set { HttpContext.Current.Session["ShopPriceLow"] = value; } }
   
    /// <summary>
    /// 最高售價
    /// </summary>
    public static int PriceHigh { get { return Utility.GetSessionIntegerValue("ShopPriceHigh", 2000); } set { HttpContext.Current.Session["ShopPriceHigh"] = value; } }
    public static int OrderID { get; set; }
    public static string OrderNo { get; set; }
    /// <summary>
    /// 排序方式
    /// </summary>
    public static string SortNo { get { return Utility.GetSessionValue("ShopSortNo", "PriceAsc"); } set { HttpContext.Current.Session["ShopSortNo"] = value; } }
    /// <summary>
    /// 搜尋文字
    /// </summary>
    public static string SearchText { get { return Utility.GetSessionValue("ShopSearchText", ""); } set { HttpContext.Current.Session["ShopSearchText"] = value; } }
    /// <summary>
    /// 目前頁數
    /// </summary>
    public static int Page { get { return Utility.GetSessionIntegerValue("ShopPage", 1); } set { HttpContext.Current.Session["ShopPage"] = value; } }
    /// <summary>
    /// 總頁數
    /// </summary>
    public static int Pages
    {
        get
        {
            int int_page_count = PageRowCount / PageSize;
            if (PageRowCount % PageSize > 0) int_page_count++;
            return int_page_count;
        }
    }
    /// <summary>
    /// 每頁筆數
    /// </summary>
    public static int PageSize { get { return Utility.GetSessionIntegerValue("ShopPageSize", 10); } set { HttpContext.Current.Session["ShopPageSize"] = value; } }
    /// <summary>
    /// 每頁顯示頁數
    /// </summary>
    public static int PageCount { get { return Utility.GetSessionIntegerValue("ShopPageCount", 10); } set { HttpContext.Current.Session["ShopPageCount"] = value; } }
    /// <summary>
    /// 總筆數
    /// </summary>
    public static int PageRowCount { get { return Utility.GetSessionIntegerValue("ShopRowCount", 0); } set { HttpContext.Current.Session["ShopRowCount"] = value; } }
    /// <summary>
    /// 開始頁數
    /// </summary>
    public static int PageStart
    {
        get
        {
            int int_start = 1;
            if (Page > PageCount)
            {
                int int_count = Page / PageCount;
                if (Page % PageCount == 0) int_count--;
                int_start = (int_count * PageCount) + 1;
            }
            return int_start;
        }
    }
    /// <summary>
    /// 結束頁數
    /// </summary>
    public static int PageEnd
    {
        get
        {
            int int_page = PageStart;
            int int_row_count = PageRowCount;
            if (PageStart > PageCount)
            {
                int_row_count -= (PageSize * (PageStart - 1));
            }
            if (int_row_count > 0)
            {
                int int_count = PageSize / int_row_count;
                if (PageSize % int_row_count > 0) int_count++;
                if (int_count > PageCount) int_count = PageCount;
                int_page += (PageCount - 1);
                if (int_page > Pages) int_page = Pages;
            }
            return int_page;
        }
    }
    public static int PriorPage()
    {
        return (PageStart - 1);
    }
    public static int NextPage()
    {
        return (PageEnd + 1);
    }
    public static int GetAllBookCount()
    {
        using (tblBooks books = new tblBooks())
        {
            return books.repo.ReadAll().Count();
        }
    }
    /// <summary>
    /// 分類名稱
    /// </summary>
    public static string CategoryName
    {
        get
        {
            if (!string.IsNullOrEmpty(SearchText)) return string.Format("搜尋：{0}", SearchText);
            if (string.IsNullOrEmpty(CategoryNo)) return "全部商品";
            using (tblCategorys categorys = new tblCategorys())
            {
                return categorys.GetCategoryName(CategoryNo);
            }
        }
    }
    /// <summary>
    /// 排序名稱
    /// </summary>
    public static string SortName
    {
        get
        {
            if (SortNo == "Hot") return "熱門商品";
            if (SortNo == "PriceAsc") return "依價格,由小到大";
            if (SortNo == "PriceDesc") return "依價格,由大到小";
            if (SortNo == "NameAsc") return "依名稱,由小到大";
            if (SortNo == "NameDesc") return "依名稱,由大到小";
            return "";
        }
    }

    /// <summary>
    /// 取得圖片路徑
    /// </summary>
    /// <param name="productNo">商品編號</param>
    /// <returns></returns>
    public static string GetProductImageUrl(string productNo)
    {
        string product_no_trim = productNo.Trim(); // 資料庫的 product_no 有空白會抓不到圖片
        string str_url = string.Format("~/Images/Product/{0}.jpg", product_no_trim);
        Console.WriteLine(str_url);
        if (!File.Exists(HttpContext.Current.Server.MapPath(str_url))) // 相對路徑改成絕對路徑判斷
            str_url = "~/Images/Product/None.jpg";
        return str_url;
    }
    public static string GetLogoImgUrl() 
    {
        string ImgUrl = "~/Images/Application/ps_logo.jpg";
        return ImgUrl;
  
    }
    /// <summary>
    /// 將訂單改為已付款
    /// </summary>
    public static void SetOrderPayed()
    {
        using (tblOrders orders = new tblOrders())
        {
            var data = orders.repo.ReadSingle(m => m.rowid == ShopService.OrderID);
            if (data != null)
            {
                data.order_validate = 1;
                orders.repo.Update(data);
                orders.repo.SaveChanges();          
            }      
        }
    }


}