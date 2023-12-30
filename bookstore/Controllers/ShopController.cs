using bookstore.Models;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace bookstore.Controllers
{
    public class ShopController : Controller
    {
        public ActionResult Index(int page = 1, int pageSize = 12)
        {
            using (tblBooks books = new tblBooks())
            {
                using (tblCategorys categorys = new tblCategorys())
                {
                    ShopService.PageCount = 10;
                    vmShopIndex model = new vmShopIndex();
                    model.BookList = books.GetShopBooksList(page, pageSize);
                    model.CategoryList = categorys.GetShopCategoryList();
                    return View(model);
                }
            }
        }
        /// <summary>
        /// 商品排序
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Sort(string id)
        {
            ShopService.SortNo = id;
            return RedirectToAction("Index", "Shop");
        }
        /// <summary>
        /// 商品分類
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Category(string id)
        {
            ShopService.CategoryNo = id;
            ShopService.SearchText = "";
            return RedirectToAction("Index", "Shop");
        }
        /// <summary>
        /// 商品搜尋
        /// </summary>
        /// <param name="formCollection"></param>
        /// <returns></returns>
        public ActionResult Search(FormCollection formCollection)
        {
            ShopService.CategoryNo = "";
            object obj_text = formCollection["SearchText"];
            string str_text = (obj_text == null) ? "" : obj_text.ToString();
            ShopService.SearchText = str_text;
            return RedirectToAction("Index", "Shop");
        }
        /// <summary>
        /// 價格區間
        /// </summary>
        /// <param name="formCollection"></param>
        /// <returns></returns>
        public ActionResult Price(FormCollection formCollection)
        {
            int int_low = 0;
            int int_high = 0;
            object obj_low = formCollection["price_low"];
            object obj_high = formCollection["price_high"];
            string str_low = (obj_low == null) ? "0" : obj_low.ToString();
            string str_high = (obj_high == null) ? "5000" : obj_high.ToString();
            int.TryParse(str_low, out int_low);
            int.TryParse(str_high, out int_high);
            ShopService.PriceLow = int_low;
            ShopService.PriceHigh = int_high;
            return RedirectToAction("Index", "Shop");
        }
        /// <summary>
        /// 商品明細
        /// </summary>
        /// <param name="id">商品編號</param>
        /// <returns></returns>
        public ActionResult Detail(string id)
        {
            using (Books book = new Books())
            {
                if (string.IsNullOrEmpty(id)) // 若有空值 預設第一筆 book_no
                {
                   id = book.GetTop1BookNo();      
                }
                var model = book.GetBookDetail(id); 
                return View(model);   
            }
        }
        /// <summary>
        /// 加入購物車
        /// </summary>
        /// <param name="model">Books Model</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddToCart(FormCollection collection )
        {
            string str_book_no = collection["book_no"];
            string str_qty = collection["Quantity"];
            int int_qty = 1;
            int.TryParse(str_qty, out int_qty);
            CarPage.AddCart(str_book_no, int_qty);
            return RedirectToAction("Detail", new { id = str_book_no}); // 停留在原本 Detail 畫面
        }
        /// <summary>
        /// 更新購物車
        /// </summary>
        /// <param name="collection"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UpdateCart(FormCollection collection)
        {
            int int_rowid = 0;
            int int_qty = 0;
            string message = "";
            for (int i = 1; i < collection.Count; i += 2)
            {
                int_rowid = int.Parse(collection[i].ToString());
                int_qty = int.Parse(collection[i + 1].ToString());
                using (Carts cart = new Carts())
                {
                    using (Books book = new Books())
                    {
                        string product_no = cart.GetProductNo(int_rowid.ToString());
                        Books bookInfo = book.GetBookDetail(product_no);
                        string book_name = bookInfo.book_name;
                        int qty_now = (int)bookInfo.qty_now;
                        if (int_qty > qty_now)
                        {
                            message = $"商品名稱:{book_name}，庫存不足!!! 更新數量失敗";
                        }
                        else
                        {
                            CarPage.UpdateCart(int_rowid, int_qty);
                        }
                    }
                }            
            }
            return RedirectToAction("Cart", "Shop",new{message});
        }
        /// <summary>
        /// 刪除購物車
        /// </summary>
        /// <param name="id">row ID</param>
        /// <returns></returns>
        public ActionResult DeleteCart(int id)
        {
            CarPage.DeleteCart(id);
            return RedirectToAction("Cart", "Shop");
        }

        /// <summary>
        /// 購物車
        /// </summary>
        /// <returns></returns>
        public ActionResult Cart()
        {
            using (tblCarts carts = new tblCarts())
            {
                if (SessionService.IsLogined)
                {
                    var data1 = carts.repo.ReadAll(m => m.user_no == SessionService.AccountNo);
                    return View(data1);
                }
                var data2 = carts.repo.ReadAll(m => m.lot_no == CarPage.LotNo);
                return View(data2);
            }
        }

        /// <summary>
        /// 結帳付款
        /// </summary>
        /// <returns></returns>
        public ActionResult Payment()
        {
            if (!SessionService.IsLogined) return RedirectToAction("Login", "Account");
            if (CarPage.Counts <= 0) return RedirectToAction("Index", "Shop");

            using (tblPayments payments = new tblPayments())
            {
                using (tblShippings shippings = new tblShippings())
                {
                    using (tblCarts carts = new tblCarts())
                    {
                        vmOrders models = new vmOrders()
                        {
                            receive_name = "",
                            receive_email = "",
                            receive_address = "",
                            payment_no = "01",
                            shipping_no = "01",
                            remark = "",
                            PaymentsList = payments.PaymentsList(),
                            ShippingsList = shippings.ShippingsList(),
                            CartsList = carts.CartList()
                        };
                        return View(models);
                    }
                }
            }
        }
        /// <summary>
        /// 結帳付款完成訂單
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Payment(vmOrders model, FormCollection form)
        {
          
            if (!ModelState.IsValid)
            {
                using (tblPayments payments = new tblPayments())
                {
                    using (tblShippings shippings = new tblShippings())
                    {
                        using (tblCarts carts = new tblCarts())
                        {
                            if (model.PaymentsList == null) model.PaymentsList = payments.PaymentsList();
                            if (model.ShippingsList == null) model.ShippingsList = shippings.ShippingsList();
                            if (model.CartsList == null) model.CartsList = carts.CartList();
                            return View(model);
                        }
                    }
                }
            }
            CarPage.CartPayment(model);
            CarPage.ClearCart();
            CarPage.NewLotNo();

            string pay_no = form["payment_no"];
            if(pay_no == "01" || pay_no =="03") return Redirect("~/ECPayment.aspx");

            return RedirectToAction("CheckoutReport");
        }

        public ActionResult PaymentReport()
        {
            return View();
        }
        /// <summary>
        /// 金流付款返回畫面
        /// </summary>
        /// <returns></returns>
        public ActionResult CheckoutReport()
        {
            ShopService.SetOrderPayed(); // 更改訂單狀態已付款
            return View();
        }
    }
}