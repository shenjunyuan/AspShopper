using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using bookstore.Models;
using PagedList;

public class tblBooks : BaseClass
{
    public IRepository<Books> repo;
    public tblBooks()
    {
        repo = new EFGenericRepository<Books>(new bookstoreEntities());
    }

    public string GetBookName(string bookNo)
    {
        return repo.ReadSingle(m => m.book_no == bookNo).book_name;
    }
    public string GetPublisherName(string bookNo)
    {
        using (tblPublishers publishers = new tblPublishers())
        {
            string str_publisher_no = repo.ReadSingle(m => m.book_no == bookNo).publisher_no;
            return publishers.GetPublisherName(str_publisher_no);
        }
    }
    public string GetLanguageName(string bookNo)
    {
        using (tblLanguages languages = new tblLanguages())
        {
            string str_language_no = repo.ReadSingle(m => m.book_no == bookNo).language_no;
            return languages.GetLanguageName(str_language_no);
        }
    }
    public string GetCategoryName(string bookNo)
    {
        using (tblCategorys categorys = new tblCategorys())
        {
            string str_category_no = repo.ReadSingle(m => m.book_no == bookNo).category_no;
            return categorys.GetCategoryName(str_category_no);
        }
    }
    public int GetSalePrice(string bookNo)
    {
        return repo.ReadSingle(m => m.book_no == bookNo).sale_price;
    }


    /// <summary>
    /// 購物商場-取得商品列表
    /// </summary>
    /// <param name="page"></param>
    /// <param name="pageSize"></param>
    /// <returns></returns>
    public List<Books> GetShopBooksList(int page = 1, int pageSize = 10)
    {
        using (tblPublishers publishers = new tblPublishers())
        {
            int int_low = ShopService.PriceLow;
            int int_high = ShopService.PriceHigh;
            var bookModel = repo.ReadAll(m => m.sale_price >= int_low && m.sale_price <= int_high).ToList();
            if (string.IsNullOrEmpty(ShopService.SearchText))
            {
                if (!string.IsNullOrEmpty(ShopService.CategoryNo))
                {
                    bookModel = bookModel.Where(m => m.category_no == ShopService.CategoryNo).ToList();
                }
            }
            if (!string.IsNullOrEmpty(ShopService.SearchText))
            {
                bookModel = bookModel.Where(m =>
                m.book_no.Contains(ShopService.SearchText) ||
                m.book_name.Contains(ShopService.SearchText) ||
                m.author_name.Contains(ShopService.SearchText)
                ).ToList();
            }
            if (ShopService.SortNo == "NameAsc") bookModel = bookModel.OrderBy(m => m.book_name).ToList();
            if (ShopService.SortNo == "NameDesc") bookModel = bookModel.OrderByDescending(m => m.book_name).ToList();
            if (ShopService.SortNo == "PriceAsc") bookModel = bookModel.OrderBy(m => m.sale_price).ToList();
            if (ShopService.SortNo == "PriceDesc") bookModel = bookModel.OrderByDescending(m => m.sale_price).ToList();

            ShopService.Page = (page == -1) ? ShopService.PriorPage() : (page == -2) ? ShopService.NextPage() : page;
            ShopService.PageSize = pageSize;
            ShopService.PageRowCount = bookModel.Count();

            bookModel = bookModel.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            return bookModel;
        }
    }

    /// <summary>
    /// 取得最新10筆資料
    /// </summary>
    /// <returns></returns>
    public List<Books> GetNewBooksList()
    {
        var bookModel = repo.ReadAll().OrderByDescending(m => m.rowid).Take(10).ToList();
        return bookModel;
    }
    /// <summary>
    /// 取得書本數量
    /// </summary>
    /// <param name="book_no">書本編號</param>
    /// <returns></returns>
    public int GetBooksQtyNow(string book_no)
    {
        int qty_now = 0;
        var data = repo.ReadSingle(m => m.book_no == book_no);
        if (data != null) qty_now = (int)data.qty_now;
        return qty_now;
    }


    /// <summary>
    /// 取得模組資料集
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    /// <summary>
    /// 取得程式資料集
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public IPagedList<Books> GetModelList(int index, int page, int pageSize, string searchText) // IPagedList 有分頁的型別
    {
        var model = repo.ReadAll().OrderBy(m=>m.rowid); //撈全部資料
        var dataSort = SessionService.GetColumnSort(index);
        if (!string.IsNullOrEmpty(searchText))
        {
            //要搜尋的欄位
           model = model.Where(m =>
           m.book_no.Contains(searchText) ||
           m.book_name.Contains(searchText) ||
           m.publisher_no.Contains(searchText) ||
           m.remark.Contains(searchText)).OrderBy(m => m.rowid);
        }
        if (model != null)
        {
            // 要排序的欄位
            if (string.IsNullOrEmpty(dataSort.SortColumn)) dataSort.SortColumn = "program_no";
            if (dataSort.SortColumn == "book_name" && dataSort.SortDirection == enumSortDirection.Asc) model = model.OrderBy(m => m.book_name);
            if (dataSort.SortColumn == "book_name" && dataSort.SortDirection == enumSortDirection.Desc) model = model.OrderByDescending(m => m.book_name);
            if (dataSort.SortColumn == "book_no" && dataSort.SortDirection == enumSortDirection.Asc) model = model.OrderBy(m => m.book_no);
            if (dataSort.SortColumn == "book_no" && dataSort.SortDirection == enumSortDirection.Desc) model = model.OrderByDescending(m => m.book_no);
            if (dataSort.SortColumn == "publisher_no" && dataSort.SortDirection == enumSortDirection.Asc) model = model.OrderBy(m => m.publisher_no);
            if (dataSort.SortColumn == "publisher_no" && dataSort.SortDirection == enumSortDirection.Desc) model = model.OrderByDescending(m => m.publisher_no);
            if (dataSort.SortColumn == "category_no" && dataSort.SortDirection == enumSortDirection.Asc) model = model.OrderBy(m => m.category_no);
            if (dataSort.SortColumn == "category_no" && dataSort.SortDirection == enumSortDirection.Desc) model = model.OrderByDescending(m => m.category_no);
            if (dataSort.SortColumn == "language_no" && dataSort.SortDirection == enumSortDirection.Asc) model = model.OrderBy(m => m.language_no);
            if (dataSort.SortColumn == "language_no" && dataSort.SortDirection == enumSortDirection.Desc) model = model.OrderByDescending(m => m.language_no);
        }
        var datas = model.ToPagedList(page, pageSize);
        SessionService.SetCurrentPage(index, page, searchText, model.ToList().Count, datas.PageCount); // model.ToList().Count : 總筆數
        return datas;
    }




}