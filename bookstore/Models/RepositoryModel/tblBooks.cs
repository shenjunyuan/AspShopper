using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using bookstore.Models;

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

}