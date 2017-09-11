using Microsoft.Practices.Unity;
using rainbow.BLL;
using rainbow.Common;
using rainbow.Common.JSON;
using rainbow.Common.PageHelp;
using rainbow.Common.Utils;
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
    public class SysRoleController : BaseController
    {
        [Dependency]
        public ISysRoleBLL roleBLL { get; set; }
        //
        // GET: /SysRole/
        [SupportFilter]
        public ActionResult Index()
        {
            ViewBag.Perm = GetPermission();
            return View();
        }
        [SupportFilter(ActionName="Index")]
        [HttpPost]
        public JsonResult GetList(GridPager pager, string queryStr)
        {
            List<SysRoleModel> list = roleBLL.GetList(ref pager, queryStr);
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
                            CreatePerson = r.CreatePerson,
                            UserName = r.UserName
                        }).ToArray()
            };
            return Json(json, JsonRequestBehavior.AllowGet);
        }

        [SupportFilter]
        public ActionResult Create()
        {
            ViewBag.Perm = GetPermission();
            return View();
        }
        [SupportFilter]
        [HttpPost]
        public ActionResult Create(SysRoleModel model)
        {
            model.Id = ResultHelper.NewId;
            model.CreateTime = ResultHelper.NowTime;
            model.CreatePerson = this.GetUserId();
            //model.Flag = "111";
            //model.UserName = "111";
            if (model != null && ModelState.IsValid)
            {
                if (roleBLL.Create(ref errors, model))
                {
                    LogHandler.WriteServiceLog(GetUserId(), "Id" + model.Id + ",Name" + model.Name, "成功", "创建", "SysRole");
                    return Json(JsonHandler.CreateJsonMessage(true, Suggestion.InsertSucceed), JsonRequestBehavior.AllowGet);
                }
                else
                {
                    string ErrorCol = errors.Error;
                    LogHandler.WriteServiceLog(GetUserId(), "Id" + model.Id + ",Name" + model.Name + "," + ErrorCol, "失败", "创建", "SysRole");
                    return Json(JsonHandler.CreateJsonMessage(false, Suggestion.InsertFail+ErrorCol), JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                 LogHandler.WriteServiceLog(GetUserId(), "Id" + model.Id + ",Name" + model.Name + "," +"没有通过验证", "失败", "创建", "SysRole");
                 return Json(JsonHandler.CreateJsonMessage(false, "数据没有通过验证"), JsonRequestBehavior.AllowGet);
                // return View(model);
            }
        }

        [SupportFilter]
        public ActionResult Edit(string id)
        {
            ViewBag.Perm = GetPermission();
            SysRoleModel role = roleBLL.GetById(id);
            return View(role);
        }
        [SupportFilter]
        [HttpPost]
        public JsonResult Edit(SysRoleModel model)
        {
            if (model != null && ModelState.IsValid)
            {
                if (roleBLL.Edit(ref errors,model))
                {
                    LogHandler.WriteServiceLog(GetUserId(), "Id" + model.Id + ",Name" + model.Name, "成功", "修改", "SysRole");
                    return Json(JsonHandler.CreateJsonMessage(true, Suggestion.EditSucceed), JsonRequestBehavior.AllowGet);
                }
                else
                {
                    string ErrorCol = errors.Error;
                    LogHandler.WriteServiceLog(GetUserId(), "Id" + model.Id + ",Name" + model.Name + "," + ErrorCol, "失败", "修改", "SysRole");
                    return Json(JsonHandler.CreateJsonMessage(false, Suggestion.EditFail+ErrorCol), JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                LogHandler.WriteServiceLog(GetUserId(), "Id" + model.Id + ",Name" + model.Name + "," + "数据验证失败", "失败", "修改", "SysRole");
                return Json(JsonHandler.CreateJsonMessage(false, "数据验证失败"), JsonRequestBehavior.AllowGet);
            }
        }

        [SupportFilter]
        public ActionResult Details(string id)
        {
            ViewBag.Perm = GetPermission();
            SysRoleModel role = roleBLL.GetById(id);
            return View(role);
        }
        [SupportFilter]
        [HttpPost]
        public JsonResult Delete(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                SysRoleModel role = roleBLL.GetById(id);
                if (role == null)
                {
                    LogHandler.WriteServiceLog(GetUserId(), "Id" + id + "," + "要删除的数据不存在", "失败", "删除", "SysRole");
                    return Json(JsonHandler.CreateJsonMessage(false, "删除的数据不存在"), JsonRequestBehavior.AllowGet);
                }
                if (roleBLL.Delete(ref errors, id))
                {
                    LogHandler.WriteServiceLog(GetUserId(), "Id" + id, "成功", "删除", "SysRole");
                    return Json(JsonHandler.CreateJsonMessage(true, Suggestion.DeleteSucceed), JsonRequestBehavior.AllowGet);
                }
                else
                {
                    string error = errors.Error;
                    LogHandler.WriteServiceLog(GetUserId(), "Id" + id + ",Error:" + error, "失败", "删除", "SysRole");
                    return Json(JsonHandler.CreateJsonMessage(false, Suggestion.DeleteFail + error), JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                LogHandler.WriteServiceLog(GetUserId(), "Id" + id + "," + "没有正确获取的删除数据的id", "失败", "删除", "SysRole");
                return Json(JsonHandler.CreateJsonMessage(false, "没有正确获取的删除数据的id"), JsonRequestBehavior.AllowGet);
            }
        }
	}
}