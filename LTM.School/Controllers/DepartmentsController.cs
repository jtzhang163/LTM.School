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
  public class DepartmentsController : Controller
  {
    private readonly SchoolDbContext _context;

    public DepartmentsController(SchoolDbContext context)
    {
      _context = context;
    }

    // GET: Departments
    public async Task<IActionResult> Index()
    {
      var schoolDbContext = _context.Departments.Include(d => d.Administrator);
      return View(await schoolDbContext.ToListAsync());
    }

    // GET: Departments/Details/5
    public async Task<IActionResult> Details(int? id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var department = await _context.Departments
          .Include(d => d.Administrator)
          .SingleOrDefaultAsync(m => m.Id == id);
      if (department == null)
      {
        return NotFound();
      }

      return View(department);
    }

    // GET: Departments/Create
    public IActionResult Create()
    {
      ViewData["InstructorId"] = new SelectList(_context.Instructors, "Id", "RealName");
      return View();
    }

    // POST: Departments/Create
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Name,Budget,StartDate,InstructorId,RowVersion")] Department department)
    {
      if (ModelState.IsValid)
      {
        _context.Add(department);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
      }
      ViewData["InstructorId"] = new SelectList(_context.Instructors, "Id", "RealName", department.Administrator);
      return View(department);
    }

    // GET: Departments/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var department = await _context.Departments.SingleOrDefaultAsync(m => m.Id == id);
      if (department == null)
      {
        return NotFound();
      }
      ViewData["InstructorId"] = new SelectList(_context.Instructors, "Id", "RealName", department.Administrator);
      return View(department);
    }

    // POST: Departments/Edit/5
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, byte[] rowVersion)
    {
      if(id == null)
      {
        return NotFound();
      }

      var department = await _context.Departments.Include(a => a.Administrator).SingleOrDefaultAsync(a => a.Id == id);

      if(department == null)
      {
        //ModelState.AddModelError("","该部门信息已被其他人删除！");
        return NotFound();
      }

      _context.Entry(department).Property("RowVersion").OriginalValue = rowVersion;

      if(await TryUpdateModelAsync<Department>(department))
      {
        try
        {
          await _context.SaveChangesAsync();
          return RedirectToAction(nameof(Index));
        }
        catch (DbUpdateException ex)
        {

          throw;
        }
      }

      //if (id != department.Id)
      //{
      //    return NotFound();
      //}

      //if (ModelState.IsValid)
      //{
      //    try
      //    {
      //        _context.Update(department);
      //        await _context.SaveChangesAsync();
      //    }
      //    catch (DbUpdateConcurrencyException)
      //    {
      //        if (!DepartmentExists(department.Id))
      //        {
      //            return NotFound();
      //        }
      //        else
      //        {
      //            throw;
      //        }
      //    }
      //    return RedirectToAction(nameof(Index));
      //}
      //ViewData["InstructorId"] = new SelectList(_context.Instructors, "Id", "Id", department.InstructorId);
      return View(department);
    }

    // GET: Departments/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var department = await _context.Departments
          .Include(d => d.Administrator)
          .SingleOrDefaultAsync(m => m.Id == id);
      if (department == null)
      {
        return NotFound();
      }

      return View(department);
    }

    // POST: Departments/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
      var department = await _context.Departments.SingleOrDefaultAsync(m => m.Id == id);
      _context.Departments.Remove(department);
      await _context.SaveChangesAsync();
      return RedirectToAction(nameof(Index));
    }

    private bool DepartmentExists(int id)
    {
      return _context.Departments.Any(e => e.Id == id);
    }
  }
}
