using LibrusProject.Model;
using Microsoft.AspNetCore.Mvc;

namespace LibrusProject.Controllers
{
    [ApiController]
    [Route("api/grades")]
    public class GradeController : ControllerBase
    {
        private readonly StudyContext _context;

        public GradeController(StudyContext context)
        {
            _context = context;
        }

        [HttpGet("{index}")]
        public IActionResult GetGradeById(int index)
        {
            var grade = _context.grades.Where(g => g.index == index).ToList();
            if (grade == null)
            {
                return NotFound();
            }
            return Ok(grade);
        }

        [HttpGet]
        public IActionResult GetAllGrades()
        {
            var allGrades = _context.grades.ToList();
            return Ok(allGrades);
        }

        //[HttpGet("{index}")]
        //public ActionResult<Grade> GetGradeByIndex(int index)
        //{
        //    var grade = _context.grades.Find(index);
        //    if (grade == null)
        //    {
        //        return NotFound();
        //    }
        //    return Ok(grade);
        //}

        [HttpPost]
        public ActionResult<Grade> AddGrade(Grade grade)
        {
            _context.grades.Add(grade);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetGradeById), new { index = grade.gradeId }, grade);
        }

        [HttpPut("{gradeId}")]
        public IActionResult UpdateGrade(int gradeId, [FromBody] Grade updatedGradeData)
        {
            var existingGrade = _context.grades.FirstOrDefault(s => s.gradeId == gradeId);

            if (existingGrade == null)
            {
                return NotFound();
            }
            existingGrade.index = updatedGradeData.index;
            existingGrade.subject = updatedGradeData.subject;
            existingGrade.value = updatedGradeData.value;
            existingGrade.description = updatedGradeData.description;


            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{gradeId}")]
        public IActionResult Deletegrade(int gradeId)
        {
            var gradeToDelete = _context.grades.FirstOrDefault(s => s.gradeId == gradeId);

            if (gradeToDelete == null)
            {
                return NotFound();
            }

            _context.grades.Remove(gradeToDelete);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
