using Microsoft.Practices.Unity;
using rainbow.Common.PageHelp;
using rainbow.Models.Sys;
using rainbow.Web.Core;
using rianbow.IBLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace rainbow.Web.Controllers
{
    public class SysRightController : BaseController
    {
        [Dependency]
        public ISysUserBLL sysRightBLL { get; set; }
        [Dependency]
        public ISysRoleBLL sysRoleBLL { get; set; }
        [Dependency]
        public ISysModuleBLL sysModuleBLL { get; set; }
        //
        // GET: /SysRight/
        [SupportFilter]
        public ActionResult Index()
        {
            ViewBag.Perm = GetPermission();
            return View();
        }
        /// <summary>
        /// 获取角色列表
        /// </summary>
        /// <param name="pager"></param>
        /// <returns></returns>
        [SupportFilter(ActionName="Index")]
        [HttpPost]
        public JsonResult GetRoleList(GridPager pager)
        {
            List<SysRoleModel> list = sysRoleBLL.GetList(ref pager, "");
            var json = new
            {
                total = pager.totalRows,
                rows = (from r in list
                        select new SysRoleModel
                        {
                            Id = r.Id,
                            Name = r.Name,
                            Description = r.Description,
                            CreateTime = r.CreateTime,
                            CreatePerson = r.CreatePerson
                        }).ToArray()
            };
            return Json(json, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 获取模组列表
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [SupportFilter(ActionName="Index")]
        [HttpPost]
        public JsonResult GetModelList(string id)
        {
            if (id == null)
                id = "0";
            List<SysModuleModel> list = sysModuleBLL.GetList(id);
            var json = from r in list
                       select new SysModuleModel
                       {
                           Id = r.Id,
                           Name = r.Name,
                           EnglishName = r.EnglishName,
                           ParentId = r.ParentId,
                           Url = r.Url,
                           Iconic = r.Iconic,
                           Sort = r.Sort,
                           Remark = r.Remark,
                           Enable = r.Enable,
                           CreatePerson = r.CreatePerson,
                           CreateTime = r.CreateTime,
                           IsLast = r.IsLast,
                           state = (sysModuleBLL.GetList(r.Id).Count > 0) ? "closed" : "open"
                       };
            return Json(json, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 根据角色和模块得出权限
        /// </summary>
        /// <param name="pager"></param>
        /// <param name="roleId"></param>
        /// <param name="moduleId"></param>
        /// <returns></returns>
        [SupportFilter(ActionName="Index")]
        [HttpPost]
        public JsonResult GetRightByRoleAndModule(GridPager pager, string roleId, string moduleId)
        {
            pager.rows = 10000;
            var right = sysRightBLL.GetRightByRoleAndModule(roleId, moduleId);
            var json = new
            {
                total = pager.totalRows,
                rows = (from r in right
                        select new SysRightModelByRoleAndModuleModel
                        {
                            Ids = r.RightId + r.KeyCode,
                            Name = r.Name,
                            KeyCode = r.KeyCode,
                            IsValid = r.isvalid,
                            RightId = r.RightId
                        }).ToArray()
            };
            return Json(json, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [SupportFilter(ActionName="Save")]
        public int UpdateRight(SysRightOperateModel model)
        {
            return sysRightBLL.UpdateRight(ref errors, model);
        }
	}
}