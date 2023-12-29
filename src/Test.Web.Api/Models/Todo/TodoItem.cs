using System.ComponentModel.DataAnnotations;

namespace Test.Web.Api.Models.Todo
{
    public class TodoItem
    {
        public long Id { get; set; }

        [Required]
        public string? Name { get; set; }
        public bool IsComplete { get; set; }
    }
}