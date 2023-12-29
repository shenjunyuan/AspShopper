using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using bookstore.Models;

public class tblCarts : BaseClass
{
    public IRepository<Carts> repo;
    public tblCarts()
    {
        repo = new EFGenericRepository<Carts>(new bookstoreEntities());
    }

    public List<Carts> CartList()
    {
        return repo.ReadAll(m => m.user_no == SessionService.AccountNo).ToList();
    }

    public void AddCart(string productNo, string prod_Spec, int buyQty)
    {
        using (tblBooks books = new tblBooks())
        {
            int int_price = books.GetSalePrice(productNo);
            int int_amount = (buyQty * int_price);
            var datas = repo.ReadSingle(m =>
              m.lot_no == CarPage.LotNo &&
                m.user_no == SessionService.AccountNo &&
               m.product_no == productNo);
            //&&   m.product_spec == prod_Spec);
            // 判斷購物車 user 有無新增資料
            if (datas == null)
            {
                Carts models = new Carts();
                models.lot_no = CarPage.LotNo;
                models.user_no = SessionService.AccountNo;
                models.create_time = CarPage.LotCreateTime;
                models.product_no = productNo;
                models.product_name = books.GetBookName(productNo);
                models.product_spec = prod_Spec;
                models.qty = buyQty;
                models.price = int_price;

                repo.Create(models);
                repo.SaveChanges();
            }
            else // 有資料 update 數量
            {
                datas.qty += buyQty;

                repo.Update(datas);
                repo.SaveChanges();
            }
        }
    }

    public void UpdateCart(int rowID, int qty)
    {
        var data = repo.ReadSingle(m => m.rowid == rowID);
        data.qty = qty;
        repo.Update(data);
        repo.SaveChanges();
    }

    public void DeleteCart(int rowID)
    {
        var data = repo.ReadSingle(m => m.rowid == rowID);
        if (data != null)
        {
            repo.Delete(data);
            repo.SaveChanges();
        }
    }

    /// <summary>
    /// 清除購物車
    /// </summary>
    public void ClearCart()
    {
        var datas = repo.ReadAll(m => m.user_no == SessionService.AccountNo);
        if (datas != null)
        {
            foreach (var item in datas)
            {
                repo.Delete(item);
            }
            repo.SaveChanges();
        }
    }
}