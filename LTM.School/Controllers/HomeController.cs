using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LTM.School.Models;
using LTM.School.EntityFramework;
using LTM.School.ViewModels;

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

    public IActionResult About()
    {
      ViewData["Message"] = "学生信息统计";

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
