﻿using System;
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

      //var department = await _context.Departments
      //    .Include(d => d.Administrator)
      //    .SingleOrDefaultAsync(m => m.Id == id);

      var department = await _context.Departments.FromSql("select * from Department WHERE Id = {0}", id)
        .Include(d => d.Administrator)
        .SingleOrDefaultAsync();
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

      if (department == null)
      {
        //ModelState.AddModelError("","该部门信息已被其他人删除！");
        //return NotFound();
        var deletedDepartment = new Department();

        await TryUpdateModelAsync(deletedDepartment);

        ModelState.AddModelError(string.Empty, "无法进行数据的修改。该部门信息已经被其他人所删除！");
        ViewBag.InstructorId =
            new SelectList(_context.Instructors, "Id", "RealName", deletedDepartment.InstructorId);
        return View(deletedDepartment);
      }

      _context.Entry(department).Property("RowVersion").OriginalValue = rowVersion;

      if (await TryUpdateModelAsync<Department>(department, "", a => a.Name, a => a.StartDate, a => a.Budget, a => a.InstructorId))
      {
        try
        {
          await _context.SaveChangesAsync();
          return RedirectToAction(nameof(Index));
        }
        catch (DbUpdateException ex)//rowVersion不一致，说明被改
        {
          var exceptionEntity = ex.Entries.Single();
          var clientValue = (Department)exceptionEntity.Entity;
          var databaseEntity = exceptionEntity.GetDatabaseValues();
          if (databaseEntity == null)
          {
            ModelState.AddModelError(string.Empty, "无法进行数据的修改。该部门信息已经被其他人所删除！");
          }
          else
          {

            var databaseValues = (Department)databaseEntity.ToObject();
            if (databaseValues.Name != clientValue.Name)
              ModelState.AddModelError("Name", $"当前值:{databaseValues.Name}");
            if (databaseValues.Budget != clientValue.Budget)
              ModelState.AddModelError("Budget", $"当前值:{databaseValues.Budget}");
            if (databaseValues.StartDate != clientValue.StartDate)
              ModelState.AddModelError("StartDate", $"当前值:{databaseValues.StartDate}");
            if (databaseValues.InstructorId != clientValue.InstructorId)
            {
              var instructorEntity =
                  await _context.Instructors.SingleOrDefaultAsync(
                      a => a.Id == databaseValues.InstructorId);

              ModelState.AddModelError("InstructorId", $"当前值:{instructorEntity?.RealName}");
            }

            ModelState.AddModelError("", "你正在编辑的记录已经被其他用户所修改，编辑操作已经被取消，数据库当前的值已经显示在页面上。请再次点击保存。否则请返回列表。");

            department.RowVersion = databaseValues.RowVersion;
            ModelState.Remove("RowVersion");
          }
        }
      }
      ViewData["InstructorId"] = new SelectList(_context.Instructors, "Id", "RealName", department.InstructorId);
      return View(department);
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
