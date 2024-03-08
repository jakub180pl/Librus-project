using LibrusProject.Model;
using Microsoft.AspNetCore.Mvc;

namespace LibrusProject.Controllers
{
    [ApiController]
    [Route("api/students")]
    public class StudentController : ControllerBase
    {



    
        private readonly StudyContext _context;

        public StudentController(StudyContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Student>> GetStudents()
        {
            return _context.students.ToList();
        }

        [HttpGet("{index}")]
        public IActionResult GetGradeById(int index)
        {
            var student = _context.grades.Where(g => g.index == index).ToList();
            if (student == null)
            {
                return NotFound();
            }
            return Ok(student);
        }

        [HttpPost]
        public ActionResult<Student> AddStudent(Student student)
        {
            _context.students.Add(student);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetStudents), new { id = student.index }, student);
        }

        [HttpPut("{index}")]
        public IActionResult UpdateStudent(int index, [FromBody] Student updatedStudentData)
        {
            var existingStudent = _context.students.FirstOrDefault(s => s.index == index);

            if (existingStudent == null)
            {
                return NotFound();
            }

            existingStudent.name = updatedStudentData.name;
            existingStudent.surname = updatedStudentData.surname;
            existingStudent.adress = updatedStudentData.adress;


            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{index}")]
        public IActionResult DeleteStudent(int index)
        {
            var studentToDelete = _context.students.FirstOrDefault(s => s.index == index);

            if (studentToDelete == null)
            {
                return NotFound();
            }

            _context.students.Remove(studentToDelete);
            _context.SaveChanges();
            return NoContent();
        }
    }

}

