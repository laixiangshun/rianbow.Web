using Microsoft.Practices.Unity;
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
    public class SysModuleController : BaseController
    {
        [Dependency]
        public ISysModuleOperateBLL sysModuleOperationBLL { get; set; }

        [Dependency]
        public ISysModuleBLL sysModuleBLL { get; set; }
        //
        // GET: /SysModule/
        [SupportFilter]
        public ActionResult Index()
        {
            ViewBag.Perm = GetPermission();
            return View();
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [SupportFilter(ActionName="Index")]
        [HttpPost]
        public JsonResult GetList(string id)
        {
            if(id==null){
                id = "0";
            }
            List<SysModuleModel> list = sysModuleBLL.GetList(id);
            var json=from r in list
                     select new SysModuleModel{
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
                           state=(sysModuleBLL.GetList(r.Id).Count>0)?"closed":"open"
                     };
            return Json(json,JsonRequestBehavior.AllowGet);
        }

        [SupportFilter(ActionName="Index")]
        [HttpPost]
        public JsonResult GetOptListByModule(GridPager pager, string mid)
        {
            pager.rows = 1000;
            pager.page = 1;
            List<SysModuleOperateModel> list = sysModuleOperationBLL.GetList(ref pager, mid);
            var json=new{
                total=pager.totalRows,
                rows=(from r in list
                          select new SysModuleOperateModel{
                                Id = r.Id,
                                Name = r.Name,
                                KeyCode = r.KeyCode,
                                ModuleId = r.ModuleId,
                                IsValid = r.IsValid,
                                Sort = r.Sort
                          }).ToArray()
            };
            return Json(json, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 创建模块
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [SupportFilter]
        public ActionResult Create(string id)
        {
            ViewBag.Perm = GetPermission();
            SysModuleModel entity = new SysModuleModel
            {
                ParentId = id,
                Enable = true,
                Sort = 0
            };
            return View(entity);
        }

        [HttpPost]
        [SupportFilter]
        public JsonResult Create(SysModuleModel model) {
            model.Id = ResultHelper.NewId;
            model.CreateTime=ResultHelper.NowTime;
            model.CreatePerson = GetUserId();
            if (model != null && ModelState.IsValid)
            {
                if (sysModuleBLL.Create(ref errors, model))
                {
                    LogHandler.WriteServiceLog(GetUserId(), "Id" + model.Id + ",Name" + model.Name, "成功", "创建", "SysModule");
                    return Json(JsonHandler.CreateJsonMessage(true, Suggestion.InsertSucceed));
                }
                else
                {
                    string ErrorCol = errors.Error;
                    LogHandler.WriteServiceLog(GetUserId(), "Id" + model.Id + ",Name" + model.Name + "," + ErrorCol, "失败", "创建", "SysModule");
                    return Json(JsonHandler.CreateJsonMessage(false, Suggestion.InsertFail + ErrorCol));
                }
            }
            else
            {
                return Json(JsonHandler.CreateJsonMessage(false, Suggestion.InsertFail));
            }
        }

        [SupportFilter(ActionName = "Create")]
        public ActionResult CreateOpt(string moduleId)
        {
            ViewBag.Perm = GetPermission();
            SysModuleOperateModel sysModuleOptModel = new SysModuleOperateModel();
            sysModuleOptModel.ModuleId = moduleId;
            sysModuleOptModel.IsValid = true;
            return View(sysModuleOptModel);
        }

        [HttpPost]
        [SupportFilter(ActionName = "Create")]
        public JsonResult CreateOpt(SysModuleOperateModel info)
        {
            if (info != null && ModelState.IsValid)
            {
                SysModuleOperateModel entity = sysModuleOperationBLL.GetById(info.Id);
                if (entity != null)
                    return Json(JsonHandler.CreateJsonMessage(false, Suggestion.PrimaryRepeat), JsonRequestBehavior.AllowGet);
                entity = new SysModuleOperateModel();
                entity.Id = info.ModuleId + info.KeyCode;
                entity.Name = info.Name;
                entity.KeyCode = info.KeyCode;
                entity.ModuleId = info.ModuleId;
                entity.IsValid = info.IsValid;
                entity.Sort = info.Sort;

                if (sysModuleOperationBLL.Create(ref errors, entity))
                {
                    LogHandler.WriteServiceLog(GetUserId(), "Id:" + info.Id + ",Name:" + info.Name, "成功", "创建", "模块设置");
                    return Json(JsonHandler.CreateJsonMessage(true, Suggestion.InsertSucceed), JsonRequestBehavior.AllowGet);
                }
                else
                {
                    string ErrorCol = errors.Error;
                    LogHandler.WriteServiceLog(GetUserId(), "Id:" + info.Id + ",Name:" + info.Name + "," + ErrorCol, "失败", "创建", "模块设置");
                    return Json(JsonHandler.CreateJsonMessage(false, Suggestion.InsertFail + ErrorCol), JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(JsonHandler.CreateJsonMessage(false, Suggestion.InsertFail), JsonRequestBehavior.AllowGet);
            }
        }

        [SupportFilter]
        public ActionResult Edit(string id)
        {
            ViewBag.Perm = GetPermission();
            SysModuleModel entity = sysModuleBLL.GetById(id);
            return View(entity);
        }


        [HttpPost]
        [SupportFilter]
        public JsonResult Edit(SysModuleModel model)
        {
            if (model != null && ModelState.IsValid)
            {
                if (sysModuleBLL.Edit(ref errors, model))
                {
                    LogHandler.WriteServiceLog(GetUserId(), "Id" + model.Id + ",Name" + model.Name, "成功", "修改", "系统菜单");
                    return Json(JsonHandler.CreateJsonMessage(true, Suggestion.EditSucceed));
                }
                else
                {
                    string ErrorCol = errors.Error;
                    LogHandler.WriteServiceLog(GetUserId(), "Id" + model.Id + ",Name" + model.Name + "," + ErrorCol, "失败", "修改", "系统菜单");
                    return Json(JsonHandler.CreateJsonMessage(false, Suggestion.EditFail + ErrorCol));
                }
            }
            else
            {
                return Json(JsonHandler.CreateJsonMessage(false, Suggestion.EditFail));
            }
        }


        [HttpPost]
        [SupportFilter]
        public JsonResult Delete(string id)
        {
            if (!string.IsNullOrWhiteSpace(id))
            {
                if (sysModuleBLL.Delete(ref errors, id))
                {
                    LogHandler.WriteServiceLog(GetUserId(), "Ids:" + id, "成功", "删除", "模块设置");
                    return Json(JsonHandler.CreateJsonMessage(true, Suggestion.DeleteSucceed), JsonRequestBehavior.AllowGet);
                }
                else
                {
                    string ErrorCol = errors.Error;
                    LogHandler.WriteServiceLog(GetUserId(), "Id:" + id + "," + ErrorCol, "失败", "删除", "模块设置");
                    return Json(JsonHandler.CreateJsonMessage(false, Suggestion.DeleteFail + ErrorCol), JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(JsonHandler.CreateJsonMessage(false, Suggestion.DeleteFail), JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        [SupportFilter(ActionName = "Delete")]
        public JsonResult DeleteOpt(string id)
        {
            if (!string.IsNullOrWhiteSpace(id))
            {
                if (sysModuleOperationBLL.Delete(ref errors, id))
                {
                    LogHandler.WriteServiceLog(GetUserId(), "Id:" + id, "成功", "删除", "模块设置KeyCode");
                    return Json(JsonHandler.CreateJsonMessage(true, Suggestion.DeleteSucceed), JsonRequestBehavior.AllowGet);
                }
                else
                {
                    string ErrorCol = errors.Error;
                    LogHandler.WriteServiceLog(GetUserId(), "Id:" + id + "," + ErrorCol, "失败", "删除", "模块设置KeyCode");
                    return Json(JsonHandler.CreateJsonMessage(false, Suggestion.DeleteFail + ErrorCol), JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(JsonHandler.CreateJsonMessage(false, Suggestion.DeleteFail), JsonRequestBehavior.AllowGet);
            }


        }
	}
}