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
    
    public partial class Publishers
    {
        public int rowid { get; set; }
        public string publisher_no { get; set; }
        public string publisher_name { get; set; }
        public string boss_name { get; set; }
        public string contact_name { get; set; }
        public string company_telphone { get; set; }
        public string contact_telphone { get; set; }
        public string company_address { get; set; }
        public string remark { get; set; }
    }
}
