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
    /// 取得 Table 資料
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="queryStr">查詢SQL語句</param>
    /// <returns></returns>
    public List<T> GetTable<T>(string queryStr, object param = null)
    {
        List<T> results = null;
        using (SqlConnection conn = new SqlConnection(connect_str))
        {
            if (param != null)
            {
                results = conn.Query<T>(queryStr, param).AsList();
            }
            else
            {
                results = conn.Query<T>(queryStr).AsList();
            }
        }
        return results;
    }

    /// <summary>
    /// 執行SQL語法 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="updateStr">更新語法</param>
    /// <param name="record">類別名稱</param>
    public void ExecuteSQL<T>(string SQLStr, T record)
    {
        using (SqlConnection conn = new SqlConnection(connect_str))
        {
            conn.Open();
            conn.Execute(SQLStr, record);
        }
    }






}