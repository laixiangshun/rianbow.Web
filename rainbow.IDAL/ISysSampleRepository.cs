using rainbow.Common.PageHelp;
using rainbow.Models;
using rainbow.Models.Sys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rainbow.IDAL
{
    public interface ISysSampleRepository
    {
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="db">数据库上下文</param>
        /// <returns>数据列表</returns>
        List<SysSampleModel> GetList(int page, int rows, string sort, string order, ref int total);
        /// <summary>
        /// 创建一个实体
        /// </summary>
        /// <param name="entity">实体</param>
        int Create(SysSample entity);
        /// <summary>
        /// 删除一个实体
        /// </summary>
        /// <param name="entity">主键ID</param>
        bool Delete(string id);

        /// <summary>
        /// 修改一个实体
        /// </summary>
        /// <param name="entity">实体</param>
        int Edit(SysSample entity);
        /// <summary>
        /// 获得一个实体
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>实体</returns>
        SysSample GetById(string id);
        /// <summary>
        /// 判断一个实体是否存在
        /// </summary>
        bool IsExist(string id);

        /// <summary>
        /// 封装的条件查询
        /// </summary>
        /// <param name="pager"></param>
        /// <returns></returns>
        List<SysSampleModel> GetListByQuery(ref GridPager pager, string queryStr);
    }
}
