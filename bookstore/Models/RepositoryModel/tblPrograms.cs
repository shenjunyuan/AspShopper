using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using bookstore.Models;

public class tblPrograms : BaseClass
{
    public IRepository<Programs> repo;
    public tblPrograms()
    {
        repo = new EFGenericRepository<Programs>(new bookstoreEntities());
    }
    /// <summary>
    /// 開啟程式
    /// </summary>
    /// <param name="prgNo">程式代號</param>
    public void PrgOpen(string prgNo)
    {
        var data = repo.ReadSingle(m => m.program_no == prgNo);
        if (data != null)
        {
            using (tblModules modules = new tblModules())
            {
                using (tblSecuritys securitys = new tblSecuritys())
                {
                    using (tblProgramTypes programTypes = new tblProgramTypes())
                    {
                        SessionService.ModuleNo = data.module_no;
                        SessionService.ModuleName = modules.GetModuleName(data.module_no);
                        SessionService.PrgNo = data.program_no;
                        SessionService.PrgName = data.program_name;
                        SessionService.PrgIcon = programTypes.GetIconName(data.program_type_no) + " mr-1";
                        SessionService.AreaName = data.area_name;
                        SessionService.ControllerName = data.controller_name;
                        SessionService.ActionName = data.action_name;
                        SessionService.ActionParm = data.parameter_value;
                        SessionService.PrgSecuritys = securitys.GetProgramSecurity(data.program_no);
                    }
                }
            }
        }
    }
    /// <summary>
    /// 取得圖示名稱
    /// </summary>
    /// <param name="prgNo">程式代號</param>
    /// <returns></returns>
    public string GetProgramIconName(string prgNo)
    {
        using (tblProgramTypes programTypes = new tblProgramTypes())
        {
            string str_value = "";
            var data = repo.ReadSingle(m => m.program_no == prgNo);
            if (data != null) str_value = programTypes.GetIconName(data.program_type_no);
            return str_value;
        }
    }
}
