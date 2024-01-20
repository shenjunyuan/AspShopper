using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using bookstore.Models;
using PagedList;

public class tblPublishers : BaseClass
{
    public IRepository<Publishers> repo;
    public tblPublishers()
    {
        repo = new EFGenericRepository<Publishers>(new bookstoreEntities());
    }
    public string GetPublisherName(string publisherNo)
    {
        return repo.ReadSingle(m => m.publisher_no == publisherNo).publisher_name;
    }

    /// <summary>
    /// Publishers下拉選單
    /// </summary>
    /// <returns></returns>
    public List<Publishers> PublishersList()
    {
        return repo.ReadAll().OrderBy(m => m.rowid).ToList();
    }





}