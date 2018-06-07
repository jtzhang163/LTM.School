using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LTM.School.Core.Models;
using LTM.School.EntityFramework;

namespace LTM.School.Controllers
{
  public class StudentsController : Controller
  {
    private readonly SchoolDbContext _context;

    public StudentsController(SchoolDbContext context)
    {
      _context = context;
    }

    // GET: Students
    public async Task<IActionResult> Index(string sortOrder)
    {
      ViewData["Name_Sort_Parm"] = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
      ViewData["Date_Sort_Parm"] = sortOrder == "date" ? "date_desc" : "date";

      var dtos = await _context.Students.AsNoTracking().ToListAsync();
      var students = from student in _context.Students select student;
      
      switch (sortOrder)
      {
        case "name_desc": students = students.OrderByDescending(s => s.RealName); break;
        case "date": students = students.OrderBy(s => s.EnrollmnetDate); break;
        case "date_desc": students = students.OrderByDescending(s => s.EnrollmnetDate); break;
        default: students = students.OrderBy(s => s.RealName); break;
      }
      dtos = await students.ToListAsync();
      return View(dtos);
    }

    // GET: Students/Details/5
    public async Task<IActionResult> Details(int? id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var student = await _context.Students.Include(a => a.Enrollments).ThenInclude(e => e.Course).AsNoTracking()
          .SingleOrDefaultAsync(m => m.Id == id);
      if (student == null)
      {
        return NotFound();
      }

      return View(student);
    }

    // GET: Students/Create
    public IActionResult Create()
    {
      return View();
    }

    // POST: Students/Create
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("RealName,EnrollmnetDate")] Student student)
    {

      try
      {
        if (ModelState.IsValid)
        {
          _context.Add(student);
          await _context.SaveChangesAsync();
          //return View(student);
          return RedirectToAction(nameof(Index));
        }
     
      }
      catch (DbUpdateException e)
      {
        ModelState.AddModelError(e.Message,"无法进行数据的保存，请检查数据是否正常！");
      }
      return View(student);  
    }

    // GET: Students/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var student = await _context.Students.SingleOrDefaultAsync(m => m.Id == id);
      if (student == null)
      {
        return NotFound();
      }
      return View(student);
    }

    // POST: Students/Edit/5
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,RealName,EnrollmnetDate")] Student student)
    {
      if (id != student.Id)
      {
        return NotFound();
      }

      if (ModelState.IsValid)
      {
        try
        {
          _context.Update(student);
          await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
          if (!StudentExists(student.Id))
          {
            return NotFound();
          }
          else
          {
            throw;
          }
        }
        return RedirectToAction(nameof(Index));
      }
      return View(student);
    }

    // GET: Students/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var student = await _context.Students
          .SingleOrDefaultAsync(m => m.Id == id);
      if (student == null)
      {
        return NotFound();
      }

      return View(student);
    }

    // POST: Students/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
      var student = await _context.Students.SingleOrDefaultAsync(m => m.Id == id);
      _context.Students.Remove(student);
      await _context.SaveChangesAsync();
      return RedirectToAction(nameof(Index));
    }

    private bool StudentExists(int id)
    {
      return _context.Students.Any(e => e.Id == id);
    }
  }
}
