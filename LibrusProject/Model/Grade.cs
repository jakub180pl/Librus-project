using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibrusProject.Model
{
    public class Grade
    {
        [Key] public int gradeId { get; set; }
        public int? index { get; set; }
        public string? subject { get; set; }
        public int? value { get; set; }
        public string? description { get; set; }
    }

}
