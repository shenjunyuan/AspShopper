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
    
    public partial class Orders
    {
        public int rowid { get; set; }
        public string order_no { get; set; }
        public Nullable<System.DateTime> order_date { get; set; }
        public string order_status { get; set; }
        public string user_no { get; set; }
        public string payment_no { get; set; }
        public string shipping_no { get; set; }
        public string receive_name { get; set; }
        public string receive_mail { get; set; }
        public string receive_address { get; set; }
        public Nullable<int> amount { get; set; }
        public Nullable<int> taxs { get; set; }
        public Nullable<int> totals { get; set; }
        public Nullable<int> order_closed { get; set; }
        public Nullable<int> order_validate { get; set; }
        public string order_guid { get; set; }
        public string receive_email { get; set; }
        public string remark { get; set; }
    }
}
