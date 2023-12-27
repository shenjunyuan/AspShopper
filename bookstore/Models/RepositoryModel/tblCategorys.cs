using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using bookstore.Models;

public class tblCategorys : BaseClass
{
    public IRepository<Categorys> repo;
    public tblCategorys()
    {
        repo = new EFGenericRepository<Categorys>(new bookstoreEntities());
    }
    public List<Categorys> GetCategoryDataList()
    {
        return repo.ReadAll(m => m.is_enabled == true && m.parentno == "")
            .OrderBy(m => m.mno).ToList();
    }
    public string GetCategoryName(string categoryNo)
    {
        var data = repo.ReadSingle(m => m.mno == categoryNo);
        return (data == null) ? "" : data.mname;
    }

    public List<Categorys> GetShopCategoryList()
    {
        using (tblBooks books = new tblBooks())
        {
            var model = repo.ReadAll(m => m.is_enabled == true).OrderBy(m => m.mno).ToList();
            foreach (var item in model)
            {
                item.book_counts = books.repo.ReadAll(m => m.category_no == item.mno).Count();
            }
            return model;
        }
    }
}