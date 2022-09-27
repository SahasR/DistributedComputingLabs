using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using DatabaseAPI.Models;

namespace DatabaseAPI.Controllers
{
    public class StudentTablesController : ApiController
    {
        private StudentsEntities db = new StudentsEntities();

        // GET: api/StudentTables
        public IQueryable<StudentTable> GetStudentTables()
        {
            return db.StudentTables;
        }

        // GET: api/StudentTables/5
        [ResponseType(typeof(StudentTable))]
        public IHttpActionResult GetStudentTable(int id)
        {
            StudentTable studentTable = db.StudentTables.Find(id);
            if (studentTable == null)
            {
                return NotFound();
            }

            return Ok(studentTable);
        }

        // PUT: api/StudentTables/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutStudentTable(int id, StudentTable studentTable)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != studentTable.id)
            {
                return BadRequest();
            }

            db.Entry(studentTable).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentTableExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/StudentTables
        [ResponseType(typeof(StudentTable))]
        public IHttpActionResult PostStudentTable(StudentTable studentTable)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.StudentTables.Add(studentTable);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (StudentTableExists(studentTable.id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = studentTable.id }, studentTable);
        }

        // DELETE: api/StudentTables/5
        [ResponseType(typeof(StudentTable))]
        public IHttpActionResult DeleteStudentTable(int id)
        {
            StudentTable studentTable = db.StudentTables.Find(id);
            if (studentTable == null)
            {
                return NotFound();
            }

            db.StudentTables.Remove(studentTable);
            db.SaveChanges();

            return Ok(studentTable);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool StudentTableExists(int id)
        {
            return db.StudentTables.Count(e => e.id == id) > 0;
        }
    }
}