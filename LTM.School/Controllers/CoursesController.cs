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
  public class CoursesController : Controller
  {
    private readonly SchoolDbContext _context;

    public CoursesController(SchoolDbContext context)
    {
      _context = context;
    }

    // GET: Courses
    public async Task<IActionResult> Index()
    {
      var schoolDbContext = _context.Courses.Include(c => c.Department);
      return View(await schoolDbContext.ToListAsync());
    }

    // GET: Courses/Details/5
    public async Task<IActionResult> Details(int? id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var course = await _context.Courses
          .Include(c => c.Department)
          .SingleOrDefaultAsync(m => m.Id == id);
      if (course == null)
      {
        return NotFound();
      }

      return View(course);
    }

    // GET: Courses/Create
    public IActionResult Create()
    {
      ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Name");
      return View();
    }

    // POST: Courses/Create
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Title,Credits,DepartmentId")] Course course)
    {
      if (ModelState.IsValid)
      {
        _context.Add(course);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
      }
      ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Name", course.DepartmentId);
      return View(course);
    }

    // GET: Courses/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var course = await _context.Courses.SingleOrDefaultAsync(m => m.Id == id);
      if (course == null)
      {
        return NotFound();
      }
      ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Name", course.DepartmentId);
      return View(course);
    }

    // POST: Courses/Edit/5
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Credits,DepartmentId")] Course course)
    {
      if (id != course.Id)
      {
        return NotFound();
      }

      if (ModelState.IsValid)
      {
        try
        {
          _context.Update(course);
          await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
          if (!CourseExists(course.Id))
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
      ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Name", course.DepartmentId);
      return View(course);
    }

    // GET: Courses/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var course = await _context.Courses
          .Include(c => c.Department)
          .SingleOrDefaultAsync(m => m.Id == id);
      if (course == null)
      {
        return NotFound();
      }

      return View(course);
    }

    // POST: Courses/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
      var course = await _context.Courses.SingleOrDefaultAsync(m => m.Id == id);
      _context.Courses.Remove(course);
      await _context.SaveChangesAsync();
      return RedirectToAction(nameof(Index));
    }

    private bool CourseExists(int id)
    {
      return _context.Courses.Any(e => e.Id == id);
    }


    public IActionResult UpdateCredits()
    {
      return View();
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> UpdateCredits(int? credit)
    {
      if(credit != null)
      {
        ViewData["UpdateRows"] = "受影响的行："+ await _context.Database.ExecuteSqlCommandAsync(@"UPDATE Course SET Credits = {0}", credit);
      }
      return View();
    }

  }
}
