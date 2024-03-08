using System.ComponentModel.DataAnnotations;

namespace LibrusProject.Model
{
    public class Student
    {
        [Key] public int index { get; set; }
        public  string name { get; set; }
        public  string surname { get; set; }
        public  string adress { get; set; }

    }
}
