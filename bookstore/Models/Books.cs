//------------------------------------------------------------------------------
// <auto-generated>
//     這個程式碼是由範本產生。
//
//     對這個檔案進行手動變更可能導致您的應用程式產生未預期的行為。
//     如果重新產生程式碼，將會覆寫對這個檔案的手動變更。
// </auto-generated>
//------------------------------------------------------------------------------

namespace bookstore.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Books
    {
        public int rowid { get; set; }
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
        public Nullable<int> qty_in { get; set; }
        public Nullable<int> qty_out { get; set; }
        public Nullable<int> qty_now { get; set; }
        public string remark { get; set; }
    }
}
