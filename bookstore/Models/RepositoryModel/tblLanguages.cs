using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using bookstore.Models;
using PagedList;


public class tblLanguages : BaseClass
{
    public IRepository<Languages> repo;
    public tblLanguages()
    {
        repo = new EFGenericRepository<Languages>(new bookstoreEntities());
    }
    public string GetLanguageName(string languageNo)
    {
        return repo.ReadSingle(m => m.language_no == languageNo).language_name;
    }


}