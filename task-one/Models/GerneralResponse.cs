using System.Linq.Expressions;

namespace task_one.Models
{
    public class GerneralResponse<T> where T : class
    {
        public int Status {  get; set; }
        public string Message { get; set; }
        public T? Data { get; set; } 

    }
}
