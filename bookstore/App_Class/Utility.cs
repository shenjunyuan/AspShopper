using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing.Printing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Web;
using System.Web.Configuration;
using System.Web.UI.WebControls;
using System.Globalization;
using bookstore.Models;
using PagedList;


/// <summary>
/// 公用的程式類別
/// </summary>
public static class Utility
{
    /// <summary>
    /// 取得 FormCollection 中讀取的 CheckBox 字串 傳回布林值字串List
    /// </summary>
    /// <param name="valueString">CheckBox 字串</param>
    /// <returns></returns>
    public static List<bool> GetCheckBoxListValue(string valueString)
    {
        List<bool> valueList = new List<bool>();
        if (!string.IsNullOrEmpty(valueString))
        {
            bool bln_value = false;
            bool addValue = true;
            List<string> checkedList = valueString.Split(',').ToList();
            foreach (var value in checkedList)
            {
                bln_value = bool.Parse(value);
                if (bln_value)
                {
                    valueList.Add(bln_value);
                    addValue = false;
                }
                else
                {
                    if (addValue) valueList.Add(false);
                    addValue = true;
                }
            }
        }
        return valueList;
    }

    /// <summary>
    ///  自動取得資料表雙字母的流水號
    /// </summary>
    /// <param name="tableName">資料表名稱</param>
    /// <param name="colName">欄位名稱</param>
    /// <param name="titleName">流水號字母開頭</param>
    /// <returns></returns>
    public static string GetTableNumber(string tableName,string colName,string title)
    {
        string newStr = "";
        DateTime now = DateTime.Today;
        string YY = (now.Year % 1000).ToString(); // 年
        string MM = now.Month.ToString(); // 月
        string DD = now.Day.ToString(); // 日
        string query = $"SELECT isnull(max(RIGHT({colName}, 4)),0) FROM {tableName} where LEFT({colName}, 8) = '{title}{YY}{MM}{DD}'";
        var maxValue = DapperHelp.GetTable<string>(query).FirstOrDefault();
        int maxInt = Convert.ToInt32(maxValue) + 1;
        string padNumber = maxInt.ToString().PadLeft(4, '0'); // 不足位數補 4 個 0
        newStr = string.Format("{0}{1}{2}{3}{4}", title, YY, MM,DD, padNumber);
        return newStr;  
    }


    /// <summary>
    ///  自動取得資料表的單字母流水號-沒日期
    /// </summary>
    /// <param name="tableName">資料表名稱</param>
    /// <param name="colName">欄位名稱</param>
    /// <param name="titleName">流水號字母開頭</param>
    /// <returns></returns>
    public static string GetTableNumNoDate(string tableName, string colName, string title)
    {
        string newStr = "";
        string query = $"SELECT isnull(max(RIGHT({colName}, 5)),0) FROM {tableName} where LEFT({colName}, 1) = '{title}'";
        int maxValue = DapperHelp.GetTable<int>(query).FirstOrDefault();
        int maxInt = maxValue + 1;
        string padNumber = maxInt.ToString().PadLeft(5, '0'); // 不足位數補 5 個 0
        newStr = string.Format("{0}{1}", title, padNumber);
        return newStr;
    }

    /// <summary>
    /// 取得資料表的筆數
    /// </summary>
    /// <param name="tableName">table名稱</param>
    /// <returns></returns>
    public static int GetTableCount(string tableName)
    {    
        int count = 0;
        string queryStr = $"select count(1) from {tableName}"; 
        int dataCount = DapperHelp.GetTable<int>(queryStr).FirstOrDefault(); // 資料筆數
        return count;
    }
    /// <summary>
    /// 字串轉換日期格式 yyyy/MM/dd
    /// </summary>
    /// <param name="inputDateString">要轉換的日期:型別為字串</param>
    /// <param name="type">轉換結果類型 ex: yyyy/MM/dd </param>
    /// <returns></returns>
    public static string FormatDate(string inputDateString, string type)
    {
        string convert_str = "";
        // 會接收到的格式類型
        string[] formats = { "yyyy-MM-dd", "MM/dd/yyyy", "dd/MM/yyyy", "yyyy-MM-dd HH:mm:ss", "dd-MMM-yyyy", "yyyyMMddTHHmmss", "yyyy-MM-dd HH:mm:ss.fff", "yyyy/MM/dd tt hh:mm:ss" };
        DateTime dateTime;
        // 嘗試解析日期
        if (DateTime.TryParseExact(inputDateString, formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTime))
        {
            // 檢查 type 是否為有效值
            if (type == "yyyy/MM/dd" || type == "yyyy-MM-dd")
            {
                // 格式化日期，根據 type 決定結果
                convert_str = dateTime.ToString(type, CultureInfo.InvariantCulture);
            }
            else
            {
                // type 不是有效值的處理邏輯
                convert_str = "無效的日期格式";
            }
        }
        else
        {
            // 解析失敗的處理邏輯
            convert_str = "日期格式異常，無法解析";
        }
        return convert_str;
    }

    /// <summary>
    /// 日期轉換格式 yyyy/MM/dd(型別:日期)
    /// </summary>
    /// <param name="inputDateString">要轉換的日期:型別為日期</param>
    /// <param name="type">轉換結果類型 ex: yyyy/MM/dd </param>
    /// <returns></returns>
    public static string FormatDate(DateTime inputDateTime, string type)
    {
        string convert_str = "";
        // 會接收到的格式類型
        // 檢查 type 是否為有效值
        if (type == "yyyy/MM/dd" || type == "yyyy-MM-dd")
        {
            // 格式化日期，根據 type 決定結果
            convert_str = inputDateTime.ToString(type, CultureInfo.InvariantCulture);
        }
        else
        {
            // type 不是有效值的處理邏輯
            convert_str = "無效的日期格式";
        }
        return convert_str;
    }


}