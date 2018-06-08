using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LTM.School.Common
{
  //分页
  public class PaginatedList<T> : List<T>
  {
    public int PageIndex { get; set; }
    public int TotalPages { get; set; }

    public PaginatedList(List<T> items, int count, int pageIndex, int pageSize)
    {
      PageIndex = pageIndex;
      TotalPages = (int)Math.Ceiling(count / (decimal)pageSize);
      this.AddRange(items);
    }

    //是否有上一页
    public bool HasPreviousPage => PageIndex > 1;

    //是否有下一页
    public bool HasNextPage => PageIndex < TotalPages;

    /// <summary>
    /// 创建分页
    /// </summary>
    /// <param name="source"></param>
    /// <param name="pageIndex"></param>
    /// <param name="pageSize"></param>
    /// <returns></returns>
    public static async Task<PaginatedList<T>> CreateAsync(IQueryable<T> source, int pageIndex,int pageSize)
    {
      var count = await source.CountAsync();
      var items = await source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();

      return new PaginatedList<T>(items, count, pageIndex, pageSize);
    }
  }
}
