﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LTM.School.Core.Models;
using LTM.School.EntityFramework;
using LTM.School.ViewModels;

namespace LTM.School.Controllers
{
  public class InstructorsController : Controller
  {
    private readonly SchoolDbContext _context;

    public InstructorsController(SchoolDbContext context)
    {
      _context = context;
    }

    // GET: Instructors
    public async Task<IActionResult> Index(int? id, int? courseid)
    {
      ///Include和ThanInclude 由于EF Core暂时不支持Lazy Loading，所以利用Include来加载额外数据
      var viewModel = new InstructorIndexData
      {
        Instructors = await _context.Instructors
        .Include(a => a.OfficeAssignment).Include(a => a.CourseAssignments)
        .ThenInclude(a => a.Course).ThenInclude(a => a.Enrollments).ThenInclude(a => a.Student)
        .Include(a => a.CourseAssignments).ThenInclude(a => a.Course).ThenInclude(a => a.Department).ToListAsync()
      };

      if (id != null)
      {

        ViewData["InstructorId"] = id.Value;

        var instructor = viewModel.Instructors.Single(a => a.Id == id.Value);

        viewModel.Courses = instructor.CourseAssignments.Select(a => a.Course);

      }

      if(courseid != null)
      {
        ViewData["CourseId"] = courseid.Value;

        viewModel.Enrollments = viewModel.Courses.Single(a => a.Id == courseid.Value).Enrollments;
      }

      return View(viewModel);
      // return View(await _context.Instructors.ToListAsync());
    }

    // GET: Instructors/Details/5
    public async Task<IActionResult> Details(int? id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var instructor = await _context.Instructors
          .SingleOrDefaultAsync(m => m.Id == id);
      if (instructor == null)
      {
        return NotFound();
      }

      return View(instructor);
    }

    // GET: Instructors/Create
    public IActionResult Create()
    {
      return View();
    }

    // POST: Instructors/Create
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,RealName,HireDate")] Instructor instructor)
    {
      if (ModelState.IsValid)
      {
        _context.Add(instructor);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
      }
      return View(instructor);
    }

    // GET: Instructors/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var instructor = await _context.Instructors.SingleOrDefaultAsync(m => m.Id == id);
      if (instructor == null)
      {
        return NotFound();
      }
      return View(instructor);
    }

    // POST: Instructors/Edit/5
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,RealName,HireDate")] Instructor instructor)
    {
      if (id != instructor.Id)
      {
        return NotFound();
      }

      if (ModelState.IsValid)
      {
        try
        {
          _context.Update(instructor);
          await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
          if (!InstructorExists(instructor.Id))
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
      return View(instructor);
    }

    // GET: Instructors/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var instructor = await _context.Instructors
          .SingleOrDefaultAsync(m => m.Id == id);
      if (instructor == null)
      {
        return NotFound();
      }

      return View(instructor);
    }

    // POST: Instructors/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
      var instructor = await _context.Instructors.SingleOrDefaultAsync(m => m.Id == id);
      _context.Instructors.Remove(instructor);
      await _context.SaveChangesAsync();
      return RedirectToAction(nameof(Index));
    }

    private bool InstructorExists(int id)
    {
      return _context.Instructors.Any(e => e.Id == id);
    }
  }
}
