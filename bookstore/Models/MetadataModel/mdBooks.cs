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
        [Display(Name = "廠商名稱")]
        public string publisher_name { get { using (tblBooks books = new tblBooks()) { return books.GetPublisherName(book_no); } } }
        [Display(Name = "類型")]
        public string category_name { get { using (tblBooks books = new tblBooks()) { return books.GetCategoryName(book_no); } } }
        [Display(Name = "語言")]
        public string language_name { get { using (tblBooks books = new tblBooks()) { return books.GetLanguageName(book_no); } } }

        //下拉選單代值
        public List<Publishers> PublishersList { get { using (tblPublishers publishers = new tblPublishers()) { return publishers.PublishersList();} } }
        public List<Languages> LanguagesList { get { using (tblLanguages language = new tblLanguages()) { return language.LanguagesList();} } }
        public List<Categorys> CategorysList { get { using (tblCategorys categorys = new tblCategorys()) { return categorys.GetCategoryDataList(); } } }

        private class mdBooks
        {
            [Key]
            public int rowid { get; set; }
            [Display(Name = "商品編號")]
            public string book_no { get; set; }
            public string isbn_no { get; set; }
            [Display(Name = "商品名稱")]
            public string book_name { get; set; }
            public string author_name { get; set; }
            [Display(Name = "上架時間")]
            [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
            public System.DateTime publish_date { get; set; }
            [Display(Name = "廠商代碼")]
            [Required(ErrorMessage = "廠商不可空白")]
            public string publisher_no { get; set; }
            [Display(Name = "語言代碼")]
            [Required(ErrorMessage = "語言不可空白")]
            public string language_no { get; set; }
            [Display(Name = "類型代碼")]
            [Required(ErrorMessage = "類型不可空白")]
            public string category_no { get; set; }
            [Display(Name = "商品價格")]
            public int sale_price { get; set; }
            public string content_text { get; set; }
            [Display(Name = "商品敘述")]
            public string detail_text { get; set; }
            [Display(Name = "備註")]
            public string remark { get; set; }
        }

        public Books()
        {
        }

        /// <summary>
        /// 取得第一筆 book_no
        /// </summary>
        /// <returns></returns>
        public string GetTop1BookNo()
        {
            string bookNo = "";

            string queryStr = "select top 1 book_no from Books";
            bookNo = DapperHelp.GetTable<string>(queryStr).FirstOrDefault();

            return bookNo;
        }
        /// <summary>
        /// 取得 Book 的明細
        /// </summary>
        /// <returns></returns>
        public Books GetBookDetail(string bookNo)
        {
           string queryStr = "select *  from Books where book_no = @book_no";
           var model = DapperHelp.GetTable<Books>(queryStr, new { book_no = bookNo }).FirstOrDefault();            
              return model;
       
        }


    }
}