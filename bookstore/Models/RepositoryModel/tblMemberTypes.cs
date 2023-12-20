using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using bookstore.Models;

public class tblMemberTypes : BaseClass
{
    public IRepository<MemberTypes> repo;
    public tblMemberTypes()
    {
        repo = new EFGenericRepository<MemberTypes>(new bookstoreEntities());
    }
}