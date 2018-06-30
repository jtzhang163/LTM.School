using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LTM.School.Models;
using LTM.School.EntityFramework;
using LTM.School.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace LTM.School.Controllers
{
  public class HomeController : Controller
  {

    private readonly SchoolDbContext _context;
    public HomeController(SchoolDbContext context)
    {
      _context = context;
    }

    public IActionResult Index()
    {
      return View();
    }

    public async Task<IActionResult> About()
    {
      ViewData["Message"] = "学生信息统计";

      var conn = _context.Database.GetDbConnection();
      var studentCount = 0;

      try
      {
        await conn.OpenAsync();
        using (var command = conn.CreateCommand())
        {
          var sql = "SELECT COUNT(*) FROM Student";
          command.CommandText = sql;
          DbDataReader reader = await command.ExecuteReaderAsync();
          if (reader.HasRows)
          {
            if (await reader.ReadAsync())
            {
              studentCount = reader.GetInt32(0);
            }
          }
        }
        ViewData["StudentCount"] = studentCount;
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.Message);
        throw;
      }
      finally
      {
        conn.Close();
      }


      var entities = from s in _context.Students
                     group s by s.EnrollmnetDate into g
                     select new EnrollmentDateGroup { EnrollmentDate = g.Key, Count = g.Count() };

      return View(entities);
    }

    public IActionResult Contact()
    {
      ViewData["Message"] = "Your contact page.";

      return View();
    }

    public IActionResult Error()
    {
      return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
  }
}
