using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using Dapper;


public class DapperRepository : BaseClass
{
    private  string connect_str { get; set; } = WebConfigurationManager.AppSettings["SQLConnectStr"].ToString();

    /// <summary>
    /// 取得 Table 資料
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="queryStr">查詢SQL語句</param>
    /// <returns></returns>
    public List<T> GetTable<T>(string queryStr)
    {
        List<T> results = null;
        using (SqlConnection conn = new SqlConnection(connect_str))
        {
            results = conn.Query<T>(queryStr).AsList();
        }
        return results;
    }
    /// <summary>
    /// Insert Table 資料
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="insertStr">Insert語法</param>
    /// <param name="record">類別名稱</param>

    public void InsertTable<T>(string insertStr, T record)
    {
        using (SqlConnection conn = new SqlConnection(connect_str))
        {
            conn.Open();
            conn.Execute(insertStr, record);
        }
    }
    /// <summary>
    /// 更新 Table 資料
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="updateStr">更新語法</param>
    /// <param name="record">類別名稱</param>
    public void UpdateTable<T>(string updateStr, T record)
    {
        using (SqlConnection conn = new SqlConnection(connect_str))
        {
            conn.Open();
            conn.Execute(updateStr, new { Id = 0, record });
        }
    }
    /// <summary>
    /// 刪除 Table 資料
    /// </summary>
    /// <param name="del_str">刪除語法</param>
    public void DeleteTableData(string del_str)
    {
        using (SqlConnection conn = new SqlConnection(connect_str))
        {
            conn.Open();
            conn.Execute(del_str, new { Id = 0 });
        }
    }






}