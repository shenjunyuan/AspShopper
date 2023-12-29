using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using bookstore.Models;


/// <summary>
/// 公用的程式類別
/// </summary>
public  class Utility:BaseClass
{

    /// <summary>
    /// 取得 FormCollection 中讀取的 CheckBox 字串 傳回布林值字串
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
    /// 取得 Web.config 中的 appSettings 的值
    /// </summary>
    /// <param name="keyName">Key 名</param>
    /// <param name="defaultValue">預設值</param>
    /// <returns></returns>
    public static string GetAppSettings(string keyName, string defaultValue)
    {
        object obj_value = WebConfigurationManager.AppSettings[keyName];
        string str_value = (obj_value == null) ? defaultValue : obj_value.ToString();
        return str_value;
    }


    /// <summary>
    ///  自動取得資料表的流水號
    /// </summary>
    /// <param name="tableName">資料表名稱</param>
    /// <param name="colName">欄位名稱</param>
    /// <param name="titleName">流水號字母開頭</param>
    /// <returns></returns>
    public static string GetTableNumber(string tableName,string colName,string titleName)
    {
        string newStr = "";
        using (DapperRepository db = new DapperRepository())
        {
            DateTime now = DateTime.Today;
            string YY = (now.Year % 1000).ToString(); // 年
            string MM = now.Month.ToString(); // 月
            string DD = now.Day.ToString(); // 日
            string title = titleName;
            string query = $"SELECT isnull(max(RIGHT({colName}, 4)),0) FROM {tableName} where LEFT({colName}, 8) = '{title}{YY}{MM}{DD}'";
            var maxValue = db.GetTable<string>(query).FirstOrDefault();
            int maxInt = Convert.ToInt32(maxValue) + 1;
            string padNumber = maxInt.ToString().PadLeft(4, '0'); // 不足位數補 4 個0
             newStr = string.Format("{0}{1}{2}{3}{4}", titleName, YY, MM,DD, padNumber);
        }
        return newStr;  
    }

}