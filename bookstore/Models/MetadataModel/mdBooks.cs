using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace bookstore.Models
{
    [MetadataType(typeof(mdBooks))]
    public partial class Books : BaseClass
    {
        public string publisher_name { get { using (tblBooks books = new tblBooks()) { return books.GetPublisherName(book_no); } } }
        public string category_name { get { using (tblBooks books = new tblBooks()) { return books.GetCategoryName(book_no); } } }
        public string language_name { get { using (tblBooks books = new tblBooks()) { return books.GetLanguageName(book_no); } } }

        private class mdBooks
        {
            [Key]
            public int rowid { get; set; }
            [Display(Name = "書本編號")]
            public string book_no { get; set; }
            public string isbn_no { get; set; }
            public string book_name { get; set; }
            public string author_name { get; set; }
            public System.DateTime publish_date { get; set; }
            public string publisher_no { get; set; }
            public string language_no { get; set; }
            public string category_no { get; set; }
            public int sale_price { get; set; }
            public string content_text { get; set; }
            public string detail_text { get; set; }
            public string remark { get; set; }
        }

        /// <summary>
        /// 取得第一筆 book_no
        /// </summary>
        /// <returns></returns>
        public string GetTop1BookNo()
        {
            string bookNo = "";
            using (DapperRepository db = new DapperRepository())
            {
                string queryStr = "select top 1 book_no from Books";
                bookNo = db.GetTable<string>(queryStr).FirstOrDefault();
            }
            return bookNo;
        }
        /// <summary>
        /// 取得 Book 的明細
        /// </summary>
        /// <returns></returns>
        public Books GetBookDetail(string bookNo)
        {
            using (DapperRepository db = new DapperRepository())
            {
                string queryStr = "select *  from Books where book_no = @book_no";
                var model = db.GetTable<Books>(queryStr, new { book_no = bookNo }).FirstOrDefault();            
                return model;
            }        
        }


    }
}