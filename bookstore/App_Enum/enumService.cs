using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// 權限設定模式枚舉類型
/// </summary>
public enum enumSecuritys
{
    /// <summary>
    /// 瀏覽資料
    /// </summary>
    Browse = 0,
    /// <summary>
    /// 新增
    /// </summary>
    Add = 1,
    /// <summary>
    /// 修改
    /// </summary>
    Edit = 2,
    /// <summary>
    /// 刪除
    /// </summary>
    Delete = 3,
    /// <summary>
    /// 列印
    /// </summary>
    Print = 4,
    /// <summary>
    /// 上傳
    /// </summary>
    Upload = 5,
    /// <summary>
    /// 下載
    /// </summary>
    Download = 6,
    /// <summary>
    /// 審核
    /// </summary>
    Confirm = 7,
    /// <summary>
    /// 回復
    /// </summary>
    Undo = 8
}

/// <summary>
/// 資料排序方向枚舉類型
/// </summary>
public enum enumSortDirection
{
    /// <summary>
    /// 由小到大排
    /// </summary>
    Asc = 0,
    /// <summary>
    /// 由大到小排
    /// </summary>
    Desc = 1
}