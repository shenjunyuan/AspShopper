using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using bookstore.Models;

public class tblShippings : BaseClass
{
    public IRepository<Shippings> repo;
    public tblShippings()
    {
        repo = new EFGenericRepository<Shippings>(new bookstoreEntities());
    }

    public List<Shippings> ShippingsList()
    {
        return repo.ReadAll().OrderBy(m => m.mno).ToList();
    }
}