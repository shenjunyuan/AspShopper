using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using Dapper;


/* 
   //在某個地方調用 GetTable 範例                  
   string query = "SELECT * FROM YourTable WHERE Id = @Id AND Name = @Name";
   var paras = new { Id = 1, Name = "John" }; // 摻數
   List<YourDataModel> result = GetTable<YourDataModel>(query, parameters)
*/
/// <summary>
/// 使用 Dapper 執行SQL語法的 Class
/// </summary>
public static class DapperHelp
{
    private static string connect_str { get; set; } = SessionService.GetAppSettings("SQLConnectStr", "");
    /// <summary>
    /// 取得 Table 資料
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="queryStr">查詢SQL語句</param>
    /// /// <param name="parameters">傳入參數</param>
    /// <returns></returns>
    public static List<T> GetTable<T>(string queryStr, object parameters = null)
    {
        List<T> results = null;
        using (SqlConnection conn = new SqlConnection(connect_str))
        {
            try
            {
                results = conn.Query<T>(queryStr, parameters).AsList();
            }
            catch (Exception ex)
            {
                // 在這裡可以進行異常處理，例如紀錄異常訊息
                Console.WriteLine($"錯誤訊息: {ex.Message}");
                // 返回一個空的 List<T>
                results = new List<T>();
            }
        }
        return results;
    }

    /// <summary>
    /// 執行SQL語法 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="SQLStr">更新語法</param>
    /// <param name="parameters">條件參數@</param>
    public static void ExecuteSQL<T>(string SQLStr, object parameters = null)
    {
        using (SqlConnection conn = new SqlConnection(connect_str))
        {
            conn.Open();
            conn.Execute(SQLStr, parameters);
        }
    }

    /// <summary>
    /// 執行SQL語法-有Rollback功能
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="SQLStr">更新語法</param>
    /// <param name="parameters">條件參數@</param>
    public static void ExecuteSQLWithTransaction(string SQLStr, object parameters = null)
    {
        using (SqlConnection conn = new SqlConnection(connect_str))
        {
            conn.Open();
            SqlTransaction transaction = conn.BeginTransaction();

            try
            {
                // 執行 SQL 操作
                conn.Execute(SQLStr, parameters, transaction);
                transaction.Commit();
            }
            catch (Exception ex)
            {
                // 如果發生錯誤，Rollback
                transaction.Rollback();
                Console.WriteLine($"錯誤訊息: {ex.Message}");
            }
        }
    }






}