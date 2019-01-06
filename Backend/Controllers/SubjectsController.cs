﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Backend.Models;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectsController : ControllerBase
    {
        private readonly BackendContext _context;

        public SubjectsController(BackendContext context)
        {
            _context = context;
        }
        // Student: get all subjects of 1 student, including start dates
        // GET: api/Subjects/Student/GetAllSubject
        [Route("Student")]
        [HttpGet]
        public async Task<IActionResult> StudentGetAllSubject()
        {
            string tokenHeader = Request.Headers["Authorization"];
            var token = tokenHeader.Replace("Basic ", "");
            var cr = _context.Credential.SingleOrDefault(c =>
                   c.AccessToken == token);
            var classAccounts = _context.ClazzAccount.Where(ac => ac.AccountId == cr.OwnerId);
            if (classAccounts.Any())
            {
                List<Subject> subjects = new List<Subject>();
                foreach (var classAccount in classAccounts)
                {
                    var classId = classAccount.ClazzId;
                    var classSubjects = _context.ClazzSubject.Where(cs => cs.ClazzId == classId).Include(cs => cs.Subject);
                    if (classSubjects.Any())
                    {
                        foreach (var classSubject in classSubjects)
                        {
                            subjects.Add(classSubject.Subject);
                        }
                    }
                }

                if (subjects.Any())
                {
                    return Ok(subjects);
                }
            }

            return NotFound();
        }

        // Manager: get all subjects of 1 student, including start dates
        // GET: api/Subjects/Manager/GetAllSubjectOneStudent
        [Route("Manager")]
        [HttpGet]
        public async Task<IActionResult> ManagerGetAllSubjectOneStudent()
        {
            bool isValid = Request.Query.ContainsKey("StudentId") != Request.Query.ContainsKey("ClassId");
            if (isValid)
            {
                if (Request.Query.ContainsKey("StudentId"))
                {
                    var studentId = Request.Query["StudentId"].ToString();
                    var classAccounts = _context.ClazzAccount.Where(ac => ac.AccountId == studentId);
                    if (classAccounts.Any())
                    {
                        List<Subject> subjects = new List<Subject>();
                        foreach (var classAccount in classAccounts)
                        {
                            var classId = classAccount.ClazzId;
                            var classSubjects = _context.ClazzSubject.Where(cs => cs.ClazzId == classId).Include(cs => cs.Subject);
                            if (classSubjects.Any())
                            {
                                foreach (var classSubject in classSubjects)
                                {
                                    subjects.Add(classSubject.Subject);
                                }
                            }
                        }

                        if (subjects.Any())
                        {
                            return Ok(subjects);
                        }
                    }
                }

                if (Request.Query.ContainsKey("ClassId"))
                {
                    List<Subject> subjects = new List<Subject>();
                    var classId = Request.Query["ClassId"].ToString();
                    var classSubjects = _context.ClazzSubject.Where(cs => cs.ClazzId == classId).Include(cs => cs.Subject);
                    if (classSubjects.Any())
                    {
                        foreach (var classSubject in classSubjects)
                        {
                            subjects.Add(classSubject.Subject);
                        }
                    }

                    return Ok(subjects);
                }
                return NotFound();
            }

            return BadRequest();
        }

        // PUT: api/Subjects/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSubject([FromRoute] string id, [FromBody] Subject subject)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != subject.Id)
            {
                return BadRequest();
            }

            _context.Entry(subject).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SubjectExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Subjects
        [HttpPost]
        public async Task<IActionResult> PostSubject([FromBody] Subject subject)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Subject.Add(subject);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSubject", new { id = subject.Id }, subject);
        }

        // DELETE: api/Subjects/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSubject([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var subject = await _context.Subject.FindAsync(id);
            if (subject == null)
            {
                return NotFound();
            }

            _context.Subject.Remove(subject);
            await _context.SaveChangesAsync();

            return Ok(subject);
        }

        private bool SubjectExists(string id)
        {
            return _context.Subject.Any(e => e.Id == id);
        }
    }
}