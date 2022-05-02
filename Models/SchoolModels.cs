using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Linq_practice_studnet_6.Models
{
    /*public class College
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Abbreviation { get; set; }
        public List<Course> Courses { get; set; }
    }

    public class Course
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public string Name { get; set; }
        public College College { get; set; }
        public List<Enrollment> Enrollments { get; set; }
    }
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Enrollment> Enrollments { get; set; }
    }
    public class Enrollment
    {
        public int Id { get; set; }
        public Course Course { get; set; }
        public Student Student { get; set; }
        public string Grade { get; set; }
        public int score { get; set; }
    }*/

    public class Restaurant
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int Restaurant_ID { get; set; }
        public string Restaurant_Name { get; set; }
        public string Restaurant_Location { get; set; }
        List<Menu> Menu { get; set; }
    }

    public class Menu
    {
        /*public int Id { get; set; }*/

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int Menu_Item_ID { get; set; }
        public string Item_Name { get; set; }
        public string Item_Category { get; set; }
        public Restaurant Restaurant { get; set; }
        public Nutrition Nutrition { get; set; }

    }

    public class Nutrition
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int Nutrition_ID { get; set; }
        public int Calorie { get; set; }
        public int Protein { get; set; }
        public int Sugar { get; set; }
        public int Total_Fat { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Menu_ForeignKey { get; set; }
        public Menu Menu { get; set; }

    }
}