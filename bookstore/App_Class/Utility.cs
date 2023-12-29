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


}