using System;
using System.ComponentModel.DataAnnotations;

namespace TodoApp.Models
{
    public abstract class BaseEntity {}
    public class Todo : BaseEntity
    {
        [Key]
        public int id {get;set;}

        [Required(ErrorMessage="Title is required")]
        [Display(Name="Title")]
        public string title {get;set;}

        [Required(ErrorMessage="Description is required")]
        [Display(Name="Description")]
        public string description {get;set;}

        [Required(ErrorMessage="Due date is required")]
        [Display(Name="Due Date")]
        [DataType(DataType.DateTime)]
        public DateTime due_date {get;set;}

        public DateTime created_at {get;set;}
        public DateTime updated_at {get;set;}
    }
}