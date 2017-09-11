using Microsoft.Practices.Unity;
using rainbow.BLL;
using rainbow.Common.PageHelp;
using rainbow.Models;
using rainbow.Models.Sys;
using rainbow.Web.Core;
using rianbow.IBLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace rainbow.Web.Controllers
{
    public class SysSampleController : BaseController
    {
        //Unity依赖注入
        [Dependency]
        public ISysSampleBLL SysSampleService { get; set; }

       // ISysSampleBLL SysSampleService = new SysSampleBLL();
        //
        // GET: /SysSample/
        public ActionResult Index()
        {
            
           // List<SysSampleModel> list = SysSampleService.GetList("");
            //return View(list);
            ViewBag.Perm = GetPermission();
            LogHandler.WriteServiceLog(this.GetUserName(), "进入主页面", "成功", "主页面", "样例程序");
            return View("List");
        }
        public ActionResult List()
        {
            LogHandler.WriteServiceLog(this.GetUserName(), "进入主页面", "成功", "主页面", "样例程序");
            return View();
        }
        /// <summary>
        /// 没封装过的查询，排序，分页方法
        /// </summary>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="sort"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetList(int page=1,int rows=10,string sort="Id",string order="desc")
        {
            int total = 0; //ref关键字使参数按引用传递,方法定义和调用方法都必须显式使用ref关键字
            List<SysSampleModel> list = SysSampleService.GetList(page,rows,sort,order,ref total);
            var json=new Object();
            if (list != null && list.Count > 0)
            {
                json = new
                {
                    total = total,
                    rows = (from r in list
                            select new SysSampleModel()
                            {
                                Id = r.Id,
                                Name = r.Name,
                                Age = r.Age,
                                Bir = r.Bir,
                                Photo = r.Photo,
                                Note = r.Note,
                                CreateTime = r.CreateTime
                            }).ToArray()
                };
            }
            else
            {
                json = null;
            }
            return Json(json,"json", Encoding.UTF8, JsonRequestBehavior.AllowGet);//返回重写的Json方法，对DateTime的格式进行处理
            //return MyJson(json, "yyyy-MM-dd HH:mm:ss");
        }

        /// <summary>
        /// 封装过的条件，排序，分页查询方法
        /// </summary>
        /// <param name="pager"></param>
        /// <returns></returns>
        [SupportFilter(ActionName="Index")]
        [HttpPost]
        public JsonResult GetListData(GridPager pager, string queryStr)
        {
            List<SysSampleModel> list = SysSampleService.GetListByQuery(ref pager, queryStr);
            var json = new object();
            if (list != null && list.Count > 0)
            {
                json=new
                {
                    total = pager.totalRows,
                    rows = (from r in list
                            select new SysSampleModel
                            {
                                Id = r.Id,
                                Name = r.Name,
                                Age = r.Age,
                                Bir = r.Bir,
                                Photo = r.Photo,
                                Note = r.Note,
                                CreateTime = r.CreateTime,
                            }).ToArray()
                };
            }
            else
            {
                json = new { total = 0, rows = 0 };
            }
            LogHandler.WriteServiceLog(this.GetUserName(), "查询数据并分页", "成功", "查询", "样例程序");
            return Json(json, "json", Encoding.UTF8, JsonRequestBehavior.AllowGet);
        }

        [SupportFilter]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create([Bind(Include = "Id,Name,Age,Bir,Note,CreateTime,Photo")] SysSampleModel model)
        {
            
            if (ModelState.IsValid)
            {
                if (SysSampleService.Create(ref errors, model))
                {
                    LogHandler.WriteServiceLog(user.TrueName, "Id:" + model.Id + ",Name:" + model.Name, "成功", "创建", "样例程序");
                    return Json(new { success = true,message="添加成功" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    string errorMsg = errors.Error;
                    LogHandler.WriteServiceLog(user.TrueName, "Id:" + model.Id + ",Name:" + model.Name + ",ErrorMessage:" + errorMsg, "失败", "创建", "样例程序");
                    return Json(new { success = false,message=errorMsg }, JsonRequestBehavior.AllowGet);
                }
            }
            return View(model);
        }
        [SupportFilter]
        public ActionResult Edit(string id)
        {
            SysSampleModel model = SysSampleService.GetById(id);
            return View(model);
        }
        [HttpPost]
        public ActionResult Edit([Bind(Include = "Id,Name,Age,Bir,Note,CreateTime,Photo")] SysSampleModel model)
        {
            if (ModelState.IsValid)
            {
                if (SysSampleService.Edit(ref errors,model))
                {
                    LogHandler.WriteServiceLog(user.TrueName, "Id:" + model.Id + ",Name:" + model.Name, "成功", "修改", "样例程序");
                    return Json(new { success = true }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    string errorMsg = errors.Error;
                    LogHandler.WriteServiceLog(user.TrueName, "Id:" + model.Id + ",Name:" + model.Name + ",ErrorMessage:" + errorMsg, "失败", "修改", "样例程序");
                    return Json(new { success = false,message=errorMsg}, JsonRequestBehavior.AllowGet);
                }
            }
            return View(model);
        }
        [SupportFilter]
        public ActionResult Details(string id)
        {
            SysSampleModel model = SysSampleService.GetById(id);
            return View(model);
        }

        [HttpPost]
        public JsonResult Delete(string id)
        {
            string errorMsg = errors.Error;
            if (!string.IsNullOrEmpty(id))
            {
                if (SysSampleService.Delete(ref errors,id))
                {
                    LogHandler.WriteServiceLog(user.TrueName, "删除Id:" + id +"的数据成功", "成功", "删除", "样例程序");
                    return Json(new { success = true, message = "删除成功" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    LogHandler.WriteServiceLog(user.TrueName, "删除Id:" +id + "的记录出错,ErrorMessage:" + errorMsg, "失败", "删除", "样例程序");
                    return Json(new { success = false, message = errorMsg }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                LogHandler.WriteServiceLog(user.TrueName, "删除Id:" + id + "的记录出错,ErrorMessage:参数id为空" , "失败", "删除", "样例程序");
                return Json(new { success = false, message = "参数错误" }, JsonRequestBehavior.AllowGet);
            }
        }
	}
}