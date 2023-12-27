using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using bookstore.Models;

public class tblOrders : BaseClass
{
    public IRepository<Orders> repo;
    public tblOrders()
    {
        repo = new EFGenericRepository<Orders>(new bookstoreEntities());
    }
}