using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace rainbow.Common.LinkHelp
{
    /// <summary>
    /// Linq动态排序
    /// </summary>
     public class LinkHelper
    {
         /// <summary>
         /// 根据linkq动态排序
         /// </summary>
         /// <typeparam name="T"></typeparam>
         /// <param name="source"></param>
         /// <param name="sortExpression"></param>
         /// <param name="sortDirection"></param>
         /// <returns></returns>
         public static IQueryable<T> DataSorting<T>(IQueryable<T> source, string sortExpression, string sortDirection)
         {
             string sortingDir = string.Empty;
             if (sortDirection.ToUpper().Trim() == "ASC")
                 sortingDir = "OrderBy";
             else if(sortDirection.ToUpper().Trim()=="DESC")
             {
                 sortingDir = "OrderByDescending";
             }
             ParameterExpression param = Expression.Parameter(typeof(T), sortExpression);//创建参数表达式
             PropertyInfo pi = typeof(T).GetProperty(sortExpression);//获取指定名称的公共属性-----反射
             Type[] types = new Type[2];
             types[0] = typeof(T);
             types[1] = pi.PropertyType;//获取属性的类型
             Expression expr = Expression.Call(typeof(Queryable), sortingDir, types, source.Expression, Expression.Lambda(Expression.Property(param, sortExpression), param));
             IQueryable<T> query = source.AsQueryable().Provider.CreateQuery<T>(expr);//生成排序表达式
             return query;
         }
         /// <summary>
         /// 分页
         /// </summary>
         /// <typeparam name="T"></typeparam>
         /// <param name="source"></param>
         /// <param name="pageNumber"></param>
         /// <param name="pageSize"></param>
         /// <returns></returns>
         public static IQueryable<T> DataPaging<T>(IQueryable<T> source, int pageNumber, int pageSize)
         {
             if (pageNumber <= 1)
             {
                 return source.Take(pageSize);
             }
             else
             {
                 return source.Skip((pageNumber - 1) * pageSize).Take(pageSize);
             }
         }

         /// <summary>
         /// 排序并分页
         /// </summary>
         /// <typeparam name="T"></typeparam>
         /// <param name="source"></param>
         /// <param name="sortExpress"></param>
         /// <param name="sortDirection"></param>
         /// <param name="pageNumber"></param>
         /// <param name="pageSize"></param>
         /// <returns></returns>
         public static IQueryable<T> SortingAndPaging<T>(IQueryable<T> source, string sortExpress, string sortDirection, int pageNumber, int pageSize)
         {
             IQueryable<T> query = DataSorting<T>(source, sortExpress, sortDirection);
             return DataPaging<T>(query, pageNumber, pageSize);
         }
    }
}
