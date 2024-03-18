using ASPcoreWebAPICRUD.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ASPcoreWebAPICRUD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentAPIController : ControllerBase
    {
        private readonly CodeSecondDbContext context;

        public StudentAPIController(CodeSecondDbContext context)
        {
            this.context = context;
        }
        [HttpGet]
        public async Task<ActionResult<List<Student>>> Getstudents()
        {
            var data = await context.Students.ToListAsync();
            return Ok(data);

        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> GetStudentById(int id)
        {
            var student = await context.Students.FindAsync(id);

            if(student == null)
            {
                return NotFound();
            }
            return student;

        }

        [HttpPost]
        public async Task<ActionResult<Student>> CreateStudent(Student student)
        {
           await context.Students.AddAsync(student);
            await context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetStudentById), new { id = student.Id }, student);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStudent(int id, Student student)
        {
            if (id != student.Id)
            {
                return BadRequest();
            }

            context.Entry(student).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentExists(id))
                {
                    return NotFound();
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while updating the student.");
                }
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            var student = await context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }

            context.Students.Remove(student);
            await context.SaveChangesAsync();

            return NoContent();
        }

        private bool StudentExists(int id)
        {
            return context.Students.Any(e => e.Id == id);
        }

        [HttpGet("gender/{gender}")]
        public async Task<ActionResult<List<Student>>> GetStudentsByGender(string gender)
        {
            var students = await context.Students.Where(s => s.StudentGender == gender).ToListAsync();
            return Ok(students);
        }

        [HttpGet("age/{age}")]
        public async Task<ActionResult<List<Student>>> GetStudentsByAge(int age)
        {
            var students = await context.Students.Where(s => s.Age == age).ToListAsync();
            return Ok(students);
        }

        [HttpGet("standard/{standard}")]
        public async Task<ActionResult<List<Student>>> GetStudentsByStandard(int standard)
        {
            var students = await context.Students.Where(s => s.Standard == standard).ToListAsync();
            return Ok(students);
        }

        [HttpGet("father/{fatherName}")]
        public async Task<ActionResult<List<Student>>> GetStudentsByFatherName(string fatherName)
        {
            var students = await context.Students.Where(s => s.FatherName == fatherName).ToListAsync();
            return Ok(students);
        }

        [HttpGet("search/{keyword}")]
        public async Task<ActionResult<List<Student>>> SearchStudents(string keyword)
        {
            var students = await context.Students.Where(s => s.StudentName.Contains(keyword) || s.FatherName.Contains(keyword)).ToListAsync();
            return Ok(students);
        }

    }
}
