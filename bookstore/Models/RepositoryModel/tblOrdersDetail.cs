using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using bookstore.Models;

public class tblOrdersDetail : BaseClass
{
    public IRepository<OrdersDetail> repo;
    public tblOrdersDetail()
    {
        repo = new EFGenericRepository<OrdersDetail>(new bookstoreEntities());
    }
}