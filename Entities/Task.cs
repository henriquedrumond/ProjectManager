using System;
using System.ComponentModel.DataAnnotations;

namespace ProjectManagerApi.Entities
{
	public class Task
	{
        [Key]
        public Guid Id { get; set; }
		public Guid ProjectId { get; set; }

        [MaxLength(250)]
        public string description { get; set; }

        [MaxLength(100)]
        public string Owner { get; set; }
		public DateTime Target { get; set; }
		public int Time { get; set; }
		public TaskPriority taskPriority { get; set; }
		public Status Status { get; set; }

        [MaxLength(250)]
        public string Comment { get; set; }
    }
}

