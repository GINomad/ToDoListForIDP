using System.ComponentModel.DataAnnotations;

namespace ToDoList.Models
{
    public class Group
    {
        public byte GroupId { get; set; }

        [Required]
        [StringLength(255)]
        public string GroupName { get; set; }
    }
}