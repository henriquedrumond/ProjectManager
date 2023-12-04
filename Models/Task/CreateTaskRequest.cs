using System;
using System.ComponentModel.DataAnnotations;
using ProjectManagerApi.Entities;

namespace ProjectManagerApi.Models
{
    public class CreateTaskRequest
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public Guid ProjectId { get; set; }

        public string description { get; set; }

        public string Owner { get; set; }

        public DateTime Target { get; set; }

        [Required]
        public int Time { get; set; }

        [Required]
        public TaskPriority taskPriority { get; set; }

        [Required]
        public string Status { get; set; }

        public string Comment { get; set; }
    }
}

