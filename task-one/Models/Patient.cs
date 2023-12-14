using System.ComponentModel.DataAnnotations;

namespace task_one.Models
{
    public class Patient
    {
        [Key]

        public long ID { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime BirthDate { get; set; }
        public int Gender { get; set; }
    }
}
