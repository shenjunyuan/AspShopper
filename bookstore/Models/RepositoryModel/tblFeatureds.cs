using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using bookstore.Models;

public class tblFeatureds : BaseClass
{
    public IRepository<Featureds> repo;
    public tblFeatureds()
    {
        repo = new EFGenericRepository<Featureds>(new bookstoreEntities());
    }
    public List<Featureds> GetFeaturedsDataList()
    {
        using (tblBooks books = new tblBooks())
        {
            // 取最後三筆
            var data = repo.ReadAll(m => m.is_enabled == true).OrderByDescending(m => m.rowid).Take(3).ToList();
            //var data = repo.ReadAll(m => m.is_enabled == true).OrderBy(m => m.rowid).ToList();
            foreach (var item in data)
            {
                item.product_name = books.GetBookName(item.product_no);
                item.category_name = books.GetCategoryName(item.product_no);
                item.sale_price = books.GetSalePrice(item.product_no);
            }
            return data;
        }
    }
}