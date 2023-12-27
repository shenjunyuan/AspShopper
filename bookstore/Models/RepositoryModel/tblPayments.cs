using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using bookstore.Models;

public class tblPayments : BaseClass
{
    public IRepository<Payments> repo;
    public tblPayments()
    {
        repo = new EFGenericRepository<Payments>(new bookstoreEntities());
    }
    /// <summary>
    /// 付款方式下拉選單
    /// </summary>
    /// <returns></returns>
    public List<Payments> PaymentsList()
    {
        return repo.ReadAll().OrderBy(m => m.mno).ToList();
    }
}